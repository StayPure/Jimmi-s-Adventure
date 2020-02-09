using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState
{
	idle,
	walk,
	attack,
	interact,
	stagger
}

public class PlayerMovement : MonoBehaviour
{
	[Header("State Machine")]
	public PlayerState currentState;
	private bool running;
	
	[Header("Stats")]
	public FloatValue currentHealth;
	public float speed;
	public VectorValue startingPosition;
	private Rigidbody2D player;
	private Vector3 change;
	
	[Header("Signals")]
	public Signal playerHealthSignal;
	public Signal usedMagic;
	
	[Header("Ranged Attacks Variables")]
	public GameObject arrow;
	public GameObject chargeAttack;
	public GameObject bowActive;
	public float chargeTime;
	private bool charging;
	private float chargingPower;
	private Animator anime;
	public Item bow;
	public Item PotC;
	
	[Header("Item Management")]
	public Inventory playerInventroy;
	public SpriteRenderer recievedItemSprite;
	
	[Header("Sound Effects")]
	public GameObject normalHit;
	public GameObject bigHit;
	
	void Start()
	{
		currentState = PlayerState.walk;
		player = GetComponent<Rigidbody2D>();
		anime = GetComponent<Animator>();
		transform.position = startingPosition.runningValue;
		anime.SetFloat("moveX", 0); anime.SetFloat("moveY", -1);
	}

	private void Update()
	{
		change = Vector3.zero;
		change.x = Input.GetAxisRaw("Horizontal"); change.y = Input.GetAxisRaw("Vertical");
		
		if (playerInventroy.checkForItem(bow))
			bowActive.SetActive(true);
		
		if (Input.GetButton("Attack") && currentState != PlayerState.attack
		&& playerInventroy.currentMana > 0 && playerInventroy.checkForItem(PotC))
		{
			charging = true;
			if (charging)
				chargingPower += 1;
			anime.SetBool("charging", true);

			if (chargingPower >= chargeTime)
			{
				anime.SetBool("charged", true);
				anime.SetBool("charging", false);
			}
		}
		else if (Input.GetButtonDown("Secondary Attack") && currentState != PlayerState.attack
		         && playerInventroy.currentMana > 0 && playerInventroy.checkForItem(bow))
		{
			StartCoroutine(SecondAttackCo());
		}
		else if (Input.GetKey(KeyCode.LeftShift) && !running)
		{
			running = true;
			speed += 2;
		}
		else if (Input.GetKeyUp(KeyCode.LeftShift) && running)
		{
			speed -= 2;
			running = false;
		}
		else if (change != Vector3.zero && 
		         (currentState == PlayerState.walk || currentState == PlayerState.idle))
		{
			MoveCharacter();
		}
		else
		{
			anime.SetBool("walking", false);
		}

		if (Input.GetButtonUp("Attack"))
		{
			anime.SetBool("charging", false);
			if (chargingPower >= chargeTime)
				StartCoroutine(ChargedAttackCo());
			else
				StartCoroutine(AttackCo());

			chargingPower = 0;
			charging = false;
		}
	}

	private IEnumerator AttackCo()
	{
		anime.SetBool("attack", true);
		currentState = PlayerState.attack;
		yield return null;
		anime.SetBool("attack", false);
		yield return new WaitForSeconds(.33f);
		currentState = PlayerState.walk;
	}
	
	private IEnumerator ChargedAttackCo()
	{
		anime.SetBool("attack", true);
		currentState = PlayerState.attack;
		yield return null;
		MakeProjectile(chargeAttack);
		anime.SetBool("attack", false);
		yield return new WaitForSeconds(.33f);
		currentState = PlayerState.walk;
		playerInventroy.reduceMana(chargeAttack.GetComponent<PlayerProjectile>().manaCost);
		usedMagic.Raise();
	}
	
	private IEnumerator SecondAttackCo()
	{
		currentState = PlayerState.attack;
		yield return null;
		MakeProjectile(arrow);
		yield return new WaitForSeconds(.33f);
		currentState = PlayerState.walk;
		playerInventroy.reduceMana(arrow.GetComponent<PlayerProjectile>().manaCost);
		usedMagic.Raise();
	}

	Vector3 ChooseProjectileDirection()
	{
		float temp = Mathf.Atan2(anime.GetFloat("moveY"), anime.GetFloat("moveX"))
			* Mathf.Rad2Deg;
		
		return new Vector3(0, 0, temp);
	}

	private void MakeProjectile(GameObject projectile)
	{
		Vector2 temp = new Vector2(anime.GetFloat("moveX"), anime.GetFloat("moveY"));
		PlayerProjectile arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<PlayerProjectile>();
		arrow.Setup(temp, ChooseProjectileDirection());
	}
	void MoveCharacter()
	{
		change.Normalize();
		player.MovePosition(transform.position + change * speed * Time.deltaTime);
		anime.SetFloat("moveX", change.x); anime.SetFloat("moveY", change.y);
		anime.SetBool("walking", true);
	}
	
	public void presentItem()
	{
		if (playerInventroy.currentItem != null)
		{
			if (currentState != PlayerState.interact)
			{
				anime.SetBool("recieveItem", true);
				currentState = PlayerState.interact;
				recievedItemSprite.sprite = playerInventroy.currentItem.itemSprite;
			}
			else
			{
				anime.SetBool("recieveItem", false);
				currentState = PlayerState.idle;
				recievedItemSprite.sprite = null;
				playerInventroy.currentItem = null;
			}
		}
	}

	public void playerKnock(float knockTime, float damage)
	{
		if (damage < 2)
			Instantiate(normalHit, transform.position, Quaternion.identity);
		else
			Instantiate(bigHit, transform.position, Quaternion.identity);
		
		currentHealth.RuntimeValue -= damage;
		playerHealthSignal.Raise();
		if (currentHealth.RuntimeValue > 0)
		{
			StartCoroutine(knockCo(knockTime));
		}
		else
		{
			GetComponent<Reset>().ResetGameState();
			SceneManager.LoadScene("GameOver");
		}
	}
	
	private IEnumerator knockCo(float knockTime)
	{
		if (player != null)
		{
			yield return new WaitForSeconds(knockTime);
			player.velocity = Vector2.zero;
			currentState = PlayerState.idle;
		}
	}
}

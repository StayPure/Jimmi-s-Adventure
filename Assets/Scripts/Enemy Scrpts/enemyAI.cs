using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum enemyState
{
    idle,
    walk,
    attack,
    stagger,
    special,
}

public class enemyAI : MonoBehaviour
{
    [Header("State Machine")]
    public enemyState currentState;
    
    [Header("Stats")]
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public Vector2 homePosition;
    public bool isBoss;
    
    [Header("Death Effects")]
    public GameObject deathEffect;
    private float deathEffectTime = 1f;
    public LootTable loottable;
    public BoolValue defeated;

    [Header("Signals")]
    public Signal roomSignal;
    public Signal healthSignal;
    public GameObject bossHUD;
    
    [Header("Sound Effects")]
    public GameObject normalHit;
    public GameObject bigHit;
    
    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    public virtual void OnEnable()
    {
        health = maxHealth.initialValue;
        maxHealth.RuntimeValue = health;
        transform.position = homePosition;
        GetComponent<knockback>().damage = baseAttack;
        currentState = enemyState.idle;
        if(isBoss)
        {
            bossHUD = GameObject.FindWithTag("Canvas").transform.GetChild(4).gameObject;
            bossHUD.SetActive(true);
            bossHUD.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = enemyName;
            bossHUD.transform.GetChild(0).GetComponent<HeartManager>().updateHearts();
        }
    }
    
    public void OnDisable()
    {
        if (isBoss) 
            bossHUD.SetActive(false);
    }

    private void takeDamage(float damage)
    {
        if (damage < 2)
            Instantiate(normalHit, transform.position, Quaternion.identity);
        else
            Instantiate(bigHit, transform.position, Quaternion.identity);
        
        health -= damage;
        if (health <= 0)
        {
            DeathEffect();
            if (roomSignal != null)
                roomSignal.Raise();
            
            gameObject.SetActive(false);
        }
        maxHealth.RuntimeValue = health;
        healthSignal.Raise();
    }

    private void MakeLoot()
    {
        if (loottable != null)
        {
            GameObject current = loottable.loot();
            if (current != null)
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
        }
    }
    
    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            MakeLoot();
            if(defeated != null)
                defeated.RuntimeValue = true;
            
            Destroy(effect, deathEffectTime);
        }
    }
    
    public void enemyKnock(Rigidbody2D currentRidgeBody, float knockTime, float damage)
    {
        StartCoroutine(knockCo(currentRidgeBody, knockTime));
        takeDamage(damage);
    }
    
    private IEnumerator knockCo(Rigidbody2D currentRidgeBody, float knockTime)
    {
        if (currentRidgeBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            currentRidgeBody.velocity = Vector2.zero;
            currentState = enemyState.idle;
        }
    }
}

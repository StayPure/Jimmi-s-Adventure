using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CreatorBoss : AreaMeleeEnemy
{
    [Header("Special Attack Variables")]
    private bool specialDone;
    public Transform[] specialPoints;
    private Transform selectedSpecialPoint;
    public GameObject specialAttack;
    public float timeBetweenSpecials;
    
    public override void OnEnable()
    {
        health = maxHealth.initialValue;
        maxHealth.RuntimeValue = health;
        transform.position = homePosition;
        currentState = enemyState.idle;

        bossHUD = GameObject.FindWithTag("Canvas").transform.GetChild(4).gameObject;
        bossHUD.SetActive(true);
        bossHUD.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = enemyName;
        bossHUD.transform.GetChild(0).GetComponent<HeartManager>().updateHearts();
        
        StartCoroutine(specialAttackTimerCo());
    }
    
    protected override void checkDistance()
    {
        if (currentState == enemyState.special)
        {
            if (Vector2.Distance(selectedSpecialPoint.position, transform.position) < 0.0001 && !specialDone)
            {
                StartCoroutine(SpecialAttackCo());
                specialDone = true;
            }
            else
            {
                Vector3 move = Vector3.MoveTowards(transform.position, 
                                                    selectedSpecialPoint.position,
                                        (moveSpeed + 2) * Time.deltaTime);
                changeAnime(move - transform.position);
                rigid.MovePosition(move);
            }
            return;
        }
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius
                                    && (currentState.Equals(enemyState.walk) || currentState.Equals(enemyState.idle))
                                    && boundary.bounds.Contains(target.transform.position))
        {
            Vector3 move = Vector3.MoveTowards(transform.position, 
                                                        target.position, 
                                        moveSpeed * Time.deltaTime);
            changeAnime(move - transform.position);
            rigid.MovePosition(move);
            changeState(enemyState.walk);
            anime.SetBool("walking", true);
        }
        else if (distance <= chaseRadius && distance <= attackRadius
                                         && (currentState.Equals(enemyState.walk) || currentState.Equals(enemyState.idle)))
        {
            StartCoroutine(attackCo());
        }
        else if (Vector2.Distance(homePosition, transform.position) < 0.001)
        {
            anime.SetBool("walking", false);
            anime.SetFloat("moveX", 0);
            anime.SetFloat("moveY", -1);
            
        }
        else if (distance > chaseRadius || !boundary.bounds.Contains(target.transform.position))
        {
            Vector3 move = Vector3.MoveTowards(transform.position, 
                                                homePosition,
                                        moveSpeed * Time.deltaTime);
            changeAnime(move - transform.position);
            rigid.MovePosition(move);
        }
    }
    
    private IEnumerator SpecialAttackCo()
    { 
        if (selectedSpecialPoint == specialPoints[0])
        {
            anime.SetFloat("moveX", 0);
            anime.SetFloat("moveY", -1);
            anime.SetBool("attack", true);
            yield return null;
            MakeProjectile(specialAttack, "se");
            MakeProjectile(specialAttack, "s");
            MakeProjectile(specialAttack, "sw");
        }
        else if (selectedSpecialPoint == specialPoints[1])
        {
            anime.SetFloat("moveX", 1);
            anime.SetFloat("moveY", 0);
            anime.SetBool("attack", true);
            yield return null;
            MakeProjectile(specialAttack, "ne");
            MakeProjectile(specialAttack, "e");
            MakeProjectile(specialAttack, "se");
        }
        else if (selectedSpecialPoint == specialPoints[2])
        {
            anime.SetFloat("moveX", -1);
            anime.SetFloat("moveY", 0);
            anime.SetBool("attack", true);
            yield return null;
            MakeProjectile(specialAttack, "nw");
            MakeProjectile(specialAttack, "w");
            MakeProjectile(specialAttack, "sw");
        }
        else if (selectedSpecialPoint == specialPoints[3])
        {
            anime.SetFloat("moveX", 0);
            anime.SetFloat("moveY", 1);
            anime.SetBool("attack", true);
            yield return null;
            MakeProjectile(specialAttack, "ne");
            MakeProjectile(specialAttack, "n");
            MakeProjectile(specialAttack, "nw");
        }
        
        anime.SetBool("attack", false);
        yield return new WaitForSeconds(.33f);
        currentState = enemyState.walk;
    }
    
    Vector3 ChooseArrowDirection(Vector2 direction)
    {
        float temp = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		
        return new Vector3(0, 0, temp);
    }

    private void MakeProjectile(GameObject projectile, string direction)
    {
        Vector2 temp = Vector2.zero;
        if (direction.Equals("s"))
            //go south
            temp = new Vector2(0, -1);
        else if (direction.Equals("sw"))
            //go southwest
            temp = new Vector2(-1, -1);
        else if (direction.Equals("se"))
            //go southeast
            temp = new Vector2(1, -1);
        else if (direction.Equals("n"))
            //go north
            temp = new Vector2(0, 1);
        else if (direction.Equals("ne"))
            //go northeast
            temp = new Vector2(1, 1);
        else if (direction.Equals("nw"))
            //go northwest
            temp = new Vector2(-1, 1);
        else if (direction.Equals("w"))
            //go west
            temp = new Vector2(-1, 0);
        else if (direction.Equals("e"))
            //go east
            temp = new Vector2(1, 0);
        
        PlayerProjectile arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<PlayerProjectile>();
        arrow.Setup(temp, ChooseArrowDirection(temp));
    }

    private IEnumerator specialAttackTimerCo()
    {
        while (true)
        {
            specialDone = false;
            yield return new WaitForSeconds(timeBetweenSpecials);
            selectedSpecialPoint = specialPoints[Random.Range(0, 4)];
            changeState(enemyState.special);
            yield return new WaitUntil(() => (currentState != enemyState.special));
        }
    }
}

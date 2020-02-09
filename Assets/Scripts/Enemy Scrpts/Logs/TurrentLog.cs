using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentLog : log
{
    [Header("Projectile Variables")]
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire;

    private void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    protected override void checkDistance()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius
                                    && (currentState.Equals(enemyState.walk) || currentState.Equals(enemyState.idle)))
        {
            if (canFire)
            {
                Vector3 distanceVector = target.transform.position - transform.position;
                GameObject currentProjectile = Instantiate(projectile, 
                                                            transform.position, 
                                                            Quaternion.identity);
                currentProjectile.GetComponent<Projectile>().Launch(distanceVector);
                changeState(enemyState.walk);
                anime.SetBool("wakeUp", true);
                canFire = false;
            }
        }
        else if (distance > chaseRadius)
        {
            anime.SetBool("wakeUp", false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLog : log
{
    [Header("Pathing Variables")]
    public Transform[] path;
    public float roundingDistance;
    private int currentPoint;
    protected override void checkDistance()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius
                                    && (currentState.Equals(enemyState.walk) || currentState.Equals(enemyState.idle)))
        {
            Vector3 move = Vector3.MoveTowards(transform.position, 
                                                        target.position, 
                                        moveSpeed * Time.deltaTime);
            changeAnime(move - transform.position);
            rigid.MovePosition(move);
            anime.SetBool("wakeUp", true);
        }
        else if (distance > chaseRadius)
        {
            if (Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
            {
                Vector3 move = Vector3.MoveTowards(transform.position, 
                                                    path[currentPoint].position,
                                            moveSpeed * Time.deltaTime);
                changeAnime(move - transform.position);
                rigid.MovePosition(move);
            }
            else
            {
                changeGoal();
            }
        }
    }

    private void changeGoal()
    {
        if (currentPoint == path.Length - 1)
            currentPoint = 0;
        else
            currentPoint++;
    }
}

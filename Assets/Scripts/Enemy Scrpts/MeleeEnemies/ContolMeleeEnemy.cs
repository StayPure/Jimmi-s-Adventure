using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContolMeleeEnemy : MeleeEnemy
{
    [Header ("Pathing Variables")]
    public PatrolPoint[] path;
    public int currentPoint;
    public float roundingDistance;
    private bool notWaiting = true;

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
            anime.SetBool("walking", true);
        }
        else if (distance <= chaseRadius && distance <= attackRadius
                                         && (currentState.Equals(enemyState.walk) || currentState.Equals(enemyState.idle)))
        {
            StartCoroutine(attackCo());
        }
        else if (distance > chaseRadius && notWaiting)
        {
            if (Vector3.Distance(transform.position, path[currentPoint].gameObject.transform.position) > roundingDistance)
            {
                Vector3 move = Vector3.MoveTowards(transform.position, 
                                                            path[currentPoint].gameObject.transform.position,
                                            moveSpeed * Time.deltaTime);
                changeAnime(move - transform.position);
                rigid.MovePosition(move);
                anime.SetBool("walking", true);
            }
            else
            {
                changeGoal();
            }
        }
    }

    private void changeGoal()
    {
        StartCoroutine(WaitCo(path[currentPoint].waitTime, currentPoint));
        if (currentPoint == path.Length - 1)
            currentPoint = 0;
        else
            currentPoint++;
    }

    private IEnumerator WaitCo(float waitTime, int curPoint)
    {
        anime.SetBool("walking", false);
        anime.SetFloat("moveX", path[curPoint].lookArea.x);
        anime.SetFloat("moveY", path[curPoint].lookArea.y);
        notWaiting = false;
        yield return new WaitForSeconds(waitTime);
        notWaiting = true;
    }
}

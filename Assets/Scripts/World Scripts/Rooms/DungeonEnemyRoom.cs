using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{
    [Header("Enemy Door Variables")]
    public Door[] doors;
    private IEnumerator doorsClosing;
    private int enemyCount;
    
    
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //Activate all enemies and breakables
            foreach (enemyAI e in enemies)
                ChangeActivation(e, true);

            foreach (breakable p in breakables)
                ChangeActivation(p, true);

            Door currentDoor = doors[0];
            foreach (Door d in doors)
            {
                if (Vector2.Distance(d.transform.position, other.transform.position) <
                    Vector2.Distance(currentDoor.transform.position, other.transform.position))
                    currentDoor = d;
            }
            
            if (roomMusic != null && roomMusic != speaker.clip)
            {
                speaker.clip = roomMusic;
                speaker.Play();
            }

            doorsClosing = CloseDoorsCo(currentDoor, other);
            StartCoroutine(doorsClosing);
            virtualCamera.SetActive(true);
            enemyCount = enemies.Length;
        }
        
    }

    public bool playerInRoom(Door d, Collider2D player)
    {
        bool inRoom;
        Vector2 playerPosition = player.transform.position;
        Vector2 doorPosition = d.gameObject.transform.parent.transform.position;
        if (d.doorPlacement == doorPlacement.top)
            inRoom = playerPosition.y + 1 < doorPosition.y;
        else if (d.doorPlacement == doorPlacement.left)
            inRoom = playerPosition.x - 1 > doorPosition.x;
        else if (d.doorPlacement == doorPlacement.right)
            inRoom = playerPosition.x + 1 < doorPosition.x;
        else
            inRoom = playerPosition.y - 1 > doorPosition.y;

        return inRoom;
    }
    
    private IEnumerator CloseDoorsCo(Door d, Collider2D player)
    {
        yield return new WaitUntil(() => playerInRoom(d, player));
        CloseDoors();
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //Deactivate all enemies and breakables
            foreach (enemyAI e in enemies)
                ChangeActivation(e, false);

            foreach (breakable p in breakables)
                ChangeActivation(p, false);
            
            StopCoroutine(doorsClosing);
            virtualCamera.SetActive(false);
        }
    }

    public void checkEnemies()
    {
        enemyCount--;
        if (enemyCount == 0)
            OpenDoors();
    }
    
    public void CloseDoors()
    {
        foreach (Door d in doors)
            d.Close();
    }
    
    public void OpenDoors()
    {
        foreach (Door d in doors)
            d.Open();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class GetEnemies : MonoBehaviour
{
    //list of enemies in range
    private List<GameObject> enemies = new List<GameObject>();

    //checks if enemies enter range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if it are enemies
        if (collision.GetComponent<Enemy>() == true)
        {
            enemies.Add(collision.gameObject);                      //adds enemies to the list
            InfoManager.instance._enemyAmount = enemies.Count;     //updates the enemy count
            enemies.Sort(SortFunc);                               //sorts the enemies by distance from the player
            InfoManager.instance._closestEnemy = enemies[0];     //sets the closest enemy variable in the InfoManager
        }
    }

    //checks if enemies exit range
    private void OnTriggerExit2D(Collider2D collision)
    {
        enemies.Remove(collision.gameObject);                    //removes enemies form the list
        InfoManager.instance._enemyAmount = enemies.Count;      //updates the enemy count
    }

    //as named this function is used to sort the enemies from far away to close
    private int SortFunc(GameObject a, GameObject b)
    {
        //checks the distances between enemies and player and returns the right values to change their position on the list
        if (Vector2.Distance(a.transform.position, transform.position) < Vector2.Distance(b.transform.position, transform.position))
        {
            return -1;
        } else if (Vector2.Distance(a.transform.position, transform.position) > Vector2.Distance(b.transform.position, transform.position)){
            return 1;
        }
        else {
            return 0;
        }
    }
}

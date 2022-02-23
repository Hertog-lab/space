using System.Collections.Generic;
using UnityEngine;

public class GetEnemies : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();    //list of enemies in range
    private List<GameObject> satalites = new List<GameObject>(); //list of satalites in range

    //checks if objects enter range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if the collision object is an enemy
        if (collision.GetComponent<Enemy>() == true)
        {
            enemies.Add(collision.gameObject);                      //adds enemies to the list
            InfoManager.instance._enemyAmount = enemies.Count;     //updates the enemy count
            enemies.Sort(SortFunc);                               //sorts the enemies by distance from the player
            InfoManager.instance._closestEnemy = enemies[0];     //sets the closest enemy variable in the InfoManager
        }

        //checks if the collision object is a satalite
        if (collision.GetComponent<Satalite>() == true)
        {
            satalites.Add(collision.gameObject);                        //adds satalites to the list
            InfoManager.instance._sataliteAmount = satalites.Count;    //updates the satalite count
            satalites.Sort(SortFunc);                                 //sortes the satalites by distance from the player
            InfoManager.instance._closestSatalite = satalites[0];    //sets the closest satalite variable in the InfoManager
        }
    }

    //checks if enemies exit range
    private void OnTriggerExit2D(Collider2D collision)
    {
        //checks if the collision object is an enemy
        if (collision.GetComponent<Enemy>() == true)
        {
            enemies.Remove(collision.gameObject);                    //removes the enemy form the list
            InfoManager.instance._enemyAmount = enemies.Count;      //updates the enemy count
        }

        //checks if the collision object is a satalite
        if (collision.GetComponent<Satalite>() == true)
        {
            satalites.Remove(collision.gameObject);                    //removes the satalite from the list
            InfoManager.instance._sataliteAmount = satalites.Count;   //updates the satalite count
        }
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

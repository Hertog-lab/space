using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    public static InfoManager instance;

    public int _enemyAmount;               //used to show how many enemies are nearby
    public int _sataliteAmount;           //used to show how many satalites are nearby
    public GameObject _closestEnemy;     //stores the closest enemy
    public GameObject _closestSatalite; //stores the closest satalite

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
}

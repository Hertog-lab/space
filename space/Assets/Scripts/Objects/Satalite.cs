using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satalite : MonoBehaviour
{
    private bool isFixed;
    void Start()
    {
        InfoManager.instance._disconectedSatalites += 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFixed != true)
        {
            if (collision.GetComponent<Movement>() == true)
            {
                InfoManager.instance._connectedSatalites += 1;
                isFixed = true;
            }
        }
    }
}

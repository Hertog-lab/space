using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satalite : MonoBehaviour
{
    void Awake()
    {
        InfoManager.instance._disconectedSatalites += 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        InfoManager.instance._connectedSatalites += 1;
    }
}

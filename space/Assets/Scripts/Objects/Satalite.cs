using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satalite : MonoBehaviour
{

    AudioSource audioSource;
    public bool isFixed;
    void Start()
    {
        InfoManager.instance._disconectedSatalites += 1;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFixed != true)
        {
            if (collision.GetComponent<Movement>() == true)
            {
                InfoManager.instance._connectedSatalites += 1;
                isFixed = true;
                //TODO Add happy fixing sound!
                audioSource.enabled = false;
            }
        }
    }
}

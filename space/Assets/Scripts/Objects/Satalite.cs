using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satalite : MonoBehaviour
{
    private SpriteRenderer sr;
    private AudioSource audioSource;
    public bool isFixed;
    void Start()
    {
        InfoManager.instance._disconectedSatalites += 1;
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (InfoManager.instance._scannerIsActive)
        {
             sr.enabled = true;
        } else {
            sr.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFixed != true)
        {
            if (collision.GetComponent<Movement>() == true)
            {
                InfoManager.instance._connectedSatalites += 1;
                isFixed = true;
                if (InfoManager.instance._hitPoints < 3) {
                    InfoManager.instance._hitPoints++;
                    //TODO Add happy fixing sound!
                    audioSource.enabled = false;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenController : MonoBehaviour
{
    Image winscreen;
    private void Start()
    {
        transform.GetChild(10).gameObject.SetActive(false);
    }

    private void Update()
    {
        if (InfoManager.instance._disconectedSatalites < 1)
            transform.GetChild(10).gameObject.SetActive(true);

    }
}

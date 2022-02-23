using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] presets;
    private void Start()
    {
        presets = Resources.LoadAll<GameObject>("Presets");
        for (int a = 0; a < presets.Length; a++)
        {
           Instantiate(presets[a],new Vector2(0, 0), Quaternion.identity);
        }
    }
}

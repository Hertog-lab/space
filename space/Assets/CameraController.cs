using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    Vector3 targetPos;
    float cameraRotation;

    private void Start()
    {
        if (targetPos == null)
            targetPos = new Vector3(0, 0, 0);
    }

    public void Update()
    {
        targetPos = new Vector3(target.transform.position.x, target.transform.position.y, -10);
        transform.position = targetPos;

        if (!InfoManager.instance._scannerIsActive)
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, 0.005f);
    }
}



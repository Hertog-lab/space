using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform target;

    Vector3 targetPos;
    float cameraRotation;

    public void Update()
    {
        targetPos = new Vector3(target.transform.position.x, target.transform.position.y, -10);
        transform.position = targetPos;
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, 0.001f);

    }

}

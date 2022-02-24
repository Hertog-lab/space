using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Transform target;
    float speed = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        Vector3 targetDir = target.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);

        rb.AddForce(transform.up * speed - angle * Time.deltaTime, ForceMode2D.Force);

    }
}

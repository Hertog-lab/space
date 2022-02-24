using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Transform target;
    float speed = 10;
    float maxVelocity = 80;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Rotate();
        LimitVelocity();

    }

    private void LimitVelocity()
    {
        if (rb.velocity.magnitude > maxVelocity)
            rb.AddForce(-rb.velocity * (rb.velocity.magnitude - (maxVelocity - rb.velocity.magnitude)));

    }

    private void Rotate()
    {
        Vector3 targetDir = target.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);

        //if (angle > 50)
        //    Brake();
        //else
            Thrust(angle);

    }

    private void Thrust(float angle)
    {
        angle = Mathf.Clamp(angle, 0.01f, 10);
        rb.AddForce(transform.up * (speed - angle) * Time.deltaTime, ForceMode2D.Force);
    }

    private void Brake()
    {
        if (rb.velocity.magnitude > 0)
            rb.AddForce(-rb.velocity * rb.velocity);
    }

}

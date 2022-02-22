using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float x, y;

    public float thrust;
    public float maxThrust;
    public float maxBoost;
    public float turnSpeed;

    public bool scannerIsActive;
    public bool boostIsActive;

    Vector2 direction;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        thrust = 200;
        maxThrust = thrust;
        maxBoost = thrust * 2;
        turnSpeed = 2;


    }

    private void Update()
    {
        ReadInputs();
        RotateShip();
        ApplyBoost();
        LimitVelocity();

        if (x + y != 0)
            Thrust();

    }

    private void LimitVelocity()
    {
        if (rb.velocity.magnitude > 70)
            rb.AddForce(-rb.velocity * (rb.velocity.magnitude - 70));
    }

    private void ReadInputs()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
            boostIsActive = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            boostIsActive = false;
    }

    private void RotateShip()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }

    private void Thrust()
    {
        thrust += Time.deltaTime;
        rb.AddForce(transform.up * thrust * y * Time.deltaTime * 10);
        rb.AddForce(transform.right * thrust * x * Time.deltaTime * 10);
    }

    private void ApplyBoost()
    {
        if (boostIsActive) {
            thrust += Time.deltaTime * 25;
            thrust = Mathf.Clamp(thrust, 0, maxBoost);
        }
        else
            thrust = Mathf.Clamp(thrust, 0, maxThrust);
    }
}

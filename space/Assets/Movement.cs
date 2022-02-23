using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float x, y;

    float thrust;
    float maxThrust;
    float maxBoost;
    float maxVelocity = 70;
    float turnSpeed;

    public bool scannerIsActive;
    bool boostIsActive;

    Vector2 direction;
    Vector2 oldDirection;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        thrust = 1500;
        maxThrust = thrust;
        maxBoost = thrust * 2;
        turnSpeed = 2;


    }

    private void Update()
    {
        ReadInputs();

        if (!scannerIsActive && x + y != 0)
            Thrust();
        
        RotateShip();

        //if (!scannerIsActive) {
        //}
        //else
        //    UseScanner();

        ApplyBoost();
        LimitVelocity();


    }

    private void UseScanner()
    {
        print("Scanning...");
    }

    private void LimitVelocity()
    {
        if (rb.velocity.magnitude > maxVelocity)
            rb.AddForce(-rb.velocity * (rb.velocity.magnitude - maxVelocity));
    }

    private void ReadInputs()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
            boostIsActive = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            boostIsActive = false;
        
        if (Input.GetKey(KeyCode.Space))
            scannerIsActive = true;
        else
            scannerIsActive = false;
        if (Input.GetKeyDown(KeyCode.Space))
            oldDirection = direction;
            
    }

    private void RotateShip()
    {
        if (!scannerIsActive) {
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        }
        if (scannerIsActive) {
            float angle = Mathf.Atan2(oldDirection.y, oldDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * rotation, turnSpeed * Time.deltaTime);
        }
    }

    private void Thrust()
    {
        thrust += Time.deltaTime;
        rb.AddForce(transform.up * thrust * y * Time.deltaTime);
        rb.AddForce(transform.right * thrust * x * Time.deltaTime);
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

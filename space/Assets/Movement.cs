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

    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] AudioSource thrusterAudio;
    [SerializeField] AudioSource boostAudio;
    Rigidbody2D rb;

    bool boostIsActive;

    Vector2 direction;
    Vector2 oldDirection;


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

        if (!InfoManager.instance._scannerIsActive && y != 0 || x != 0) {
            Thrust();
        }
        ApplyBoost();
        RotateShip();
        LimitVelocity();


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
            InfoManager.instance._scannerIsActive = true;
        else
            InfoManager.instance._scannerIsActive = false;

        if (Input.GetKeyDown(KeyCode.Space))
            oldDirection = direction;
        if (Input.GetKeyUp(KeyCode.Space))
            oldDirection = new Vector2(0, 0);

    }

    private void RotateShip()
    {
        if (!InfoManager.instance._scannerIsActive) {
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        }
        if (InfoManager.instance._scannerIsActive) {
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
            if (!boostAudio.isPlaying)
                boostAudio.Play();

            thrust += Time.deltaTime * 25;
            thrust = Mathf.Clamp(thrust, 0, maxBoost);
        }
        else
            boostAudio.Stop();
        thrust = Mathf.Clamp(thrust, 0, maxThrust);
    }
}

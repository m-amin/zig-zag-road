using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody rb;
    private bool rollLeft;

    public float speed = 4f;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rollLeft = true;
    }

    void Update()
    {
        CheckInput();
        CheckBallIsOutOfTheBound();
    }

    void CheckBallIsOutOfTheBound()
    {
        if (GameplayController.instance.gamePlaying)
        {
            if (transform.position.y < -4)
            {
                GameplayController.instance.gamePlaying = false;
                Destroy(gameObject);
            }
        }
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameplayController.instance.gamePlaying)
            {
                GameplayController.instance.gamePlaying = true;
                GameplayController.instance.ActivateTileSpawner();
            }
        }

        if (GameplayController.instance.gamePlaying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rollLeft = !rollLeft;   
            }
        }
    }

    void FixedUpdate()
    {
        if (GameplayController.instance.gamePlaying)
        {
            if (rollLeft)
            {
                rb.velocity = new Vector3(-speed, Physics.gravity.y, 0f);
            }else
            {
                rb.velocity = new Vector3(0f, Physics.gravity.y, speed);
            }
        }
    }
}

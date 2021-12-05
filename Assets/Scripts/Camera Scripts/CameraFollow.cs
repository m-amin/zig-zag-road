using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform ballTarget;
    private Vector3 oldPosition;
    
    
    void Awake()
    {
        ballTarget = GameObject.FindWithTag("Ball").transform;
        oldPosition = ballTarget.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ballTarget)
        {
            Vector3 newPosition = ballTarget.position;
            Vector3 delta = oldPosition - newPosition;
            delta.y = 0f;
            transform.position = transform.position - delta;
            oldPosition = newPosition;
        }
    }
}

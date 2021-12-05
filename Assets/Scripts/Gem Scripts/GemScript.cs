using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    [SerializeField] private GameObject sparkleX;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            Instantiate(sparkleX, transform.position, Quaternion.identity);
            GameplayController.instance.PlayCollectAudio();
            gameObject.SetActive(false);
        }
    }
}

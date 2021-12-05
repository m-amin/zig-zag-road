using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleScripts : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DeactivateAfterTime());
    }

    IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(1.5f);  
        gameObject.SetActive(false);
    }
}

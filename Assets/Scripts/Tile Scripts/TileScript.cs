
using System.Collections;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;

    [SerializeField] private GameObject gem;
    [SerializeField] private float chanceForCollectable;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        if (Random.value < chanceForCollectable)
        {
            Vector3 temp = transform.position;
            temp.y += 2f;
            Instantiate(gem, temp, Quaternion.identity);
        }
    }
    
    

    IEnumerator TriggerFollingDown()
    {
        yield return new WaitForSeconds(0.3f);
        rb.isKinematic = false;
        audioSource.Play();
        StartCoroutine(TurnOffGameObject());
    }

    IEnumerator TurnOffGameObject()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    void OnTriggerExit(Collider target)
    {
        if (target.tag == "Ball")
        {
            StartCoroutine(TriggerFollingDown());
        }
    }
}

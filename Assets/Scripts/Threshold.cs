using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Threshold : MonoBehaviour
{
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private float threshold = 10f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            transform.position = playerSpawnPoint.position;
            rb.velocity = new Vector3(0,0,0);
           
        }
    }
}



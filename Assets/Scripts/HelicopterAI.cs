using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterAI : MonoBehaviour
{
    [SerializeField] private Transform[] flyingPoints;
    [SerializeField] private float flyingSpeed = 5f;
    public bool isPassengerInside = false;
    private Rigidbody rb;
    private float distanceThreshhold = 0.9f;
    private int flyingPointIndex;
    private float rotationSpeed = 1f;

    void Start()
    {
        flyingPointIndex = 0;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isPassengerInside) 
        { 
            Vector3 targetDirection = (flyingPoints[flyingPointIndex].position - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, flyingPoints[flyingPointIndex].position);

            if (distanceToTarget <= distanceThreshhold)
            {
                flyingPointIndex++;
            }
            else
            {
                //rb.velocity = targetDirection * flyingSpeed;
                rb.MovePosition(transform.position + targetDirection * flyingSpeed * Time.deltaTime);
            }

            // Rotate towards the next flying point
            if (flyingPointIndex == 1)
            {
                Vector3 newDirection = Vector3.Slerp(transform.forward, targetDirection, rotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(newDirection);
                //transform.LookAt(flyingPoints[flyingPointIndex]);
            }
        }
    }
}

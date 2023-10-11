using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SlipEffector3D : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private float initialSpeed;
    private bool isInsideCollider;

    [SerializeField] private float slideSpeedFactor = 1.3f;
    [SerializeField] private float slowdownFactor = 1.3f;
    [SerializeField] private GameObject splashParticles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControler>())
        {
            playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody)
            {
                splashParticles.SetActive(true);
                initialSpeed = playerRigidbody.velocity.magnitude;
                isInsideCollider = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isInsideCollider && playerRigidbody)
        {
            // Calculate sliding speed proportionally based on the initial speed
            float slideSpeed = initialSpeed * slideSpeedFactor;

            // Apply force to maintain sliding speed
            Vector3 slideForce = playerRigidbody.velocity.normalized * slideSpeed;
            playerRigidbody.AddForce(slideForce - playerRigidbody.velocity, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isInsideCollider && playerRigidbody)
        {
            // Slow down the player speed 
            playerRigidbody.velocity *= slowdownFactor;

            splashParticles.SetActive(false);
            isInsideCollider = false;
            playerRigidbody = null;
        }
    }
}

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControler>())
        {
            playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody)
            {
                initialSpeed = playerRigidbody.velocity.magnitude;
                isInsideCollider = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isInsideCollider && playerRigidbody)
        {
            // Calculate the slide speed proportionally based on the initial speed
            float slideSpeed = initialSpeed * slideSpeedFactor;

            // Apply a force to maintain the sliding speed
            Vector3 slideForce = playerRigidbody.velocity.normalized * slideSpeed;
            playerRigidbody.AddForce(slideForce - playerRigidbody.velocity, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isInsideCollider && playerRigidbody)
        {
            // Slow down the player proportionally based on the initial speed
            playerRigidbody.velocity *= slowdownFactor;

            isInsideCollider = false;
            playerRigidbody = null;
        }
    }
}

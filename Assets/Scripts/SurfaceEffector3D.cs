using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceEffector3D : MonoBehaviour
{
    public float conveyorSpeed = 5f;

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

        if (rb)
        {
            Vector3 moveDirection = transform.forward;
            Vector3 targetVelocity = moveDirection * conveyorSpeed;
            Vector3 currentVelocity = rb.velocity;

            // Cancel the current rigid body velocity
            rb.velocity = new Vector3(0f, currentVelocity.y, 0f);

            // Apply the target velocity
            rb.AddForce(targetVelocity - currentVelocity, ForceMode.VelocityChange);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipEffector3D : MonoBehaviour
{
    public float slipSpeed = 5f;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb)
        {
            Vector3 moveDirection = rb.transform.forward;
            Vector3 targetVelocity = moveDirection * slipSpeed;
            rb.AddForce(targetVelocity , ForceMode.VelocityChange);
        }
    }
}

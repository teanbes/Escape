using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffector3D : MonoBehaviour
{
    public float upwardForce = 25f;
    public float maxUpwardDisplacement = 1f;
    public float upwardForceFactor = 1.5f;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerControler>())
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

                Debug.Log("Flyiiingggg");
                float displacementRatio = Mathf.Clamp01(rb.position.y / maxUpwardDisplacement);
                Vector3 upwardForceVector = Vector3.up * upwardForce * displacementRatio * upwardForceFactor;
                rb.AddForce(upwardForceVector, ForceMode.Acceleration);
            
        }
    }

}

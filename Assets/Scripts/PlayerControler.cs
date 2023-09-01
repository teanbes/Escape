using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float impulseForce;
    [SerializeField] private Transform indicator;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private AnimationCurve impulseCurve;
    [SerializeField] private AnimationCurve indicatorCurve;
    private Rigidbody rb;
    private bool isImpulsing;
    private float impulseDistance;
    private Vector3 impulseInitPosition;
    private RaycastHit hit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
        // Cast a ray from the mouse cursor into the world
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayerMask) && isImpulsing == false)
        {
            // Calculate the direction from the object to the hit point
            Vector3 direction = hit.point - transform.position;
            direction.y = 0f; // Keep the rotation in the y-axis

            if (direction != Vector3.zero)
            {
                // Calculate the rotation needed to look at the hit point
                Quaternion rotation = Quaternion.LookRotation(direction);

                // Apply the rotation to the object's y-axis
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
            }
        }

        // Get the mouse start position to calculate impulse force
        if (Input.GetMouseButtonDown(0)) 
        {
            isImpulsing = true;
            impulseInitPosition = hit.point;
        }

        // Update arrow indicator
        if (Input.GetMouseButton(0))
        {
            
            float indicatorDistance = Vector3.Distance(impulseInitPosition, hit.point);
            float indicatorSize = indicatorCurve.Evaluate(indicatorDistance);
            
            if (indicatorDistance > 1f)
            {
                indicator.localScale = new Vector3(1.0f, 1.0f, indicatorSize);
            }
   
        }

        // Get the mouse end position and calculate impulse force
        if (Input.GetMouseButtonUp(0)) 
        {
            isImpulsing = false;

            // Get the mouse end position to calculate impulse force
            Vector3 impulseEndPosition = hit.point;
            // Calculate distance between mouse inputs
            impulseDistance = Vector3.Distance(impulseInitPosition, impulseEndPosition);

            // Calculate impulseForce acording to curve
            impulseForce = impulseCurve.Evaluate(impulseDistance);
             // Aply impulse force
            rb.AddForce(transform.forward * impulseForce, ForceMode.Acceleration);

            indicator.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, hit.point);
    }
}

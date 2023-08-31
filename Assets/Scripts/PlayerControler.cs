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
        // Cast a ray from the mouse cursor into the world
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && isImpulsing == false)
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

        if (Input.GetMouseButtonDown(0)) 
        {
            isImpulsing = true;

            // Get the mouse start position to calculate impulse force
            impulseInitPosition = hit.point;

            
        }

        if (Input.GetMouseButton(0))
        {
            // Calculate distance between mouse inputs
            float indicatorDistance = Vector3.Distance(impulseInitPosition, hit.point);
            float indicatorSize = indicatorCurve.Evaluate(indicatorDistance);
            Debug.Log("indicator distance: " + indicatorDistance);
            if (indicatorDistance > 1f)
            {
                indicator.localScale = new Vector3(1.0f, 1.0f, indicatorSize);
            }

            //Debug.Log("impulsing'");
            
        }
        

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

            Debug.Log("Impulse force: " + impulseForce);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, hit.point);
    }
}

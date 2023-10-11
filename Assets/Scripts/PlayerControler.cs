using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float impulseForce;
    [SerializeField] private Transform indicator;
    [field: SerializeField] public Transform ThisPlayerSpawnPoint { get; private set; }
    private LayerMask groundLayerMask;
    [SerializeField] private AnimationCurve impulseCurve;
    [SerializeField] private AnimationCurve indicatorCurve;
    private Rigidbody rb;
    private bool isImpulsing;
    
    private float impulseDistance;
    private Vector3 impulseInitPosition;
    private RaycastHit hit;

    // Multiplayer Variables
    private GameManager gameManager; // Reference to the GameManager
    public int playerNumber; // 1 for player 1, 2 for player 2
    public bool isCurrentPlayerTurn;
    public bool isTurnEnded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        isTurnEnded = false;
        isCurrentPlayerTurn = playerNumber == 1; // Player 1 starts first
    }

    void Update()
    {
        if (!isCurrentPlayerTurn)
            return; // Skip input if it's not the current player's turn

        if (!isTurnEnded)
        {
            groundLayerMask = 1 << LayerMask.NameToLayer("Ground");

            CalculateImpulseDirection();
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
                isTurnEnded = true;

                // Get the mouse end position to calculate impulse force
                Vector3 impulseEndPosition = hit.point;
                // Calculate distance between mouse inputs
                impulseDistance = Vector3.Distance(impulseInitPosition, impulseEndPosition);

                // Calculate impulseForce acording to curve
                impulseForce = impulseCurve.Evaluate(impulseDistance);
                 // Aply impulse force
                rb.AddForce(transform.forward * impulseForce, ForceMode.Acceleration);

                indicator.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                // Switch turns
                StartCoroutine(DelayedTurnSwitch());

            }
        }
        
    }

    private void CalculateImpulseDirection()
    {
         // Cast a ray from the mouse cursor into the world
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Debug.Log("Player " + playerNumber + " Update");
        //RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayerMask))
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
    }

    private IEnumerator DelayedTurnSwitch()
    {
        yield return new WaitForSeconds(3f);

        // Switch turns after the delay
        gameManager.SwitchTurn();
    }

}

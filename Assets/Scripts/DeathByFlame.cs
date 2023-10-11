using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathByFlame : MonoBehaviour
{
    private PlayerControler playerRef;
    private Rigidbody rb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControler>())
        { 
            playerRef = other.GetComponent<PlayerControler>();
            rb = playerRef.GetComponent<Rigidbody>();

            playerRef.transform.position = playerRef.ThisPlayerSpawnPoint.position;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}

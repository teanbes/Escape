using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerControler>())
        {
            Debug.Log("you win");
            if (other.GetComponent <PlayerControler>().GetComponent<Rigidbody>().velocity.magnitude <= 0.1f) 
            { 
                winPanel.SetActive(true);
                Time.timeScale = 0;

            }

        }
    }
}

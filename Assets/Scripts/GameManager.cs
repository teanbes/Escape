using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    
    public PlayerControler player1;
    public PlayerControler player2;

    public GameObject player1Camera;
    public GameObject player2Camera;

    private void Start()
    {
        // Set the initial active camera to follow Player 1
        player1Camera.SetActive(true);
        player2Camera.SetActive(false);

        player1.isCurrentPlayerTurn = true; // Player 1 starts first
        player2.isCurrentPlayerTurn = false;
    }

    public void SwitchTurn()
    {
        if (player1.isCurrentPlayerTurn)
        {
            // Switch to Player 2's camera
            player1Camera.SetActive(false);
            player2Camera.SetActive(true);
            player1.isCurrentPlayerTurn = false;
            player2.isCurrentPlayerTurn = true;
        }
        else
        {
            // Switch to Player 1's camera
            player2Camera.SetActive(false);
            player1Camera.SetActive(true);
            player1.isCurrentPlayerTurn = true;
            player2.isCurrentPlayerTurn = false;
        }
    }
}

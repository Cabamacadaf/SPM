﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchTrigger : MonoBehaviour
{
    public GameObject teleportPosition;

  
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();


            PlayerWalkState PC = player.GetCurrentState() as PlayerWalkState;
            if (PC != null)
            {
                player.transform.position = teleportPosition.transform.position;
                player.Transition<PlayerCrouchState>();

            }
            else
            {
                player.transform.position = teleportPosition.transform.position;
                player.Transition<PlayerIdleState>();
            }

        }
    }

}
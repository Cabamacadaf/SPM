using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchTrigger : InteractiveObject
{
    public GameObject teleportPosition;

    Player player;

    private void Awake ()
    {
        player = GameManager.PlayerInstance;
    }
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.gameObject.CompareTag("Player"))
        {
            //PlayerIdleState PIS = player.GetCurrentState() as PlayerIdleState;
            //if (PIS != null)
            //{
            //    player.Transition<PlayerCrouchState>();

            //}
            //else
            //{
            //    player.Transition<PlayerIdleState>();
            //}
            player.transform.position = teleportPosition.transform.position;

        }
    }

}

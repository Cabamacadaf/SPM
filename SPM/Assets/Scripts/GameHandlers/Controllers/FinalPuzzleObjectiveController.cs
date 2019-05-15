using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzleObjectiveController : MonoBehaviour
{
    private bool active = true;

    private void OnTriggerEnter(Collider other)
    {
        if (active && other.CompareTag("PuzzleObject"))
        {
            active = false;
            TransformObject(other);
            GameController.Instance.AddLastPuzzle();

        }
    }

    private void TransformObject(Collider other)
    {
        other.transform.position = transform.position;
        other.transform.parent = transform;
        other.transform.rotation = Quaternion.identity;
        other.gameObject.layer = 0;
        Destroy(other.GetComponent<Rigidbody>());
    }
}

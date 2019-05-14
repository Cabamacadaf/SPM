using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzelQuest : MonoBehaviour
{
    //[SerializeField] private Light[] lights;
    //[SerializeField] private Color newColor;
    //[SerializeField] private float timeBetweenLights;

    private bool active = true;

    private void OnTriggerEnter(Collider other)
    {
        if (active && other.CompareTag("PuzzleObject"))
        {
            active = false;
            other.transform.position = transform.position;
            other.transform.parent = transform;
            other.transform.rotation = Quaternion.identity;
            other.gameObject.layer = 0;
            Destroy(other.GetComponent<Rigidbody>());
            GameController_2.gameControllerInstance_2.greenCollecting++;
            //StartCoroutine(ChangeColors());

            //objectB = other.transform.gameObject;
            //objectB.transform.position = objectA.transform.position;
            //objectB.transform.parent = objectA.transform;
            //GameController.gameControllerInstance.powerCoreCollection++;
            //GameObject.Find("Objective_Power_Core").GetComponent<PickUpObject>().enabled = false;
            ////stäng av Rigided body
            //objectB.gameObject.SetActive(false);
            ////byt ljusen till grönt!
            //lt.color = (color1 / 2.0f) * Time.deltaTime;
        }
    }
    //private IEnumerator ChangeColors()
    //{
    //    foreach (Light light in lights)
    //    {
    //        light.color = newColor;
    //        yield return new WaitForSeconds(timeBetweenLights);
    //    }
    //}
}

//Main Author: Marcus Mellström
//Secondary Author: Simon Sundström

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GravityGun : StateMachine
{
    public float pullRange = 12.0f;
    public float pushForce = 900f;
    public float pullForce = 12.0f;
    public float distanceToGrab = 0.1f;
    public float objectRotationSpeed = 2.0f;

    public LayerMask raycastCollideLayer;
    public LayerMask pullLayer;

    public Image crosshair;

    public Transform pullPoint;
    [HideInInspector] public PickUpObject holdingObject;
    
    //Power up
    [SerializeField] private float powerUpLength;
    [SerializeField] private float powerUpIncreaseRange;

    protected override void Awake ()
    {
        base.Awake();
    }

    private IEnumerator PowerDownRoutine ()
    {
        yield return new WaitForSeconds(powerUpLength);
        
        pullRange -= powerUpIncreaseRange;
    }

    public void PowerUp ()
    {
        pullRange += powerUpIncreaseRange;
        StartCoroutine(PowerDownRoutine());
    }
}

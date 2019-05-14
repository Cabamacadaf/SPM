//Main Author: Marcus Mellström
//Secondary Author: Simon Sundström

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GravityGun : StateMachine
{
    public float pushRange;
    public float pullRange;
    public float pushForce;
    public float pullForce;
    public float cameraOffset;

    public LayerMask hitLayer;

    public Image crosshair;

    [HideInInspector] public PickUpObject holdingObject;
    
    //Power up
    [SerializeField] private float powerUpLength;
    [SerializeField] private float powerUpIncreaseRange;
    
    protected override void Awake()
    {
        base.Awake();
    }

    private IEnumerator PowerDownRoutine()
    {
        yield return new WaitForSeconds(powerUpLength);

        pushRange -= powerUpIncreaseRange;
        pullRange -= powerUpIncreaseRange;
    }

    public void PowerUp()
    {
        pushRange += powerUpIncreaseRange;
        pullRange += powerUpIncreaseRange;
        StartCoroutine(PowerDownRoutine());
    }
}

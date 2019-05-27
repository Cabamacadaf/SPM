//Author: Marcus Mellström

using UnityEngine;

public class BlastRadius : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask wallLayer;

    private GravityBlast gravityBlast;

    private float timer = 0.0f;
    private float activeTime = 0.5f;

    private void Awake ()
    {
        gravityBlast = GetComponentInParent<GravityBlast>();
    }

    private void Update ()
    {
        if (isActiveAndEnabled) {
            timer += Time.deltaTime;
            if(timer >= activeTime) {
                timer = 0.0f;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Enemy")) {
            Debug.Log("Gravity Blast Hit");
            Vector3 raycastDirection = firePoint.position - other.transform.position;
            if (!Physics.Raycast(other.transform.position, raycastDirection.normalized, out RaycastHit hit, raycastDirection.magnitude, wallLayer)) {
                Enemy enemy = other.GetComponentInParent<Enemy>();
                enemy.Transition<EnemyKnockbackState>();
                EnemyKnockbackState knockbackState = (EnemyKnockbackState)enemy.GetCurrentState();
                knockbackState.KnockBack(gravityBlast.BlastForce, enemy.BlastRecoveryTime);
            }
        }
    }
}

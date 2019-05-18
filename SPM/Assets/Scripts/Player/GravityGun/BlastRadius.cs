//Author: Marcus Mellström

using UnityEngine;

public class BlastRadius : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask wallLayer;

    private GravityBlast gravityBlast;

    private void Awake ()
    {
        gravityBlast = GetComponentInParent<GravityBlast>();
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Enemy")) {
            Vector3 raycastDirection = firePoint.position - other.transform.position;
            if (!Physics.Raycast(other.transform.position, raycastDirection.normalized, out RaycastHit hit, raycastDirection.magnitude, wallLayer)) {
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.Transition<EnemyBlastedState>();
                enemy.RigidBody.AddForce((enemy.transform.position - enemy.Player.transform.position).normalized * gravityBlast.BlastForce);
            }
        }
    }
}

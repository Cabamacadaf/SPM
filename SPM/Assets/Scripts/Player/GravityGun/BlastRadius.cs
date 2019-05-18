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
                Debug.DrawLine(enemy.Collider.bounds.center, enemy.Player.transform.position, Color.green, 5.0f);
                Debug.Log("Force: " + (enemy.Collider.bounds.center - enemy.Player.transform.position).normalized * gravityBlast.BlastForce);
                enemy.RigidBody.AddForce((enemy.Collider.bounds.center - enemy.Player.transform.position).normalized * gravityBlast.BlastForce);
            }
        }
    }
}

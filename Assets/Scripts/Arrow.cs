using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform arrowRotationPoint;

    [Header("Attributes")]
    [SerializeField] private float speed = 10f;
    [SerializeField] public float damage = 20f; // Damage inflicted by the arrow
    [SerializeField] private bool isAreaDamage = false; // Is the arrow an area damage projectile?
    [SerializeField] private float explosionRadius = 2f; // Radius of the explosion

    private Transform target;
    
    public void SetTarget(Transform _target) {
        target = _target;
    }

    private void FixedUpdate() {
        if (!target) {
            Destroy(gameObject); // Destroy the arrow if the target no longer exists
            return;
        }

        // Calculate the direction to the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Calculate the rotation to make the arrow perpendicular to the movement direction
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);

        // Apply a -90 degree offset to the rotation
        rotation *= Quaternion.Euler(0, 0, -90);

        // Apply the rotation to the arrow rotation point
        arrowRotationPoint.rotation = rotation;

        // Apply velocity in the direction of the target
        rb.velocity = direction * speed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public void Explode()
    {
        if (!isAreaDamage)
        {
            Collider2D collider = GetComponentInChildren<Collider2D>(); // Busca o collider filho do prefab da flecha
            if (collider != null)
            {
                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
        }

        // Find all colliders within the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                // Apply damage to enemies within the explosion radius
                EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }

}

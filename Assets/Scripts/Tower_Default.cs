using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tower_Default : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float fireRate = 2f;

    [SerializeField] private AudioSource shootSound;

    public GameObject prefab; // Adicionado para facilitar a instanciação

    private Transform target;
    private float timeUntilFire;

    private void Update() {
        if (target == null) {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange()){
            target = null;
        } else {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / fireRate) {
                Fire();
                timeUntilFire = 0f;
            }
        }
    }

    private void Fire() {
        GameObject projectileObj = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
        Arrow arrowScript = projectileObj.GetComponent<Arrow>();
        arrowScript.SetTarget(target);
        shootSound.Play();
    }

    private void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2) transform.position, 0f, enemyMask);
        
        if (hits.Length > 0) {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange() {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget() {
        // Calculate the direction to the target
        Vector3 direction = target.position - transform.position;

        // Determine the direction (right or left) based on the x component of the direction vector
        float directionSign = Mathf.Sign(direction.x);

        // Set the scale of the turretRotationPoint to mirror it
        turretRotationPoint.localScale = new Vector3(directionSign, 1f, 1f);
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
    #endif

    private void OnMouseDown() {
        // Chamada para destruir a torre
        DestroyTower();
    }

    private void DestroyTower() {
        // Implemente a lógica para destruir a torre aqui
        Destroy(gameObject);
        Debug.Log("Torre destruída.");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("house")) {
            BuildManager.main.EnemyReachedBase();
            Destroy(gameObject);
        }

        if (other.CompareTag("projectile")) {
            EnemyHealth enemyHealth = gameObject.GetComponent<EnemyHealth>();
            Arrow arrow = other.gameObject.GetComponent<Arrow>();
            arrow.Explode();

        }
    }

}


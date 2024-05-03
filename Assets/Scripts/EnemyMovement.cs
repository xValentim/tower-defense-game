using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("Attributes")]
    [SerializeField]
    private float moveSpeed = 1f;

    private Waypoint target;

    private void Start()
    {
        target = FindObjectOfType<LevelManager>().startPoint.GetComponent<Waypoint>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (target != null && Vector2.Distance(transform.position, target.transform.position) <= 0.9f)
        {
            if (target.nextWaypoints.Count == 0)
            {
                // LevelManager.main.IncreaseCoins(50); // Debugging - Simulando a adição de moedas ao jogador
                Destroy(gameObject);
                return;
            }
            target = target.nextWaypoints[Random.Range(0, target.nextWaypoints.Count)];
        }

        AnimateMovement();
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (target.transform.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
            AdjustSpriteDirection(direction);
        }
    }

    private void AnimateMovement()
    {
        // if (rb.velocity.magnitude > 0.1f)
        // {
        //     animator.SetBool("IsRunning", true);
        // }
        // else
        // {
        //     animator.SetBool("IsRunning", false);
        // }
    }

    private void AdjustSpriteDirection(Vector2 direction)
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}

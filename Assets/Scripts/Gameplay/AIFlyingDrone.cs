using UnityEngine;

using UnityEngine;

public class AIFlyingDrone : MonoBehaviour
{
    private enum State
    {
        Idle,
        Patrol,
        Attack,
        Death
    }

    [SerializeField] private State currentState;

    public Transform target;
    public float detectionRange = 10f;
    public float attackRange = 5f;
    public float speed = 3f;
    public float rotationSpeed = 2f;
    public float attackCooldown = 2f;
    private float attackTimer;

    public Vector3 islandCenter;
    public float islandRadius = 20f;
    public float flyAwayRadius = 15f; // Radius for flying away from spawn point
    public Vector3 spawnPoint; // The spawning point of the drone

    private Vector3 currentPatrolPoint;

    private float hoverAmplitude = 0.5f;
    private float hoverFrequency = 1f;
    private Vector3 initialPosition;

    private LineRenderer lineRenderer;

    public float health = 100f;
    public float fallSpeed = 2f;

    public GameObject explosionPrefab; // Prefab for explosion effect

    public float dtShot = 1;
    public float damageProj = 10;
    public ParticleSystem shotParticle;
    public Transform firePoint;
    public Projectil projectil;

    void Start()
    {
        currentState = State.Idle;
        attackTimer = attackCooldown;
        initialPosition = transform.position;
        spawnPoint = transform.position; // Assuming spawn point is the initial position

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Patrol:
                Patrol();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Death:
                Death();
                break;
        }

        target = FindNearestEnemy();

        UpdateLineRenderer();
    }

    private void Idle()
    {
        Hover();

        Transform nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null && Vector3.Distance(transform.position, nearestEnemy.position) <= detectionRange)
        {
            target = nearestEnemy;
            currentState = State.Patrol;
        }
    }

    private void Patrol()
    {
        if (target == null)
        {
            currentState = State.Idle;
            return;
        }

        RotateTowards(target.position);
        MoveTowards(target.position);

        if (Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            currentState = State.Attack;
        }
        else if (Vector3.Distance(transform.position, target.position) > detectionRange)
        {
            currentState = State.Idle;
        }
    }

    private void Attack()
    {
        if (target == null)
        {
            currentState = State.Idle;
            return;
        }

        RotateTowards(target.position);
        if (Vector3.Distance(transform.position, target.position) > attackRange)
        {
            MoveTowards(target.position);
        }

        if (Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                PerformAttack();
                attackTimer = attackCooldown;
            }
        }
        else if (Vector3.Distance(transform.position, target.position) > attackRange && Vector3.Distance(transform.position, target.position) <= detectionRange)
        {
            currentState = State.Patrol;
        }
        else if (Vector3.Distance(transform.position, target.position) > detectionRange)
        {
            currentState = State.Idle;
        }
    }

    private void Death()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y <= 0)
        {
            Explode(); // Call the method to instantiate explosion prefab
            Destroy(gameObject); // Destroy the drone
        }
    }

    private void Explode()
    {
        // Instantiate explosion prefab at the drone's position
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        // Make the explosion prefab a child of the drone
        explosion.transform.parent = transform;
        // Destroy the explosion prefab after 3 seconds
        Destroy(explosion, 3f);
    }

    private void MoveTowards(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void PerformAttack()
    {
        Debug.Log("Drone attacks the target!");
        // Implement actual attack logic here (e.g., reducing target health)
        Shot();
    }

    protected virtual void Shot()
    {
        Projectil p = Instantiate(projectil, firePoint.position, firePoint.rotation);
        p.damage = damageProj;

        ParticleSystem sp = Instantiate(shotParticle, firePoint.position, firePoint.rotation);
        Destroy(sp.gameObject, 1);
    }

    protected Enemy SearchEnemy(Transform body)
    {
        Transform parent = body.parent;
        if (parent)
        {
            Enemy enemy = parent.GetComponent<Enemy>();
            if (enemy) return enemy;
        }
        return null;
    }

    private void SetRandomPatrolPoint()
    {
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);
        float x = islandCenter.x + islandRadius * Mathf.Cos(randomAngle);
        float z = islandCenter.z + islandRadius * Mathf.Sin(randomAngle);
        currentPatrolPoint = new Vector3(x, transform.position.y, z);
    }

    private void Hover()
    {
        float hoverOffset = Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude;
        transform.position = new Vector3(transform.position.x, initialPosition.y + hoverOffset, transform.position.z);
    }

    private void UpdateLineRenderer()
    {
        if (target != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, target.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            currentState = State.Death;
        }
    }
}


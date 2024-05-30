using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighteningGunTurret : Tower
{
    

    private enum State
    {
        Idle,
        Patrol,
        Attack,
        Death
    }

    [Header("Drone State")]
    [SerializeField] private State currentState;

    [Space]
    [Space]
    [Header("Force Field")]
    public ForceField forceField;
    public AudioSource fieldFieldSound;


    [Space]
    [Space]
    [Header("Target Info")]
    public Transform target;
    public float detectionRange = 10f;
    public float attackRange = 5f;
    public float speed = 3f;
    public float rotationSpeed = 2f;
    public float attackCooldown = 2f;
    private float attackTimer;

    [Header(" Island Health and Fall Speed")]
    public float health = 100f;
    public float fallSpeed = 2f;

    [Space]
    [Space]
    [Header("Island Explosion Particle Effect")]
    public GameObject explosionPrefab;

    [Space]
    [Space]
    [Header("Island Center and Radius")]
    public Vector3 islandCenter;
    public float islandRadius = 20f;
    private Vector3 currentPatrolPoint;

    [Space]
    [Space]
    [Header("Drone Hover Functionality")]
    private float hoverAmplitude = 0.5f;
    private float hoverFrequency = 1f;
    private Vector3 initialPosition;

    [Space]
    [Space]
    [Header("Projectile Type and Settings")]
    public float dtShot = 1;
    public float damageProj = 10;
    public float ray = 0.5f;
    //protected float t_nextshot;

    public ParticleSystem shotParticle;
    public Transform firePoint;
    public Projectil projectil;
    private float t_nextShot;

    void Start()
    {
        currentState = State.Idle;
        attackTimer = attackCooldown;
        SetRandomPatrolPoint();
        initialPosition = transform.position;
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
    }

    private void Idle()
    {
        // Hover();

        Transform nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null && Vector3.Distance(transform.position, nearestEnemy.position) <= detectionRange)
        {
            target = nearestEnemy;
            currentState = State.Patrol;
        }
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

    private void Patrol()
    {
        MoveTowards(currentPatrolPoint);

        if (Vector3.Distance(transform.position, currentPatrolPoint) <= 0.2f)
        {
            SetRandomPatrolPoint();
        }

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

    public void Death()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y <= 0)
        {
            Explode(); // Call the method to instantiate explosion prefab
            Destroy(gameObject); // Destroy the drone
        }
    }

    void Explode()
    {
        // Instantiate explosion prefab at the drone's position
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        // Make the explosion prefab a child of the drone
        explosion.transform.parent = transform;
        // Destroy the explosion prefab after 3 seconds
        Destroy(explosion, 3f);
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void MoveTowards(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void PerformAttack()
    {
        Debug.Log("Drone attacks the target!");
        Shot();
    }

    protected virtual void Shot()
    {
        //Projectil p = Instantiate(projectil, firePoint.position, firePoint.rotation);
        //p.damage = damageProj;

        //ParticleSystem sp = Instantiate(shotParticle, firePoint.position, firePoint.rotation);
        //Destroy(sp.gameObject, 1);

        Enemy target;
        float dist;
        (target, dist) = Enemy.GetClosest(transform.position);

        if(target && dist < ray)
        {
            if (Time.time > t_nextShot)
            {
                t_nextShot = Time.time + dtShot;
                ForceField ff = Instantiate(forceField, firePoint.position, Quaternion.identity);
                ff.damage = damageProj;

            }
        }
    }

    private void SetRandomPatrolPoint()
    {
        float randomAngle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
        float x = islandCenter.x + islandRadius * Mathf.Cos(randomAngle);
        float z = islandCenter.z + islandRadius * Mathf.Sin(randomAngle);
        currentPatrolPoint = new Vector3(x, transform.position.y, z);
    }

    protected override void UpdateStats()
    {
        damageProj *= 1.08f;
        dtShot /= 1.06f;
        ray *= 1.04f;
    }

    protected override void UpStats()
    {
        stats = "Damage : " + ((int)damageProj) + " -> " + ((int)(damageProj * 1.08)) +
              "\nFreq : " + Math.Round(1f / dtShot, 1) + " /s -> " + Math.Round(1.06f / dtShot, 1) +
              "\nRay : " + Math.Round(ray, 1) + "m -> " + Math.Round(ray * 1.04, 1);
    }
}

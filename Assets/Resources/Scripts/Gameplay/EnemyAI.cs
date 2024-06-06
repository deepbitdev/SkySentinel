using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform laserSpawnPoint;
    public float laserSpeed = 20f;
    public float fireRate = 1f;
    public float detectionRange = 30f;
    private float nextFireTime;

    private Transform currentTarget;
    private List<Transform> potentialTargets;

    void Start()
    {
        potentialTargets = new List<Transform>();
        InvokeRepeating("ScanForTargets", 0f, 1f);
    }

    void Update()
    {
        if(currentTarget != null)
        {
            laserSpawnPoint.LookAt(currentTarget);

            if(Time.time > nextFireTime)
            {
                ShootLaser();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void ScanForTargets()
    {
        potentialTargets.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);

        foreach(var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Target"))
            {
                potentialTargets.Add(hitCollider.transform);
            }
        }
        SelectClosestTarget();
    }

    private void SelectClosestTarget()
    {
        float closetDistance = Mathf.Infinity;
        Transform closetTarget = null;

        foreach(var target in potentialTargets)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if(distanceToTarget < closetDistance)
            {
                closetDistance = distanceToTarget;
                closetTarget = target;
            }
        }

        currentTarget = closetTarget;
    }

    private void ShootLaser()
    {
        if(currentTarget != null)
        {
            GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
            Rigidbody rb = laser.GetComponent<Rigidbody>();
            //rb.velocity = (currentTarget.position - laserSpawnPoint.position).normalized * laserSpeed;
            rb.velocity = laserSpawnPoint.forward * laserSpeed;
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AIDroneController : MonoBehaviour
{
    public static AIDroneController instance;

    [Header("Flight Settings")]
    public float flightSpeed = 5f;
    public float slowdownDistance = 15f;
    public float stopDistance = 4f;
    public float detectionRadius;
    public float wanderRadius = 30f;
    public float wanderChangeInterval = 3f;
    public float minHeight = 5f;
    public float maxHeight = 20f;
    public float rotationSpeed = 5f;
    public bool restrictToYawOnly = true;

    [Header("Layer Masks")]
    public LayerMask enemyLayer;
    public LayerMask islandLayerMask;
    public LayerMask droneLayer;

    [Header("Avoidance Settings")]
    public float avoidanceRadius = 5f;
    public float avoidanceStrength = 5f;

    public bool isFlying = true;

    private Rigidbody rb;
    private Transform targetEnemy;
    private Vector3 islandCenter;
    private Vector3 wanderTarget;
    private float wanderTimer;

    void Start()
    {
        instance = this;

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        islandCenter = FindClosestIslandCenter();
        SetNewWanderTarget();
        wanderTimer = wanderChangeInterval;
    }

    void FixedUpdate()
    {
        if (!isFlying)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        DetectEnemy();
        Vector3 avoidanceVector = CalculateAvoidanceVector();

        if (targetEnemy != null)
        {
            float distance = Vector3.Distance(transform.position, targetEnemy.position);

            if (distance <= stopDistance)
            {
                rb.linearVelocity = Vector3.zero;
            }
            else
            {
                float speedFactor = (distance <= slowdownDistance)
                    ? Mathf.InverseLerp(stopDistance, slowdownDistance, distance)
                    : 1f;

                Vector3 direction = (targetEnemy.position - transform.position).normalized;
                Vector3 combinedDirection = (direction + avoidanceVector).normalized;
                rb.linearVelocity = combinedDirection * flightSpeed * speedFactor;
                RotateTowards(combinedDirection);
            }
        }
        else
        {
            wanderTimer -= Time.fixedDeltaTime;
            if (wanderTimer <= 0f || Vector3.Distance(transform.position, wanderTarget) < 2f)
            {
                SetNewWanderTarget();
                wanderTimer = wanderChangeInterval;
            }

            Vector3 direction = (wanderTarget - transform.position).normalized;
            Vector3 combinedDirection = (direction + avoidanceVector).normalized;
            rb.linearVelocity = combinedDirection * flightSpeed;
            RotateTowards(combinedDirection);
        }

        ClampHeight();
    }

    void DetectEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        if (hits.Length > 0)
        {
            Transform closest = hits[0].transform;
            float minDistance = Vector3.Distance(transform.position, closest.position);

            foreach (Collider hit in hits)
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < minDistance)
                {
                    closest = hit.transform;
                    minDistance = distance;
                }
            }

            targetEnemy = closest;
        }
        else
        {
            targetEnemy = null;
        }
    }

    Vector3 FindClosestIslandCenter()
    {
        Collider[] islands = Physics.OverlapSphere(transform.position, 100f, islandLayerMask);
        if (islands.Length == 0)
        {
            Debug.LogWarning("No island found in range. Defaulting to current position.");
            return transform.position;
        }

        Transform closest = islands[0].transform;
        float minDistance = Vector3.Distance(transform.position, closest.position);

        foreach (Collider island in islands)
        {
            float distance = Vector3.Distance(transform.position, island.transform.position);
            if (distance < minDistance)
            {
                closest = island.transform;
                minDistance = distance;
            }
        }

        return closest.position;
    }

    void SetNewWanderTarget()
    {
        Vector2 randomCircle = Random.insideUnitCircle * wanderRadius;
        float randomHeight = Random.Range(minHeight, maxHeight);
        wanderTarget = islandCenter + new Vector3(randomCircle.x, randomHeight, randomCircle.y);
    }

    void RotateTowards(Vector3 direction)
    {
        Quaternion targetRotation;

        if (restrictToYawOnly)
        {
            direction.y = 0;
            if (direction.sqrMagnitude < 0.01f) return;
            targetRotation = Quaternion.LookRotation(direction);
        }
        else
        {
            targetRotation = Quaternion.LookRotation(direction.normalized);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    Vector3 CalculateAvoidanceVector()
    {
        Collider[] nearbyDrones = Physics.OverlapSphere(transform.position, avoidanceRadius, droneLayer);
        Vector3 avoidance = Vector3.zero;

        foreach (Collider drone in nearbyDrones)
        {
            if (drone.transform != this.transform)
            {
                Vector3 away = transform.position - drone.transform.position;
                float distance = away.magnitude;
                if (distance > 0)
                {
                    avoidance += away.normalized / distance;
                }
            }
        }

        return avoidance * avoidanceStrength;
    }

    void ClampHeight()
    {
        Vector3 clampedPosition = rb.position;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minHeight, maxHeight);
        rb.position = clampedPosition;
    }

    public void StopFlying()
    {
        isFlying = false;
        rb.linearVelocity = Vector3.zero;
    }

    public void StartFlying()
    {
        isFlying = true;
    }

    public void IncreaseDetectionRadius(float amount)
    {
        detectionRadius += amount;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(islandCenter, wanderRadius);
    }
}

using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform target;

    public float speed;

    void Update()
    {
        transform.RotateAround(target.transform.position, target.transform.up, 100 * speed * Time.deltaTime);
    }
}

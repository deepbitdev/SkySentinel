using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyAlertMan))]
public class EnemyAlertViewEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyAlertMan fov = (EnemyAlertMan)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);
        Vector3 viewAngleA = DirFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngleB = DirFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.radius);

        Handles.color = Color.green;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.radius);

        //foreach(Transform visibleTarget in fov.visibleTargets)
        //{
        //    Handles.DrawLine(fov.transform.position, visibleTarget.position);
        //}

        foreach(GameObject enemy in fov.enemyRef)
        {
            Handles.DrawLine(fov.transform.position, enemy.transform.position);
        }

        //if(fov.inRange)
        //{
        //    Handles.color = Color.red;
        //    Handles.DrawLine(fov.transform.position, fov.enemyRef.transform.position);
        //}
    }

    private Vector3 DirFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}

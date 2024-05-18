using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUI_Controller : MonoBehaviour
{
    // Referencing wrist bone of the tracked hand
    public Transform wristBoneTransform;

    public GameObject objToActivate;

    public float activationAngleThreshold = 45.0f;

    private void Start()
    {
        objToActivate.SetActive(false);
    }

    private void Update()
    {
        if(wristBoneTransform == null || objToActivate == null) return;

        Vector3 directionToCamera = (Camera.main.transform.position - wristBoneTransform.transform.position).normalized;

        Vector3 palmNormal = wristBoneTransform.forward;

        float angleToCamera = Vector3.Angle(palmNormal, directionToCamera);

        if(angleToCamera <= activationAngleThreshold)
        {
            objToActivate.SetActive(true);
        }
        else
        {
            objToActivate.SetActive(false);
        }
    }
}

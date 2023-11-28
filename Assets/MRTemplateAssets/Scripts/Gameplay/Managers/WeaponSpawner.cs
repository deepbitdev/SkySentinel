using System;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Utilities;
using UnityEngine.XR;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponSpawner : MonoBehaviour
{
    #region New Functionality with button list
    public GameObject[] objectsToSpawn;
    public ActionBasedController xrController;
    public GameObject followingObjectPrefab;

    private GameObject followingObject;
    private bool canSpawn = true;
    private int currentObjectIndex = 0;

    void Start()
    {
        if (objectsToSpawn == null || objectsToSpawn.Length == 0)
        {
            Debug.LogError("Please assign objects to spawn in the inspector.");
        }

        if (xrController == null)
        {
            Debug.LogError("Please assign an ActionBasedController to the inspector.");
        }

        if (followingObjectPrefab == null)
        {
            Debug.LogError("Please assign a following object prefab to the inspector.");
        }

        SpawnFollowingObject();
    }

    void Update()
    {
        // Check if objects can spawn
        if (!canSpawn)
        {
            return;
        }
        

        // Check if the trigger button is pressed and canSpawn flag is true
        if (canSpawn && xrController.activateAction.action.ReadValue<float>() > 0.5f)
        {
            SpawnObject();
            canSpawn = false; // Set flag to prevent rapid spawning, adjust as needed
        }
        else if (xrController.activateAction.action.ReadValue<float>() <= 0.5f && !canSpawn)
        {
            canSpawn = true; // Reset the flag when the trigger is released
        }

        UpdateFollowingObjectPosition();
    }

    void SpawnObject()
    {
        // Spawn the object at the XRController's position and rotation
        if (objectsToSpawn.Length > 0)
        {
            GameObject newObject = Instantiate(objectsToSpawn[currentObjectIndex], xrController.transform.position, xrController.transform.rotation);

            // Access the TowerBase component on the spawned object
            TowerBase towerBase = newObject.GetComponent<TowerBase>();
            if (towerBase != null)
            {
                towerBase.m_hologram.Dissolve();
            }

            // Grab Event Trigger
            GrabMan.inst.grabActiveEvent.Invoke();
        }
    }

    public void SwitchObjectToSpawn(int newIndex)
    {
        // Set the currentObjectIndex directly from the UI button script
        if (newIndex >= 0 && newIndex < objectsToSpawn.Length)
        {
            currentObjectIndex = newIndex;
            Debug.Log("Switched to object index: " + currentObjectIndex);
        }
    }

    void SpawnFollowingObject()
    {
        // Spawn the following object at the XRController's position
        followingObject = Instantiate(followingObjectPrefab, xrController.transform.position, Quaternion.identity);
    }

    void UpdateFollowingObjectPosition()
    {
        // Update the position of the following object to match the XRController
        if (followingObject != null)
        {
            followingObject.transform.position = xrController.transform.position;
        }
    }

    public void StopSpawning()
    {
        canSpawn = false;
    }

    public void StartSpawning()
    {
        canSpawn = true;
    }
    #endregion
}

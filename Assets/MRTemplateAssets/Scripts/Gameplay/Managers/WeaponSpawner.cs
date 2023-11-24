using System;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Utilities;
using UnityEngine.XR;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponSpawner : MonoBehaviour
{
    #region
    //public GameObject[] objectsToSpawn; // List of game objects to spawn
    //private int currentObjectIndex = 0; // Index of the current object to spawn

    //// Reference to the UI button
    //public Button spawnButton;

    //void Start()
    //{
    //    // Ensure there is at least one object in the list
    //    if (objectsToSpawn.Length == 0)
    //    {
    //        Debug.LogError("No objects to spawn. Please assign objects to the 'objectsToSpawn' array in the inspector.");
    //    }

    //    // Attach the SpawnObject method to the button's onClick event
    //    if (spawnButton != null)
    //    {
    //        spawnButton.onClick.AddListener(SpawnObject);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No UI button assigned to 'spawnButton'. Please assign a button in the inspector.");
    //    }
    //}

    //void SpawnObject()
    //{
    //    // Ensure there are objects in the list
    //    if (objectsToSpawn.Length > 0)
    //    {
    //        // Spawn the object at the current index
    //        GameObject newObject = Instantiate(objectsToSpawn[currentObjectIndex], transform.position, Quaternion.identity);


    //        currentObjectIndex = (currentObjectIndex + 1) % objectsToSpawn.Length;

    //        TowerBase towerBase = newObject.GetComponent<TowerBase>();
    //        if(towerBase != null)
    //        {
    //            towerBase.m_hologram.Dissolve();
    //        }

    //        // Increment the index for the next spawn
    //        currentObjectIndex = (currentObjectIndex + 1) % objectsToSpawn.Length;
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No objects to spawn. Please assign objects to the 'objectsToSpawn' array in the inspector.");
    //    }
    //}
    #endregion


    #region
    //// List of game objects to spawn
    //public GameObject[] objectsToSpawn;

    //// List of buttons corresponding to each object in objectsToSpawn
    //public Button[] spawnButtons;

    //void Start()
    //{
    //    // Ensure there is at least one object in the list
    //    if (objectsToSpawn.Length == 0)
    //    {
    //        Debug.LogError("No objects to spawn. Please assign objects to the 'objectsToSpawn' array in the inspector.");
    //    }

    //    // Ensure the number of buttons matches the number of objects to spawn
    //    if (spawnButtons.Length != objectsToSpawn.Length)
    //    {
    //        Debug.LogError("The number of spawn buttons does not match the number of objects to spawn.");
    //    }

    //    //// Attach the SpawnObject method to each button's onClick event
    //    //for (int i = 0; i < spawnButtons.Length; i++)
    //    //{
    //    //    int index = i; // Create a local variable to capture the current value of i
    //    //    spawnButtons[i].onClick.AddListener(() => SpawnObject(index));
    //    //}
    //}

    //public void SpawnTurret()
    //{
    //    // Attach the SpawnObject method to each button's onClick event
    //    for (int i = 0; i < spawnButtons.Length; i++)
    //    {
    //        int index = i; // Create a local variable to capture the current value of i
    //        spawnButtons[i].onClick.AddListener(() => SpawnObject(0));
    //    }
    //}

    //public void SpawnFT()
    //{
    //    // Attach the SpawnObject method to each button's onClick event
    //    for (int i = 0; i < spawnButtons.Length; i++)
    //    {
    //        int index = i; // Create a local variable to capture the current value of i
    //        spawnButtons[i].onClick.AddListener(() => SpawnObject(1));
    //    }
    //}

    //public void SpawnLGun()
    //{
    //    // Attach the SpawnObject method to each button's onClick event
    //    for (int i = 0; i < spawnButtons.Length; i++)
    //    {
    //        int index = i; // Create a local variable to capture the current value of i
    //        spawnButtons[i].onClick.AddListener(() => SpawnObject(2));
    //    }
    //}

    //public void SpawnRailGun()
    //{
    //    // Attach the SpawnObject method to each button's onClick event
    //    for (int i = 0; i < spawnButtons.Length; i++)
    //    {
    //        int index = i; // Create a local variable to capture the current value of i
    //        spawnButtons[i].onClick.AddListener(() => SpawnObject(3));
    //    }
    //}

    //public void SpawnRocketLauncher()
    //{
    //    // Attach the SpawnObject method to each button's onClick event
    //    for (int i = 0; i < spawnButtons.Length; i++)
    //    {
    //        int index = i; // Create a local variable to capture the current value of i
    //        spawnButtons[i].onClick.AddListener(() => SpawnObject(4));
    //    }
    //}

    //void SpawnObject(int objectIndex)
    //{
    //    // Ensure the object index is within bounds
    //    if (objectIndex >= 0 && objectIndex < objectsToSpawn.Length)
    //    {
    //        // Spawn the object at the specified index
    //        GameObject newObject = Instantiate(objectsToSpawn[objectIndex], transform.position, Quaternion.identity);

    //        TowerBase towerBase = newObject.GetComponent<TowerBase>();
    //        if (towerBase != null)
    //        {
    //            towerBase.m_hologram.Dissolve();
    //        }

    //        // Optionally, you can perform additional actions or call functions on the spawned object here
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Invalid object index.");
    //    }
    //}
    #endregion

    #region
    //public GameObject objectToSpawn;
    //public ActionBasedController xrController;
    //public GameObject followingObjectPrefab;

    //private GameObject followingObject;
    //private bool canSpawn = true;

    //void Start()
    //{
    //    if (objectToSpawn == null)
    //    {
    //        Debug.LogError("Please assign an object to spawn in the inspector.");
    //    }

    //    if (xrController == null)
    //    {
    //        Debug.LogError("Please assign an ActionBasedController to the inspector.");
    //    }

    //    if (followingObjectPrefab == null)
    //    {
    //        Debug.LogError("Please assign a following object prefab to the inspector.");
    //    }

    //    SpawnFollowingObject();
    //}

    //void Update()
    //{
    //    bool triggerButtonPressed = false;

    //    // Check if the trigger button is pressed and canSpawn flag is true
    //    if (canSpawn && xrController.activateAction.action.ReadValue<float>() > 0.5f)
    //    {
    //        SpawnObject();
    //        canSpawn = false; // Set flag to prevent rapid spawning, adjust as needed
    //    }
    //    else if (xrController.activateAction.action.ReadValue<float>() <= 0.5f && !canSpawn)
    //    {
    //        canSpawn = true; // Reset the flag when the trigger is released
    //    }

    //    UpdateFollowingObjectPosition();
    //}

    //void SpawnObject()
    //{
    //    // Spawn the object at the XRController's position and rotation
    //    Instantiate(objectToSpawn, xrController.transform.position, xrController.transform.rotation);



    //    TowerBase towerBase = objectToSpawn.GetComponent<TowerBase>();
    //    if (towerBase != null)
    //    {
    //        towerBase.m_hologram.Dissolve();
    //    }
    //}

    //void SpawnFollowingObject()
    //{
    //    // Spawn the following object at the XRController's position
    //    followingObject = Instantiate(followingObjectPrefab, xrController.transform.position, Quaternion.identity);
    //}

    //void UpdateFollowingObjectPosition()
    //{
    //    // Update the position of the following object to match the XRController
    //    if (followingObject != null)
    //    {
    //        followingObject.transform.position = xrController.transform.position;
    //    }
    //}
    #endregion


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

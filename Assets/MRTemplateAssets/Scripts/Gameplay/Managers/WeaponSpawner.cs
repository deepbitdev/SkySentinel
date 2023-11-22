using System;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Utilities;
using UnityEngine.XR;
using UnityEngine;
using UnityEngine.UI;

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


    // List of game objects to spawn
    public GameObject[] objectsToSpawn;

    // List of buttons corresponding to each object in objectsToSpawn
    public Button[] spawnButtons;

    void Start()
    {
        // Ensure there is at least one object in the list
        if (objectsToSpawn.Length == 0)
        {
            Debug.LogError("No objects to spawn. Please assign objects to the 'objectsToSpawn' array in the inspector.");
        }

        // Ensure the number of buttons matches the number of objects to spawn
        if (spawnButtons.Length != objectsToSpawn.Length)
        {
            Debug.LogError("The number of spawn buttons does not match the number of objects to spawn.");
        }

        //// Attach the SpawnObject method to each button's onClick event
        //for (int i = 0; i < spawnButtons.Length; i++)
        //{
        //    int index = i; // Create a local variable to capture the current value of i
        //    spawnButtons[i].onClick.AddListener(() => SpawnObject(index));
        //}
    }

    public void SpawnTurret()
    {
        // Attach the SpawnObject method to each button's onClick event
        for (int i = 0; i < spawnButtons.Length; i++)
        {
            int index = i; // Create a local variable to capture the current value of i
            spawnButtons[i].onClick.AddListener(() => SpawnObject(1));
        }
    }

    void SpawnObject(int objectIndex)
    {
        // Ensure the object index is within bounds
        if (objectIndex >= 0 && objectIndex < objectsToSpawn.Length)
        {
            // Spawn the object at the specified index
            GameObject newObject = Instantiate(objectsToSpawn[objectIndex], transform.position, Quaternion.identity);

            TowerBase towerBase = newObject.GetComponent<TowerBase>();
            if (towerBase != null)
            {
                towerBase.m_hologram.Dissolve();
            }

            // Optionally, you can perform additional actions or call functions on the spawned object here
        }
        else
        {
            Debug.LogWarning("Invalid object index.");
        }
    }
}

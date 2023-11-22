using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class SpawnedWeaponManager : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown m_ObjectSelectorDropdown;

    [SerializeField]
    Button m_DestroyObjectsButton;

    WeaponSpawner m_Spawner;

    void OnEnable()
    {
        m_Spawner = GetComponent<WeaponSpawner>();
        //m_Spawner.spawnAsChildren = true;
        OnObjectSelectorDropdownValueChanged(m_ObjectSelectorDropdown.value);
        m_ObjectSelectorDropdown.onValueChanged.AddListener(OnObjectSelectorDropdownValueChanged);
        m_DestroyObjectsButton.onClick.AddListener(OnDestroyObjectsButtonClicked);
    }

    void OnDisable()
    {
        m_ObjectSelectorDropdown.onValueChanged.RemoveListener(OnObjectSelectorDropdownValueChanged);
        m_DestroyObjectsButton.onClick.RemoveListener(OnDestroyObjectsButtonClicked);
    }

    void OnObjectSelectorDropdownValueChanged(int value)
    {
        if (value == 0)
        {
            //m_Spawner.RandomizeSpawnOption();
            return;
        }

        //m_Spawner.spawnOptionIndex = value - 1;
    }

    void OnDestroyObjectsButtonClicked()
    {
        foreach (Transform child in m_Spawner.transform)
        {
            Destroy(child.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public static TowerBase instance;
    public Hologram m_hologram;

    public GameObject m_gameObject;

    void Awake()
    {
        instance = this;
    }

}

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class TowerShop : MonoBehaviour
{
    public Tower towerPrefab;
    public RawImage img;
    public TMP_Text nameCost;

    bool clicking = false;
    float t_click;

    bool locked;

    public XRNode inputSource = XRNode.RightHand;


    void Awake()
    {
        nameCost.text = towerPrefab.name + " " + towerPrefab.cost + "$";
    }


    void Update()
    {
        bool currentButtonState = false;
        InputDevices.GetDeviceAtXRNode(inputSource).TryGetFeatureValue(CommonUsages.triggerButton, out currentButtonState);

        
        if (Tool.Click(img.rectTransform)) {
            clicking = true;
            t_click = Time.time;
        }

        if(clicking && !currentButtonState)
        {
            clicking = false;
            if(!locked && Time.time - t_click < 0.2)
            {
                Tower tower = Instantiate(towerPrefab);
                tower.gameObject.SetActive(false);
                GrabMan.inst.Grab(tower);
            }
        }
        else if(!clicking && currentButtonState)
        {
            clicking = true;
            t_click = Time.time;
        }

        // Convert this to work with the object spawner script

        if (clicking && !Input.GetMouseButton(0)) {
           clicking = false;
           if (!locked && Time.time - t_click < 0.2) {
               Tower tower = Instantiate(towerPrefab);
               tower.gameObject.SetActive(false);
               GrabMan.inst.Grab(tower);
           }
        }

        // Implement a new Object Spawner script for tower shop
    }

    public void UpdateLock()
    {
        float a = 1;
        locked = false;

        if (Shop.inst.money < towerPrefab.cost) {
            a = 0.3f;
            locked = true;
        }
        Color color = Color.white;
        color.a = a;
        img.color = color;
        color = nameCost.color;
        color.a = a;
        nameCost.color = color;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    public UnityEvent openMenu;
    public UnityEvent closeMenu;


    // Start is called before the first frame update
    void Start()
    {
        Base.inst.gameObject.SetActive(false);
        openMenu.Invoke();
    }

    public void StartGame()
    {
        Base.inst.gameObject.SetActive(true);
        closeMenu.Invoke();
    }
}

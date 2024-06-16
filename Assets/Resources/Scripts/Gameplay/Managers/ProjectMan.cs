using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using UnityEngine.XR;


public class ProjectMan : MonoBehaviour
{
    public static ProjectMan inst;

    // Smooth Translate
    public float smoothTranslate_t;
    public AnimationCurve smoothTranslate_curve;

    // Dissolve
    public float dissolve_t;

    // Layer Masks
    public static int LayerMask_NAR_Ground, LayerMask_TowerGround, LayerMask_BaseGround, LayerMask_ForceField;

    // Device
    public static bool test;
    public GameObject AR;
    public Camera ARcam, NARcam;
    [HideInInspector] public Camera cam;


    void Awake()
    {
        inst = this;

        //LayerMask_NAR_Ground  = LayerMask.GetMask("NAR Ground");
        LayerMask_TowerGround = LayerMask.GetMask("Tower Ground");
        LayerMask_BaseGround  = LayerMask.GetMask("Base Ground");
        LayerMask_ForceField  = LayerMask.GetMask("Force Field");

        AR.SetActive(!test);
        //NAR.SetActive(test);
        if (test) cam = NARcam;
        else      cam = ARcam;
    }


    public void setTime(int t) => Time.timeScale = t;

    public void Reload()
    {
        setTime(1);
        Enemy.enemies.Clear();
        Tower.towers.Clear();
        WaveMan.inWave = false;
        SessionResultsManager.Instance.ResetResults();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit() => Application.Quit();


   
}

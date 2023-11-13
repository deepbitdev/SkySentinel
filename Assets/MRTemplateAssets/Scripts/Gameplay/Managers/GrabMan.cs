using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class GrabMan : MonoBehaviour
{
    public static GrabMan inst;

    // Ref
    bool test;
    Camera cam;


        // Init
    bool initBase = true;
    public ARRaycastManager arRaycastManager;


    [Header("Object Spawner")]
    public ObjectSpawner objectSpawner;


    [SerializeField]
    //ARRaycastHitEventAsset m_RaycastHit;


    // Grab
    [HideInInspector] public Tower tower;
    public RectTransform rtCancelPurchase;
    bool replace = false;

    // Events
    public UnityEvent
        activeBaseEvent, placeBaseEvent,
        grabEvent, ungrabEvent, grabActiveEvent,
        replaceEvent, unreplaceEvent;


    void Awake()
    {
        inst = this;
    }

    void Start()
    {
        Base.inst.gameObject.SetActive(false);
        test = ProjectMan.test;
        cam = ProjectMan.inst.cam;
    }


    void Update()
    {
        if (initBase)
            InitBase();
            //InitBase();


        else if (tower)
            Drag();
    }

    private void Drag()
    {
        Vector3 pos;

            //if ((test && Tool.MouseHit(cam, out pos, ProjectMan.LayerMask_NAR_Ground)) ||
            //    (!test && Tool.ScreenCenterHitAR(ProjectMan.inst.cam, arRaycastManager, out pos)))
            //{
            //    // rotation
            //    Vector3 camProj = cam.transform.position;
            //    camProj.y = pos.y;
            //    Tool.LookDir(tower, Tool.Dir(pos, camProj, false));

            //    // position
            //    tower.smoothTranslate.SetTarget(pos);

            //    // active
            //    if (!replace && !tower.gameObject.active)
            //    {
            //        tower.gameObject.SetActive(true);
            //        grabActiveEvent.Invoke();
            //    }
            //}




            #region Drag Game Environ
            //if ((test && Tool.MouseHit(cam, out pos, ProjectMan.LayerMask_NAR_Ground)) ||
            //    (!test && Tool.ScreenCenterHitAR(ProjectMan.inst.cam, arRaycastManager, out pos)))
            //{
            //    // rotation
            //    Vector3 camProj = cam.transform.position;
            //    camProj.y = pos.y;
            //    Tool.LookDir(tower, Tool.Dir(pos, camProj, false));

            //    // position
            //    tower.smoothTranslate.SetTarget(pos);

            //    // active
            //    if (!replace && !tower.gameObject.active)
            //    {
            //        tower.gameObject.SetActive(true);
            //        grabActiveEvent.Invoke();
            //    }
            //}
            #endregion




        // You can call these from the button UI onClickEvent 

        if (Tool.Click())
        {
            if (!replace && tower.gameObject.active && !Tool.MouseInRT(rtCancelPurchase))
                Buy();
            else if (replace)
                unreplace();
        }
    }


    private void Buy()
    {
        tower.hologram.Dissolve();
        Shop.inst.AddMoney(-tower.cost);
        tower = null;
        ungrabEvent.Invoke();
    }

    private void unreplace()
    {
        tower = null;
        unreplaceEvent.Invoke();
    }

    public void CancelPurchase()
    {
        Destroy(tower.gameObject);
        tower = null;
        ungrabEvent.Invoke();
    }

    

    public void InitBase()
    {
        Vector3 pos;

        ObjectSpawner objectSpawner = GetComponent<ObjectSpawner>();

        if(objectSpawner != null)
        {
            Vector3 spawnPoint = new Vector3(1f, 1f, 1f);
            Vector3 spawnNormal = new Vector3(0.0f, 0.0f, 0.0f);

            bool spawnSuccessful = objectSpawner.TrySpawnObject(spawnPoint, spawnNormal);

            if(spawnSuccessful)
            {
                //active
               if (!Base.inst.gameObject.active)
               {
                   Base.inst.gameObject.SetActive(true);
                   activeBaseEvent.Invoke();
               }
            }
            else
            {

            }
        }

        // Assuming you have a reference to SpawnManager and the parameters are defined
        

            #region Place Game
            //if ((test && Tool.MouseHit(cam, out pos, ProjectMan.LayerMask_NAR_Ground)) ||
            //    (!test && Tool.ScreenCenterHitAR(ProjectMan.inst.cam, arRaycastManager, out pos)))
            //{
            //    // position
            //    Base.inst.smoothTranslate.SetTarget(pos);

            //    // active
            //    if (!Base.inst.gameObject.active)
            //    {
            //        Base.inst.gameObject.SetActive(true);
            //        activeBaseEvent.Invoke();
            //    }
            //}

            // if(objectSpawner != null && objectSpawner.TrySpawnObject())
            // {
            //     if (!Base.inst.gameObject.active)
            //    {
            //        Base.inst.gameObject.SetActive(true);
            //        activeBaseEvent.Invoke();
            //    }

            // //    return true;
            // }


            // if(success)
            // {
            //     if (!Base.inst.gameObject.active)
            //    {
            //        Base.inst.gameObject.SetActive(true);
            //        activeBaseEvent.Invoke();
            //    }
            // }
            // else
            // {

            // }

            // if(ObjectSpawner.TrySpawnObject)
            // {
            //     //active
            //    if (!Base.inst.gameObject.active)
            //    {
            //        Base.inst.gameObject.SetActive(true);
            //        activeBaseEvent.Invoke();
            //    }
            // }
            #endregion


        if (Base.inst.gameObject.active)
        {
            initBase = false;
            Base.inst.hologram.Dissolve();
            placeBaseEvent.Invoke();
        }
    }



    public void Grab(Tower tower)
    {
        replace = false;
        this.tower = tower;
        grabEvent.Invoke();
    }

    public void Replace()
    {
        replace = true;
        tower = SightTower.inst.target;
        replaceEvent.Invoke();
    }
}

using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;

public class GrabMan : MonoBehaviour
{
    public static GrabMan inst;

    // Ref
    bool test;
    Camera cam;

    // Controller Haptics
    //public XRNode inputSource = XRNode.RightHand;
    //private InputDevice targetDevice;
    private bool isHapticSupported;

    private float defaultDuration = 0.5f;
    private float defaultAmplitude = 0.5f;


    // Init
    bool initTower = true;
    bool initBase = true;
    //public ARRaycastManager arRaycastManager;
    //public ARPlaneManager arPlaneManager;


    [Header("Object Spawner")]
    //public ObjectSpawner objectSpawner;


    // Grab
    //[SerializeField] public Tower tower;
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
        Base.inst.gameObject.SetActive(true);
        test = ProjectMan.test;
        cam = ProjectMan.inst.cam;
    }


    void Update()
    {


        if (initBase)
            InitBase();
            //InitBase();


        //else if (tower)
        //    Drag();
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

        //if (Tool.Click())
        //{
        //    if (!replace && tower.gameObject.active && !Tool.MouseInRT(rtCancelPurchase))
        //        Buy();
        //    else if (replace)
        //        unreplace();
        //}

        //if (!replace && tower.gameObject.active && !Tool.MouseInRT(rtCancelPurchase))
        //{
        //    Buy();
        //}
        //else if (replace)
        //{
        //    unreplace();
        //}
    }


    public void BackToWave()
    {
        activeBaseEvent.Invoke();
    }


    private void Buy()
    {
        //tower.hologram.Dissolve();
        //Shop.inst.AddMoney(-tower.cost);
        //tower = null;
        ungrabEvent.Invoke();
    }

    private void unreplace()
    {
        //tower = null;
        unreplaceEvent.Invoke();
    }

    public void CancelPurchase()
    {
        //Destroy(tower.gameObject);
        //tower = null;
        ungrabEvent.Invoke();
    }







    public void InitBase()
    {
        Vector3 pos;

        //ObjectSpawner objectSpawner = GetComponent<ObjectSpawner>();

        //if(objectSpawner != null)
        //{
        //    Vector3 spawnPoint = new Vector3(1f, 1f, 1f);
        //    Vector3 spawnNormal = new Vector3(0.0f, 0.0f, 0.0f);

        //    bool spawnSuccessful = objectSpawner.TrySpawnObject(spawnPoint, spawnNormal);

        //    if (spawnSuccessful)
        //    {
        //        //active
        //       //if (!Base.inst.gameObject.active)
        //       //{

        //       //     Base.inst.gameObject.SetActive(true);


        //       //     activeBaseEvent.Invoke();
        //       //}
        //    }
        //    else
        //    {

        //    }
        //}

        if (!Base.inst.gameObject.active)
        {

            Base.inst.gameObject.SetActive(true);


            activeBaseEvent.Invoke();
        }


        if (Base.inst.gameObject.active)
        {
            initBase = false;
            //Base.inst.hologram.Dissolve();
            ControllerHaptics.instance.EnvPlacementHaptic();
            placeBaseEvent.Invoke();
        }
    }

    //public void TurnOffPlanes()
    //{
    //    foreach (var plane in arPlaneManager.trackables)
    //    {
    //        plane.gameObject.SetActive(false);
    //    }
    //}


    public void Grab(Tower tower)
    {
        replace = false;
        //this.tower = tower;
        grabEvent.Invoke();
    }

    public void Replace()
    {
        replace = true;
        //tower = SightTower.inst.target;
        replaceEvent.Invoke();
    }
}

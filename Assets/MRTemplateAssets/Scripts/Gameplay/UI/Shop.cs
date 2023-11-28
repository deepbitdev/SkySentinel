using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    public static Shop inst;

    public Animator animator;

    //public RectTransform wirst, body;

    public int money;
    public TMP_Text moneyText;

    public const string moneyKey = "$";

    public UnityEvent moneyChangedEvent;

    void Awake()
    {
        inst = this;
        // moneyText.text = money + " $";
        moneyChangedEvent.Invoke();

        // update tower shop
        moneyText.text = PlayerPrefs.GetInt("money").ToString();
    }


    public RectTransform canvas;
    //void Update()
    //{
    //    if (IsOpened())
    //    {
    //        if (Tool.Click(body, false))
    //            Close();
    //    }


    //    else if (IsClosed())
    //    {
    //        if (Tool.Click(wirst))
    //            Open();
    //    }
    //}

    //public bool IsOpened() { return Tool.AnimIs(animator, "Shop Opened"); }
    //public bool IsClosed() { return Tool.AnimIs(animator, "Shop Closed"); }

    //public void Open()  => animator.SetTrigger("open");
    //public void Close() => animator.SetTrigger("close");
    //public void SetClosedAnim() => animator.Play("Shop Closed");


    

    private void OnDisable()
    {
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.Save();
    }

    void Reset()
    {
        PlayerPrefs.DeleteAll();
    }


    public void AddMoney(int m) {
        PlayerPrefs.SetInt("money", m);
        PlayerPrefs.Save();
        
        money += m;
        moneyText.text = money + " $";
        moneyChangedEvent.Invoke();
    }
}

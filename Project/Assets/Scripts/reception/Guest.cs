using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Guest : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI guestNameText;
    public TextMeshProUGUI orderText;
    public Slider waitSlider;

    private float waitTime;
    private float timer;
    public bool isServed = false;

    void Start()
    {
        string[] names = { "小狐", "阿喵", "团子", "橘橘", "年糕" };
        string[] orders = { "点了烤鸡", "点了饺子", "点了汤圆", "点了桂花糕", "点了红烧肉" };
        guestNameText.text = names[Random.Range(0, names.Length)];
        orderText.text = orders[Random.Range(0, orders.Length)];

        waitTime = Random.Range(10f, 20f);
        timer = 0;
        waitSlider.maxValue = waitTime;
        waitSlider.value = waitTime;
    }

    void Update()
    {
        if (isServed) return;

        timer += Time.deltaTime;
        waitSlider.value = waitTime - timer;

        if (timer >= waitTime)
        {
            gameObject.SetActive(false);
        }
    }

    public void Serve()
    {
        if (isServed) return;
        isServed = true;
        orderText.text = "已上菜！";
        waitSlider.gameObject.SetActive(false);
    }
}
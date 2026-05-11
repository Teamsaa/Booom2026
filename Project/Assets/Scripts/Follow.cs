using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Follow : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject knife;
    [SerializeField] private GameObject fr;
    [SerializeField] private GameObject sr;
    public bool isFollow = false;

    private Image image;


    void Update()
    {
        SetFollow();
        image = GetComponent<Image>();
    }

    public void SetFollow()
    {
        if (!isFollow) return;
        // 直接利用相机把屏幕点转成世界点
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = canvas.planeDistance; // UI 距离相机的深度
        image.raycastTarget = false;

        transform.position = canvas.worldCamera.ScreenToWorldPoint(screenPos);
    }

    public void SetMeat(bool isKnifeActive, bool isMeat)
    {
        image.enabled = false;
        knife.SetActive(isKnifeActive);
        fr.SetActive(isMeat == true);
        sr.SetActive(isMeat == true);
    }
}

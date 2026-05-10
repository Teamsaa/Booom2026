using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Follow : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    public bool isFollow = false;


    void Update()
    {
        SetFollow();
    }

    public void SetFollow()
    {
        if (!isFollow) return;
        // 直接利用相机把屏幕点转成世界点
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = canvas.planeDistance; // UI 距离相机的深度

        transform.position = canvas.worldCamera.ScreenToWorldPoint(screenPos);
    }
}

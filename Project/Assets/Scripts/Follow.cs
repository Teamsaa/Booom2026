using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
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

    private void OnDestroy()
    {
        Debug.Log("我被销毁了！");
    }

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

    /// <summary>
    /// 设置播放完动画的下一步，这个框架写的太烂的，这步只能这么写了，没办法解耦
    /// </summary>
    /// <param name="foodName">传入食材的名称</param>
    /// <param name="isActive">约定：true表示想要隐藏掉播放动画前的场景物体， false表示想要显现出播放完动画后的物体
    /// <param name="isDestroy">是否要销毁物体
    /// 因为我们最终都是要进行隐藏物体，所以对传进来的参数进行！运算处理，如果传进来的是true表示想在动画播放前进行隐藏，进行==true预算，结果false隐藏;
    /// 如果传进来为false, 表示想播完后重新显现，==ture运算，结果为true,显示。（只对==true的做特殊处理）
    /// </param>
    public void SetAnimationNext(string foodName, bool isActive, out bool isDestroy)
    {
        switch (foodName)
        {
            case "meat":
                image.enabled = false;// 这个是特殊的不管
                knife.SetActive(!isActive == true);
                fr.SetActive(isActive == false);
                sr.SetActive(isActive == false);
                isDestroy = true;
                break;
            case "baijiu":
                isDestroy = false;
                break;
            default:
                Debug.Log("传进来空参数，有问题！");
                isDestroy = true;
                break;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCookWare : MonoBehaviour
{
    [SerializeField] private GameObject changeCookWare;
    [SerializeField] private GameObject choppingBoard;
    [SerializeField] private GameObject knife;
    [SerializeField] private GameObject plate;

    [SerializeField] private Bowl bowl1;
    [SerializeField] private Bowl bowl2;

    private void Update()
    {
        if (bowl1.ISBowl1 && bowl2.ISBowl2)
        {
            Debug.Log("现在开始播放变换做菜场景动画！");
        }
    }


    IEnumerator CookeWareAnimationAndWait()
    {
        yield return null;
    }

}

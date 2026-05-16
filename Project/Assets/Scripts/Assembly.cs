using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Assembly : MonoBehaviour
{
    [SerializeField] private GameObject choppingBoard;
    [SerializeField] private GameObject jdjuan;
    [SerializeField] private GameObject roll;
    [SerializeField] private GameObject rollyc;
    [SerializeField] private GameObject arrow;
    private int index = 1; // 用来检查做到哪一步，如果步骤不对则无法继续

    private void OnEnable()
    {
        GameEvent.OnAssembly += AssemblyFood;
    }

    private void OnDestroy()
    {
        GameEvent.OnAssembly -= AssemblyFood;
    }

    private void AssemblyFood(GameObject food, Vector3 currentTransform)
    {
        var temp1 = food.GetComponent<Follow>();
        var temp2 = food.GetComponent<Image>();
        if (index == 1 && food.name == "qhsr")
        {
            temp1.isFollow = false;
            Cursor.visible = true;
            temp2.raycastTarget = false;
            food.transform.position = choppingBoard.transform.position;
            index++;
            return;
        }

        if (index == 2 && food.name == "qhfr")
        {
            temp1.isFollow = false;
            Cursor.visible = true;
            temp2.raycastTarget = false;
            food.transform.position = choppingBoard.transform.position;
            index++;
            return;
        }

        if (index == 3 && food.name == "jd")
        {
            Destroy(food);
            jdjuan.SetActive(true);
            Cursor.visible = true;
            index++;
            StartCoroutine(WaitMinute());
            return;
        }

        if (index == 4 &&food.name == "yc")
        {
            Destroy(food);
            roll.SetActive(false);
            rollyc.SetActive(true);
            Cursor.visible = true;
            index++;
        }

        Cursor.visible = true;
        temp1.isFollow = false;
        temp2.raycastTarget = true;
        food.transform.position = currentTransform;
    }

    IEnumerator WaitMinute()
    {
        yield return new WaitForSeconds(0.5f);

        arrow.SetActive(true);
    }
}

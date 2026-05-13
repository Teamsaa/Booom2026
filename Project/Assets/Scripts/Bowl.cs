using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ÍëµÄÂß¼­
/// </summary>
public class Bowl : MonoBehaviour
{
    [SerializeField] private Sprite bowl1;
    [SerializeField] private Sprite bowl2;

    public bool ISBowl1 => isBowl1;
    public bool ISBowl2 => isBowl2;

    private Image myImage;
    private RectTransform rect;
    private bool isBowl1 = false;
    private bool isBowl2 = false;


    private void OnEnable()
    {
        GameEvent.OnBowlChange += ChangeBowl;
    }

    private void OnDestroy()
    {
        GameEvent.OnBowlChange -= ChangeBowl;
    }

    private void Start()
    {
        myImage = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    private void ChangeBowl(GameObject food, GameObject targetBowl)
    {
        if (!food) return;

        if (food.name == "fr")
        {
            Cursor.visible = true;
            Destroy(food);

            if (targetBowl == this.gameObject)
            {
                myImage.sprite = bowl1;
                isBowl1 = true;
                rect.sizeDelta = new Vector2(343, 291);
            }
        }
        else if (food.name == "sr")
        {
            Cursor.visible = true;
            Destroy(food);

            if (targetBowl == this.gameObject)
            {
                myImage.sprite = bowl2;
                isBowl2 = true;
                rect.sizeDelta = new Vector2(347, 309);
            }
        }
    }
}

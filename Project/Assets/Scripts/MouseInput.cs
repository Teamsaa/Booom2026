using DG.Tweening;
using DG.Tweening.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private GameObject textUI;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Animator animator;
    [SerializeField] private List<GameObject> foodGameObject;

    public bool ISTexureNull => isTexureNull;

    private bool isTexureNull = true; // 判断当前玩家是否有拿着食材,true 表示当前没有拿, false 表示当前玩家有拿
    private bool isTextEnd = false; // 判断文本是否播放完毕
    private RaycastHit2D hit;
    private Food food;
    private TweenerCore<string, string, DG.Tweening.Plugins.Options.StringOptions> textAnimaion;
    private Tween tween;
    private string currentFoodName;

    private void Update()
    {
        Detection();
    }

    private void Start()
    {
        food = FindObjectOfType<Food>();
        animator.Play("egg");
    }

    // 鼠标点击检测函数
    private void Detection()
    {

        if (Input.GetMouseButtonDown(0) && textUI.activeSelf && isTextEnd)
        {
            if (tween.IsComplete())
            {
                tween.Rewind();
            }

            if (textAnimaion.IsComplete())
            {
                textAnimaion.Rewind();
            }

            textUI.SetActive(false);
            isTextEnd = false;

        }
        else if (Input.GetMouseButtonDown(0) && textUI.activeSelf && !isTextEnd)
        {
            Debug.Log("发现当前动画未完成，直接结束！");
            textAnimaion.Complete();
        }


        if (Input.GetMouseButtonDown(0) && !textUI.activeSelf)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction);
            Debug.Log($"当前玩家拿着食物:{isTexureNull == false}");

            // 判断玩家是否点到食材
            if (Physics2D.Raycast(ray.origin,ray.direction) && hit.collider != null)
            {
                for (int i = 0; i < food.FoodName.Count; i++)
                {
                    if (hit.collider.gameObject.name == food.FoodName[i])
                    {
                        UnityEngine.Cursor.SetCursor(food.FoodTextures[i], Vector2.zero, CursorMode.ForceSoftware);
                        currentFoodName = food.FoodName[i];
                        isTexureNull = false;
                        textUI.SetActive(true);
                        Text(food.FoodText[i]);
                    }
                }
              
                Debug.Log($"当前玩家拿着食物:{isTexureNull == false }" );
                Debug.Log("点击的物品是：" + hit.collider.gameObject.name);

                if (!isTexureNull && hit.collider && hit.collider.name == "Chopping board")
                {
                    Debug.Log(currentFoodName);
                    for (int i = 0; i < foodGameObject.Count; i++)
                    {

                        if (currentFoodName == foodGameObject[i].gameObject.name)
                        {
                            Debug.Log("播放动画");
                            Debug.Log(foodGameObject[i].gameObject.name);
                            StartCoroutine(PlayAnimationDelay(foodGameObject[i].gameObject.name, i));
                        }
                    }
                }
            }
        }

        
    }

    // 文字效果
    private void Text(string text)
    {
        tween = canvasGroup.DOFade(1, 1);
        this.textAnimaion = DOTween.To(
            () => "",
            currentText => textMeshProUGUI.text = currentText,
            text,
            5f
         ).SetEase(Ease.Linear).OnComplete(()=> { isTextEnd = true;  Debug.Log("文字播放完毕！"); });
    }


    IEnumerator PlayAnimationDelay(string name, int i)
    {
        foodGameObject[i].SetActive(true);

        yield return null;

        animator.SetBool("IsActive", true);
    }    
}

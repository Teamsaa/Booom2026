using DG.Tweening;
using DG.Tweening.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private GameObject textUI;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Animator animator;
    [SerializeField] private List<GameObject> foodGameObject;

    public bool ISTexureNull => isTexureNull; // 暂时没用到先放着

    private bool isTexureNull = true; // 判断当前玩家是否有拿着食材,true 表示当前没有拿, false 表示当前玩家有拿
    private bool isTextEnd = false; // 判断文本是否播放完毕
    private RaycastHit2D hit;
    private Food food;
    private TweenerCore<string, string, DG.Tweening.Plugins.Options.StringOptions> textAnimaion;
    private Tween tween;
    private string currentFoodName; // 用来保存当前玩家点击的食物的名字 后续用来判断动画播放和文本内容

    private void Update()
    {
        Detection();
    }

    private void Start()
    {
        food = FindObjectOfType<Food>();
    }

    // 鼠标点击检测函数
    private void Detection()
    {
        // 如过判断点击到的是UI界面，直接返回，因为Ray射线检测会穿透UI层，所以要做一下特殊处理
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && !textUI.activeSelf)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction);
            Debug.Log($"当前玩家拿着食物:{isTexureNull == false}");

            // 判断玩家是否点到物品（包括案板、食材）
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

                // 点击到菜板就播放做菜动画
                if (!isTexureNull && hit.collider && hit.collider.name == "Chopping board")
                {
                    Debug.Log(currentFoodName);

                    for (int i = 0; i < foodGameObject.Count; i++)
                    {

                        if (currentFoodName == foodGameObject[i].gameObject.name)
                        {
                            Debug.Log("播放动画");
                            Debug.Log(foodGameObject[i].gameObject.name);
                            foodGameObject[i].SetActive(true);
                            StartCoroutine(FoodAnimationAndWait(i, foodGameObject[i].gameObject.name));
                        }
                    }
                }
            }
            else if (!isTexureNull && !hit.collider)
            {
                UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
                isTexureNull = true;
            }
        }

        // 这里是快速播放完Dotween文字动画效果的逻辑 和 重置Dotween动画效果
        if (Input.GetMouseButtonDown(0) && textUI.activeSelf && isTextEnd)
        {
            canvasGroup.alpha = 0;
            textMeshProUGUI.text = "";
            textUI.SetActive(false);
            isTextEnd = false;

        }
        else if (Input.GetMouseButtonDown(0) && textUI.activeSelf && !isTextEnd)
        {
            Debug.Log("发现当前动画未完成，直接结束！");
            textAnimaion.Complete();
        }

    }

    #region Dotween做的文字动画效果
    // 文字效果
    private void Text(string text)
    {
        tween = canvasGroup.DOFade(1, 1);
        StartCoroutine(TextAnimation(text));
    }

    IEnumerator TextAnimation(string text)
    {
        yield return new WaitForSeconds(0.5f);
        this.textAnimaion = DOTween.To(
           () => "",
           currentText => textMeshProUGUI.text = currentText,
           text,
           5f
        ).SetEase(Ease.Linear).OnComplete(() => { isTextEnd = true; Debug.Log("文字播放完毕！"); });
    }
    #endregion

    // 判断动画是否播完

    IEnumerator FoodAnimationAndWait(int i, string name)
    {
        animator.SetBool("Is" + name, true);
        yield return null;

        while (true)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log(stateInfo.IsName(name));
            if (stateInfo.IsName(name))
            {
                if (stateInfo.normalizedTime >= 1.0f)
                    break;
            }

            yield return null;
        }

        foodGameObject[i].SetActive(false);
        animator.SetBool("Is" + name, false);
    }
}

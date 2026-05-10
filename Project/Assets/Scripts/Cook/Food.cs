using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Food : Singleton<Food>
{
    [SerializeField] private FoodScriptableObject food;

    public List<string> FoodName => foodName;
    public List<string> FoodText => foodText;

    private List<string> foodText;
    private List<string> foodName;

    protected override void Init()
    {
        base.Init();
        foodName = new List<string>();
        foodText = new List<string>();
        for (int i = 0; i<food.foodName.Count; i++)
        {
            foodText.Add(food.foodText[i]);
            foodName.Add(food.foodName[i]);
            Debug.Log($"食物是：{foodName[i]}, 文本内容是：{foodText[i]}");
        }

        Debug.Log("成功初始化食物列表！一共初始化" +  foodName.Count + "个！");
    }
    
}

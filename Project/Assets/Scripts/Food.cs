using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Food : Singleton<Food>
{
    [SerializeField] private FoodScriptableObject food;
    [SerializeField] private TextScriptableObject text;

    public List<string> FoodName => foodName;
    public List<string> FoodText => foodText;
    public List<Texture2D> FoodTextures => foodTextures;

    private List<string> foodText;
    private List<string> foodName;
    private List<Texture2D> foodTextures;
    
    protected override void Init()
    {
        base.Init();
        foodName = new List<string>();
        foodText = new List<string>();
        foodTextures = new List<Texture2D>();
        for (int i = 0; i<food.foodList.Count; i++)
        {
            foodText[i] = text.text[i];
            foodName[i] = food.foodName[i];
            foodTextures[i] = food.foodList[i];
        }
    }
    
}

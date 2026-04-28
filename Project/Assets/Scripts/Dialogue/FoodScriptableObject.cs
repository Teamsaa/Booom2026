using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "ScriptableObject/Food")]
public class FoodScriptableObject : ScriptableObject
{
    [Header("食物图片")]
    public List<Texture2D> foodList;

    [Header("食物名字")]
    public List<string> foodName;
}

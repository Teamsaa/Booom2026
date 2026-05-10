using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "ScriptableObject/Food")]
public class FoodScriptableObject : ScriptableObject
{
    [Header("食物名字")]
    public List<string> foodName;

    [Header("文本内容")]
    public List<string> foodText;

}

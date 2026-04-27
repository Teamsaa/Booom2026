using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Food/Food Image")]
public class FoodImage : ScriptableObject
{
    [Header("Ê³ÎïÍ¼Æ¬")]
    public List<Texture2D> foodList;
}

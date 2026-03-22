using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItems", menuName = "ShopItems")]
public class ShopItems : ScriptableObject
{
    public int day = 0;
    public List<Food.FoodType> foodTypes = new();
    public List<int> foodShopCount = new();
    public List<int> foodShopPrices = new();
}

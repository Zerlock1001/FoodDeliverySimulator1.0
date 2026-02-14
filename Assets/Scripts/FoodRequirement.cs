using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FoodRequirement", menuName = "FoodRequirement")]
public class FoodRequirement : ScriptableObject
{
    public List<Food.FoodType> requirementLevel0 = new List<Food.FoodType>();
    public List<Food.FoodType> requirementLevel1 = new List<Food.FoodType>();
    public List<Food.FoodType> requirementLevel2 = new List<Food.FoodType>();
    public List<Food.FoodType> requirementLevel3 = new List<Food.FoodType>();
}

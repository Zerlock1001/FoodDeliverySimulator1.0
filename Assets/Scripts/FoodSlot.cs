using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodSlot : MonoBehaviour
{
    public Food.FoodType foodInThisSlot = Food.FoodType.Null;
    public int foodCount = 0;
    public TMP_Text foodCountText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        UpdateNumberOfFood();
    }
    public virtual void UpdateNumberOfFood(){
        if(foodInThisSlot != Food.FoodType.Null){
        foodCount = GameData.instance.foodCount[GameData.instance.foodList.IndexOf(foodInThisSlot)];
            foodCountText.text = foodCount.ToString();
            if(foodCount == 0){
                foodCountText.text = "";
            }
        }
        else{
            foodCountText.text = "";
        }
    }
}

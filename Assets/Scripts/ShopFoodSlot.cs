using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopFoodSlot : FoodSlot
{
    public int price = 0;
    public Image soldOutImage;
    public TMP_Text priceText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }
    public void UpdatePrice(){
        priceText.text = "Price: $" + price.ToString();
    }
    public void Purchase(){
        if(GameData.instance.money >= price && foodCount > 0){
            GameData.instance.money -= price;
            foodCount--;
            if(foodCount == 0){
                soldOutImage.gameObject.SetActive(true);
            }
            GameData.instance.foodCount[GameData.instance.foodList.IndexOf(foodInThisSlot)]++;
            UpdateNumberOfFood();
            ShopManager.instance.ShowFeedbackText("Purchase successful.");
            return ;
        }
        if(foodCount <= 0){
            ShopManager.instance.ShowFeedbackText("Sold out!");
            return ;
        }
        ShopManager.instance.ShowFeedbackText("You don't have enough money!");
        return ;
    }
    public override void UpdateNumberOfFood(){
        foodCountText.text = foodCount.ToString();
        UpdatePrice();
    }
}

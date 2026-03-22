using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public List<FoodSlot> foodSlots = new();
    public List<ShopFoodSlot> shopFoodSlots = new();
    public List<GameObject> foodPrefab = new();
    public ShopItems shopItems;
    public TMP_Text moneyText;
    public GameObject feedbackText;
    public static ShopManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        for(int i = 0; i < GameData.instance.foodList.Count; i++){
            if(GameData.instance.foodCount[i] > 0){ // 如果食物数量大于0
                foodSlots[i].foodInThisSlot = GameData.instance.foodList[i];
                foodSlots[i].gameObject.GetComponent<Image>().sprite = foodPrefab[i].GetComponent<SpriteRenderer>().sprite;
                foodSlots[i].UpdateNumberOfFood();
            }
        }
        shopItems = GameData.instance.shopItems.Find(item => item.day == GameData.instance.day);
        foreach(ShopFoodSlot shopFoodSlot in shopFoodSlots){
            shopFoodSlot.gameObject.SetActive(false);
        }
        for(int i = 0; i < shopItems.foodTypes.Count; i++){
            shopFoodSlots[i].gameObject.SetActive(true);
            shopFoodSlots[i].foodInThisSlot = shopItems.foodTypes[i];
            shopFoodSlots[i].gameObject.GetComponent<Image>().sprite = foodPrefab[GameData.instance.foodList.IndexOf(shopItems.foodTypes[i])].GetComponent<SpriteRenderer>().sprite;
            shopFoodSlots[i].foodCount = shopItems.foodShopCount[i];
            shopFoodSlots[i].price = shopItems.foodShopPrices[i];
            shopFoodSlots[i].UpdatePrice();
            shopFoodSlots[i].UpdateNumberOfFood();
        }
        UpdateMoneyText();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoneyText();
    }
    public void UpdateMoneyText(){
        moneyText.text = "Money: $" + GameData.instance.money.ToString();
    }
    public void ShowFeedbackText(string text){
        feedbackText.GetComponent<TMP_Text>().text = text;
        feedbackText.GetComponent<Animator>().SetTrigger("PopUp");
    }
}

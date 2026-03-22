using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public int day = 1;
    public int money = 0;
    public static GameData instance;
    public List<Food.FoodType> foodList = new List<Food.FoodType>();
    public List<int> foodCount = new List<int>();
    public List<ShopItems> shopItems = new List<ShopItems>();

    // Start is called before the first frame update
    void Awake(){
        if(instance == null){
            DontDestroyOnLoad(this);
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

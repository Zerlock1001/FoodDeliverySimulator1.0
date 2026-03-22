using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
public class GamePlayManagement : MonoBehaviour
{
    public static GamePlayManagement instance;// 实例   
    public GameState currentGameState;// 当前游戏状态  
    public static event Action<GameState> OnGameStateChanged;// 游戏状态改变事件
    public List<Character> characters = new List<Character>();// 角色列表
    public Character currentCharacter;// 当前角色
    public Transform characterPosition;// 角色位置
    public Transform characterLeavePosition;// 角色离开位置
    bool isGamePlayEnd = false;// 游戏是否结束
    public List<GameObject> CharacterSlots = new List<GameObject>();// 角色槽位
    public FoodRequirement foodRequirement;// 食物需求
    public List<GameObject> foodPrefab = new List<GameObject>();// 食物预制体
    public int salary = 0;
    public int punishment = 0;
    public int extraMoney = 10;
    public int dailyOutcome = -10;
    public List<CheckManager.CheckResult> checkResults = new List<CheckManager.CheckResult>();
    public GameObject DayEndPanel;
    public List<FoodSlot> foodSlots = new List<FoodSlot>();
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;// 设置实例
        ShuffleCharacters();// 洗牌

    }
    void Start(){
        BlackScreenController.instance.BlackScreenFadeOut("Day " + GameData.instance.day, 2f,DayStart);
        int k = 0;
        for(int i = 0; i < GameData.instance.foodList.Count; i++){
            if(GameData.instance.foodCount[i] > 0){ // 如果食物数量大于0
                for(int j = 0; j < GameData.instance.foodCount[i]; j++){
                    GameObject food = Instantiate(foodPrefab[i],foodSlots[k].transform.position,Quaternion.identity);
                }
                foodSlots[k].foodInThisSlot = GameData.instance.foodList[i];
                foodSlots[k].UpdateNumberOfFood();
                k++;
            }
        }
    }

    void DayStart()
    {
        SetCharactersPosition();// 设置角色位置
        if (GameData.instance.day == 1) 
        {
            SetGameState(GameState.OpeningGuidance);
            
            string[] tutorial =
            {
                "Welcome to Food Delivery Simulator.",
                "Please check the guidance book below.",
                "Deliver food to each person according to their class.",
                "Delivering the wrong food will be punished.",
                "Good Luck."
            };
            DialogueManagement.instance.StartDialogue(tutorial);
        }
        else 
        {
            NextCharacter();
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            SetGameState(GameState.CharacterWaiting);// 设置游戏状态为角色等待
        }
        if(Input.GetKeyDown(KeyCode.A)){
            SetGameState(GameState.CharacterMoving);// 设置游戏状态为角色移动
        }
    }
    public void SetGameState(GameState newGameState)// 设置游戏状态
    {
        currentGameState = newGameState;// 设置当前游戏状态
        OnGameStateChanged?.Invoke(newGameState);
        if(newGameState == GameState.CharacterMoving){
            CharacterMoving();// 角色移动
            //Invoke("ChangeToCharacterWaiting", 2f);// 2秒后调用ChangeToCharacterWaiting方法     
        }
    }
    public void CharacterMoving(){// 角色移动
        if(currentCharacter != null && !isGamePlayEnd){
            currentCharacter.CharacterMove();// 角色移动
        }

    }
    public void ChangeToCharacterWaiting(){// 改变为角色等待
        SetGameState(GameState.CharacterWaiting);// 设置游戏状态为角色等待
    }
    
    public enum GameState{
        OpeningGuidance, // 新增：开场引导
        CharacterMoving,// 角色移动
        CharacterWaiting,// 角色等待
        GamePlayEnd,// 游戏结束
    }
    public bool NextCharacter(){// 下一个角色
        if(characters.Count > 0){
            currentCharacter = characters[0];// 设置当前角色
            characters.RemoveAt(0);
            SetGameState(GameState.CharacterMoving);// 设置游戏状态为角色移动
            return true;
        }else{
            SetGameState(GameState.GamePlayEnd);// 设置游戏状态为游戏结束
            Debug.Log("Game Play End");// 打印游戏结束
            DayEnd();
            return false;
        }
    }
    void DayEnd(){
        DayEndPanel.SetActive(true);
        Debug.Log("checkResults count: " + checkResults.Count);
        List<CheckManager.CheckResult> checkResultsList = new();
        if(salary != 0){
            checkResultsList.Add(new("Salary", salary));
        }
        if(punishment != 0){
            checkResultsList.Add(new("Punishment", punishment));
        }
        if(extraMoney != 0){
            checkResultsList.Add(new("Income", extraMoney));
        }
        if(dailyOutcome != 0){
            checkResultsList.Add(new("Daily Outcome", dailyOutcome));
        }
        foreach(CheckManager.CheckResult result in checkResults){
            checkResultsList.Add(new(result.sentence, result.money));
        }
        DayEndPanel.GetComponent<CheckManager>().UpdateMoney(checkResultsList);
    }
    public void ShuffleCharacters(){
        for (int i = characters.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (characters[i], characters[j]) = (characters[j], characters[i]);
        }
    }
    public void SetCharactersPosition(){
        for(int i = 0; i < characters.Count; i++){
            characters[i].transform.position = CharacterSlots[i].transform.position;
        }
    }
    public void RemoveFood(List<Food.FoodType> foodInThisPlate){// 移除食物
        foreach(Food.FoodType foodType in foodInThisPlate){
            GameData.instance.foodCount[GameData.instance.foodList.IndexOf(foodType)]--;
        }
    }
}

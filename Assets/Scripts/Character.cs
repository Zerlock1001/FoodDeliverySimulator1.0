using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Character : MonoBehaviour// 角色類別
{
    public bool isTalking = false;// 是否正在對話
    public string[] dialogueLines;// 对话文本
    public bool isFed = false;// 是否已经分配食物
    public float moveTime = 1.5f;// 移动时间
    public int characterLevel = 0;// 角色等级 0 = D级 1 = C级 2 = B级 3 = A级

    // Start is called before the first frame update
    void Start()
    {
        GamePlayManagement.OnGameStateChanged += StartDialogue;// 游戏状态改变时，调用StartDialogue方法
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartDialogue(GamePlayManagement.GameState gameState)
    {
        if (GamePlayManagement.instance.currentCharacter != this) return;
        Debug.Log(gameObject.name + " 尝试说话，当前状态: " + gameState);
        
        if(gameState == GamePlayManagement.GameState.CharacterWaiting && !isFed)
        {
            // 【关键修改 2】：防止行数为 0 导致的闪退
            if (dialogueLines != null && dialogueLines.Length > 0)
            {
                Debug.Log(gameObject.name + " 正在显示对话，行数: " + dialogueLines.Length);
                DialogueManagement.instance.StartDialogue(dialogueLines, false);
            }
            else
            {
                Debug.LogError(gameObject.name + " 没填台词！跳过对话。");
                // 如果没词，直接进入等待喂食状态，不要卡死
            }
        }
        else
        {
            DialogueManagement.instance.EndDialogue();
        }
    }
    public void CharacterMove(){// 角色移动
        if(isFed){// 如果已经分配食物
            Vector3 targetPosition = GamePlayManagement.instance.characterLeavePosition.position;// 目标位置
            targetPosition.z = transform.position.z;
            transform.DOMove(targetPosition, moveTime).onComplete = () => {
                GamePlayManagement.instance.NextCharacter();// 下一个角色
            };
        }else{
            Vector3 targetPosition = GamePlayManagement.instance.characterPosition.position;// 目标位置
            targetPosition.z = transform.position.z;
            transform.DOMove(targetPosition, moveTime).onComplete = () => {
                GamePlayManagement.instance.SetGameState(GamePlayManagement.GameState.CharacterWaiting);// 设置游戏状态为角色等待

            };
        }
    }
    public void Feed(List<Food.FoodType> foodInThisPlate){// 判断食物是否满足
        isFed = true;
        GamePlayManagement.instance.SetGameState(GamePlayManagement.GameState.CharacterMoving);// 设置游戏状态为角色移动
        switch(characterLevel){
            case 0://D级
                foreach(Food.FoodType foodType in GamePlayManagement.instance.foodRequirement.requirementLevel0){
                    if(!foodInThisPlate.Contains(foodType)){
                        GamePlayManagement.instance.punishment -= 5;
                        //To-Do 处理食物不足的情况
                        return;
                    }
                }
                GamePlayManagement.instance.salary += 10;
                break;
            case 1://C级
                foreach(Food.FoodType foodType in GamePlayManagement.instance.foodRequirement.requirementLevel1){
                    if(!foodInThisPlate.Contains(foodType)){
                        GamePlayManagement.instance.punishment -= 5;
                        //To-Do 处理食物不足的情况
                        return;
                    }
                }
                GamePlayManagement.instance.salary += 10;
                break;
            case 2://B级
                foreach(Food.FoodType foodType in GamePlayManagement.instance.foodRequirement.requirementLevel2){
                    if(!foodInThisPlate.Contains(foodType)){
                        GamePlayManagement.instance.punishment -= 5;
                        //To-Do 处理食物不足的情况
                        return;
                    }
                }
                GamePlayManagement.instance.salary += 10;
                break;
            case 3://A级
                foreach(Food.FoodType foodType in GamePlayManagement.instance.foodRequirement.requirementLevel3){
                    if(!foodInThisPlate.Contains(foodType)){
                        GamePlayManagement.instance.punishment -= 10;
                        //To-Do 处理食物不足的情况
                        return;
                    }
                }
                GamePlayManagement.instance.salary += 10;
                break;
            default:
                break;
        }
    } 
}

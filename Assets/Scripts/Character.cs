using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour// 角色類別
{
    public bool isTalking = false;// 是否正在對話
    public string dialogueText;// 对话文本
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
    public void StartDialogue(GamePlayManagement.GameState gameState){// 开始对话
        if(gameState == GamePlayManagement.GameState.CharacterWaiting && !isFed){
            DialogueManagement.instance.StartDialogue(this);// 开始对话
        }else{
            if(GamePlayManagement.instance.currentCharacter == this){
                DialogueManagement.instance.EndDialogue();// 结束对话
            }
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
            case 0:
                foreach(Food.FoodType foodType in GamePlayManagement.instance.foodRequirement.requirementLevel0){
                    if(!foodInThisPlate.Contains(foodType)){
                        Debug.Log("Character " + name + " is not fed because of " + foodType);
                        //To-Do 处理食物不足的情况
                        return;
                    }
                }
                Debug.Log("Character " + name + " is fed");
                break;
            case 1:
                foreach(Food.FoodType foodType in GamePlayManagement.instance.foodRequirement.requirementLevel1){
                    if(!foodInThisPlate.Contains(foodType)){
                        Debug.Log("Character " + name + " is not fed because of " + foodType);
                        //To-Do 处理食物不足的情况
                        return;
                    }
                }
                Debug.Log("Character " + name + " is fed");
                break;
            case 2:
                foreach(Food.FoodType foodType in GamePlayManagement.instance.foodRequirement.requirementLevel2){
                    if(!foodInThisPlate.Contains(foodType)){
                        Debug.Log("Character " + name + " is not fed because of " + foodType);
                        //To-Do 处理食物不足的情况
                        return;
                    }
                }
                Debug.Log("Character " + name + " is fed");
                break;
            case 3:
                foreach(Food.FoodType foodType in GamePlayManagement.instance.foodRequirement.requirementLevel3){
                    if(!foodInThisPlate.Contains(foodType)){
                        Debug.Log("Character " + name + " is not fed because of " + foodType);
                        //To-Do 处理食物不足的情况
                        return;
                    }
                }
                Debug.Log("Character " + name + " is fed");
                break;
            default:
                break;
        }
    }

    
}

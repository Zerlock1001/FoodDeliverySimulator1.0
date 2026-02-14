using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoodType = Food.FoodType;

public class FoodPlate : MonoBehaviour,IDragHandler
{
    public List<FoodType> foodInThisPlate = new List<FoodType>();// 食物列表
    public Vector3 startPosition;// 起始位置
    public void OnDragEnd()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.zero,100f,LayerMask.GetMask("Character"));// 射线检测   
        if (hit.collider != null || GamePlayManagement.instance.currentGameState != GamePlayManagement.GameState.CharacterMoving)// 如果检测到碰撞
        {
            GameObject hitObject = hit.collider.gameObject != null ? hit.collider.gameObject : null;// 获取碰撞对象
            if(hitObject != null && hitObject.GetComponent<Character>() != null && GamePlayManagement.instance.currentCharacter == hitObject.GetComponent<Character>())// 如果碰撞对象是角色且当前角色是该角色
            {
                hitObject.GetComponent<Character>().Feed(foodInThisPlate);// 分配食物
                gameObject.transform.position = startPosition;// 重置位置
                GamePlayManagement.instance.RemoveFood(foodInThisPlate);
                foodInThisPlate.Clear();// 清空食物列表
                foreach(Transform food in gameObject.transform.GetComponentsInChildren<Transform>())// 遍历子物体
                {
                    if(food.GetComponent<Food>() != null){// 如果子物体是食物
                        food.gameObject.SetActive(false);// 隐藏食物
                    }
                }
                //To-Do 根据食物类型处理Character逻辑
            }
            else{
                gameObject.transform.position = startPosition;// 重置位置
            }
        }
        else{
            gameObject.transform.position = startPosition;// 重置位置
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        Debug.Log("FoodPlate clicked,开始拖拽");
        DragManager.Instance.StartDragging(this.gameObject);
    }
}

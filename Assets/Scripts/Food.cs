using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour,IDragHandler
{
    public FoodType foodType;
    public Vector3 startPosition;
    public void OnDragEnd()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.zero,100f,LayerMask.GetMask("FoodPlate"));
        if (hit.collider != null)
        {
            Debug.Log("Food hit " + hit.collider.gameObject.name);
            GameObject hitObject = hit.collider.gameObject;
            if(hitObject.GetComponent<FoodPlate>() != null)
            {
                gameObject.transform.SetParent(hitObject.transform);
                hitObject.GetComponent<FoodPlate>().foodInThisPlate.Add(foodType);
                Debug.Log("Food hit FoodPlate");
                return ;
            }
        }
        gameObject.transform.SetParent(null);
        gameObject.transform.position = startPosition;

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
    void OnMouseDown()//当鼠标点击食物时，开始拖拽
    {
        if(gameObject.transform.parent != null && gameObject.transform.parent.gameObject.GetComponent<FoodPlate>() != null){
            FoodPlate foodPlate = gameObject.transform.parent.gameObject.GetComponent<FoodPlate>();
            foodPlate.foodInThisPlate.Remove(foodType);
        }
        Debug.Log("Food clicked,开始拖拽");
        DragManager.Instance.StartDragging(this.gameObject);
    }
    public enum FoodType
    {
        Bread,
        Burger,
        Cookie,
    }

}

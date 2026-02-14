using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    public GameObject DraggingObject;
    public Vector3 DraggingOffset;
    public static DragManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (DraggingObject != null)
        {
            Dragging();
        }
    }
    void Dragging(){
        Vector3 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        DraggingObject.transform.position = new Vector3(mousePositon.x, mousePositon.y, DraggingObject.transform.position.z) + DraggingOffset;
        if (Input.GetMouseButtonUp(0))
        {
            IDragHandler dragHandler = DraggingObject.GetComponent<IDragHandler>() as IDragHandler;
            Debug.Log("dragHandler: " + dragHandler);
            if(dragHandler != null)
            {
                dragHandler.OnDragEnd();
            }
            DraggingObject = null;
            //TO-DO 放下拖拽物的处理
        }
    }
    public void StartDragging(GameObject draggingObject)
    {
        DraggingObject = draggingObject;
        if(draggingObject != null){
            Vector3 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DraggingOffset = draggingObject.transform.position - new Vector3(mousePositon.x, mousePositon.y, draggingObject.transform.position.z);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    public GameObject introductionPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenIntroduction(){
        Time.timeScale = 0;//暂停游戏
        introductionPanel.SetActive(true);
    }
    public void CloseIntroduction(){
        Time.timeScale = 1;//继续游戏
        introductionPanel.SetActive(false);
    }
    void OnMouseDown(){
        OpenIntroduction();
    }
}

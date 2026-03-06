using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckManager : MonoBehaviour
{
    public List<TMP_Text> sentenceTexts = new List<TMP_Text>();
    public List<TMP_Text> moneyTexts = new List<TMP_Text>();
    public TMP_Text currentMoneyText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateMoney(GamePlayManagement GPM){
        GameData.instance.money += GPM.salary + GPM.extraMoney + GPM.dailyOutcome;
        moneyTexts[0].text = GetSentence(GPM.salary);
        moneyTexts[1].text = GetSentence(GPM.extraMoney);
        moneyTexts[2].text = GetSentence(GPM.dailyOutcome);
        currentMoneyText.text = "$" + GameData.instance.money.ToString();
    }
    string GetSentence(int moneyCount){
        if(moneyCount >= 0){
            return "+$" + moneyCount.ToString();
        }else{
            return "-$" + moneyCount.ToString().Replace("-", "");
        }
    }
}

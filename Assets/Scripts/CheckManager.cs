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
    public void UpdateMoney(List<CheckResult> checkResults){
        foreach(CheckResult result in checkResults){
            GameData.instance.money += result.money;
        }
        Debug.Log("checkResults: " + checkResults.Count);
        foreach(TMP_Text text in sentenceTexts){
            text.text = "";
        }
        foreach(TMP_Text text in moneyTexts){
            text.text = "";
        }
        for(int i = 0; i < checkResults.Count; i++){
            sentenceTexts[i].text = checkResults[i].sentence;
            moneyTexts[i].text = GetSentence(checkResults[i].money);
        }
        currentMoneyText.text = "$" + GameData.instance.money.ToString();
    }
    string GetSentence(int moneyCount){
        if(moneyCount >= 0){
            return "+$" + moneyCount.ToString();
        }else{
            return "-$" + moneyCount.ToString().Replace("-", "");
        }
    }
    [System.Serializable]public class CheckResult{
        public string sentence;
        public int money;
        public CheckResult(string sentence, int money){
            this.sentence = sentence;
            this.money = money;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManagement : MonoBehaviour
{
    public static DialogueManagement instance;

    [Header("常规对话UI")]
    public GameObject dialoguePanel;// 对话面板
    public TextMeshProUGUI dialogueText;// 对话文本

    [Header("新手引导UI")]
    public GameObject guidePanel; 
    public TextMeshProUGUI guideText;

    private TextMeshProUGUI activeText; 
    private GameObject activePanel;
    private Queue<string> sentences = new Queue<string>();

    void Awake() { instance = this; }

    void Update()
    {
        // 如果有面板在显示，且按下回车或空格，显示下一句
        if ((dialoguePanel.activeSelf || guidePanel.activeSelf) && 
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(string[] lines, bool isGuidance = false)
    {
        sentences.Clear();
        foreach (string line in lines) sentences.Enqueue(line);

        // 根据类型，分别激活/隐藏对应的面板
        if (isGuidance) {
            guidePanel.SetActive(true);    
            dialoguePanel.SetActive(false); 
        } else {
            guidePanel.SetActive(false);   
            dialoguePanel.SetActive(true);  
        }

        activePanel = isGuidance ? guidePanel : dialoguePanel;
        activeText = isGuidance ? guideText : dialogueText;

        DisplayNextSentence();
    }
    
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        activeText.text = sentences.Dequeue();
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        guidePanel.SetActive(false);

        if (GamePlayManagement.instance.currentGameState == GamePlayManagement.GameState.OpeningGuidance)
        {
            GamePlayManagement.instance.NextCharacter();
        }
    }
}


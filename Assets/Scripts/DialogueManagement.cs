using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManagement : MonoBehaviour
{
    public GameObject dialoguePanel;// 对话面板
    public TextMeshProUGUI dialogueText;// 对话文本
    public static DialogueManagement instance;
    // Start is called before the first frame update
    void Awake()// 初始化
    {
        instance = this;// 设置实例
    }

    public void StartDialogue(string dialogueText)// 开始对话，重载方法参数为字符串
    {
        dialoguePanel.SetActive(true);// 显示对话面板
        this.dialogueText.text = dialogueText;// 设置对话文本
    }
    public void StartDialogue(Character character)// 开始对话，重载方法参数为角色
    {
        dialoguePanel.SetActive(true);// 显示对话面板
        this.dialogueText.text = character.dialogueText;// 设置对话文本
    }
    public void EndDialogue()// 结束对话
    {
        dialoguePanel.SetActive(false);
    }
}

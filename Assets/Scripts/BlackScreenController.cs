using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.UI;
public class BlackScreenController : MonoBehaviour
{
    public Image blackScreen;
    public TMP_Text blackScreenText;
    public static BlackScreenController instance;

    void Awake(){
        instance = this;
    }
    public void BlackScreenFadeIn(string text,float duration,Action onComplete){
        blackScreen.gameObject.SetActive(true);
        blackScreen.color = new Color(blackScreen.color.r,blackScreen.color.g,blackScreen.color.b,0);
        blackScreenText.text = text;
        blackScreenText.DOFade(1, duration);
        blackScreen.DOFade(1, duration).OnComplete(() => {
            onComplete?.Invoke();
        });
    }
    public void BlackScreenFadeIn(string text,float duration){
        blackScreen.gameObject.SetActive(true);
        blackScreen.color = new Color(blackScreen.color.r,blackScreen.color.g,blackScreen.color.b,0);
        blackScreenText.text = text;
        blackScreenText.DOFade(1, duration);
        blackScreen.DOFade(1, duration);
    }
    public void BlackScreenFadeOut(string text,float duration,Action onComplete){
        blackScreen.gameObject.SetActive(true);
        blackScreen.color = new Color(blackScreen.color.r,blackScreen.color.g,blackScreen.color.b,1);
        blackScreenText.text = text;
        blackScreenText.DOFade(0, duration);
        blackScreen.DOFade(0, duration).OnComplete(() => {
            blackScreen.gameObject.SetActive(false);
            onComplete?.Invoke();
        });
    }
    public void BlackScreenFadeOut(string text,float duration){
        blackScreen.gameObject.SetActive(true);
        blackScreen.color = new Color(blackScreen.color.r,blackScreen.color.g,blackScreen.color.b,1);
        blackScreenText.text = text;
        blackScreenText.DOFade(0, duration);
        blackScreen.DOFade(0, duration).OnComplete(() => {
            blackScreen.gameObject.SetActive(false);
        });
    }
}

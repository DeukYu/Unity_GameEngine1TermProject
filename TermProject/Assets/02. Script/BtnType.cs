using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;
    public Transform buttonScale;
    Vector3 defaultScale;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;

    public TextMeshProUGUI SoundBtntext;

    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }

    bool isSound;
    public void OnBtnClick()
    {
        switch(currentType)
        {
            case BTNType.New:
                SceneLoad.LoadSceneHandle("InGameScene", 0);
                //Debug.Log("Start");
                break;
            case BTNType.Continue:
                SceneLoad.LoadSceneHandle("InGameScene", 1);
                //Debug.Log("Continue");
                break;
            case BTNType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                Debug.Log("Back");
                break;
            case BTNType.Option:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                Debug.Log("Option");
                break;
            case BTNType.Sound:
                if(isSound)
                {
                    SoundBtntext.text = "Sound On";
                    Debug.Log("사운드 OFF");
                }
                else
                {
                    SoundBtntext.text = "Sound Off";
                    Debug.Log("사운드 ON");
                }
                isSound = !isSound;
                break;
            case BTNType.Quit:
                Application.Quit();
                Debug.Log("Exit");
                break;
        }
    }

    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}

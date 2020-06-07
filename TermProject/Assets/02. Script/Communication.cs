using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Communication : MonoBehaviour
{
    public Camera cam;
    public Text Script;
    public Animator Communication_Animator;

    public bool IsTalk;

    int Talk_Count;

    RaycastHit hitInfo;

    string NPC_1 = "안녕하세요? NPC 입니다.";
    string NPC_2 = "게임엔진 텀프 개객기";

    // Update is called once per frame
    void Update()
    {
        Talk();
        Communication_Setting();
    }

    void Talk()
    {
        Vector3 MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        if (Physics.Raycast(cam.ScreenPointToRay(MousePos), out hitInfo, 4))
        {
            if(hitInfo.transform.CompareTag("NPC"))
            {
                if(Input.GetKeyDown(KeyCode.F))
                {
                    Communication_Start();
                }
            }
        }
    }

    void Communication_Start()
    {
        Communication_Animator.SetBool("Appear", true);
        IsTalk = true;
        Talk_Count = 1;
    }

    void Communication_End()
    {
        Communication_Animator.SetBool("Appear", false);
        IsTalk = false;
        Talk_Count = 0;
    }

    void Communication_Setting()
    {
        if(IsTalk == true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Talk_Count++;
            }
        }

        if(Talk_Count == 0)
        {
            Script.text = "";
        }

        Vector3 MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        if(Physics.Raycast(cam.ScreenPointToRay(MousePos), out hitInfo, 4))
        {
            if(hitInfo.transform.gameObject.name == "NPC")
            {
                if(Talk_Count == 1)
                {
                    Script.text = NPC_1;
                }
                else if(Talk_Count == 2)
                {
                    Script.text = NPC_2;
                }
                else if(Talk_Count == 3)
                {
                    Communication_End();
                }
            }
        }
    }
}

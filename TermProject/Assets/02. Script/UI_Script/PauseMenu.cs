using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUi;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //if (!GameManager.isPause)
                CallMenu();
            //else
                CloseMenu();
        }
    }

    public void CallMenu()
    {
        //GameObject.isPause = true;
        go_BaseUi.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        //GameObject.isPause = false;
        go_BaseUi.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ClickSave()
    {
        Debug.Log("Save");
    }

    public void ClickLoad()
    {
        Debug.Log("Load");
    }

    public void ClickExit()
    {
        Debug.Log("Exit");
    }
}

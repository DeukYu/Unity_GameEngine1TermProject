using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool canPlayerMove = true; // 플레이어의 움직임 제어

    public static bool isNight = false;
    public static bool isWater = false;

    public static bool isPause = false; // 메뉴가 호출되면 true

    //private WeaponManager theWM;
    private bool flag = false;

    // Update is called once per frame
    void Update()
    {
        if(isPause)
        {
            canPlayerMove = false;
        }
        else
        {
            canPlayerMove = true;
        }
    }
}

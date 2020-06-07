﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum STATUS_TYPE
{
    HP,
    MP,
    EXP,
    SP
}
public class StatusController : MonoBehaviour
{

    private int Level;

    private int hp;
    private int currentHp;

    private int mp;
    private int currentMp;

    private int currentExp;
    private int needExp;

    private int sp;
    private int currentSp;
    private int spIncreaseSpeed;
    private int spRechargeTime;
    private int currentSpRechargeTime;
    private bool spUsed;

    private int atk;
    private int dp;

    [SerializeField]
    private Slider[] Slider_Gauge;

    private const int HP = 0, MP = 1, SP = 2, EXP = 3;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = hp;
        currentMp = mp;
        currentSp = sp;
    }

    // Update is called once per frame
    void Update()
    {
        SPRechargeTime();
        SPRecover();
        GaugeUpdate();
    }

    void GaugeUpdate()
    {
        Slider_Gauge[HP].value = currentHp / hp;
        Slider_Gauge[MP].value = currentMp / mp;
        Slider_Gauge[SP].value = currentSp / sp;
        Slider_Gauge[EXP].value = currentExp / needExp;
    }

    private void SPRechargeTime()
    {
        if(spUsed)
        {
            if (currentSpRechargeTime < spRechargeTime)
                ++currentSpRechargeTime;
            else
                spUsed = false;
        }
    }

    private void SPRecover()
    {
        if(!spUsed && currentSp < sp)
        {
            currentSp += spIncreaseSpeed;
        }
    }

    public void DecreaseStamina(int _count)
    {
        spUsed = true;
        currentSpRechargeTime = 0;

        if (currentSp - _count > 0)
            currentSp -= _count;
        else
            currentSp = 0;
    }

    public int GetCurrentSP()
    {
        return currentSp;
    }
}
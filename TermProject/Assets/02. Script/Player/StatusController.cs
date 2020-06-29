using System.Collections;
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

    private int Level = 1;

    private int hp;
    private int currentHp;

    private int mp;
    private int currentMp;

    private int currentExp;
    private int needExp;

    private float sp;
    private float currentSp;
    private float spIncreaseSpeed;
    private float spRechargeTime;
    private float currentSpRechargeTime;
    private bool spUsed;

    private int atk;

    [SerializeField]
    private Slider[] Slider_Gauge;
    [SerializeField]
    private Text[] Text_Gauge;

    private const int HP = 0, MP = 1, SP = 2, EXP = 3;

    // Start is called before the first frame update
    void Start()
    {
        atk = Level * 100;
        hp = Level * 150;
        mp = Level * 50;
        sp = 100;
        spIncreaseSpeed = Time.deltaTime;
        currentExp = 0;
        needExp = Level * 100;
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

        Text_Gauge[HP].text = "HP: " + currentHp + " / " + hp;
        Text_Gauge[MP].text = "MP: " + currentMp + " / " + mp;
        Text_Gauge[SP].text = "SP: " + (int)currentSp + " / " + sp;
        Text_Gauge[EXP].text = "HP: " + currentExp + " / " + needExp;
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

    public void IncreaseHP(int _count)
    {
        if(currentHp + _count < hp)
        {
            currentHp += _count;
        }
        else
        {
            currentHp = hp;
        }
    }

    public void IncreaseMP(int _count)
    {
        if(currentMp + _count < mp)
        {
            currentMp += _count;
        }
        else
        {
            currentMp = mp;
        }
    }

    public void DecreaseHP(int _count)
    {
        currentHp -= _count;
        if (currentHp <= 0)
            Debug.Log("캐릭터의 hp가 0이 되었습니다.");
    }

    public void DecreaseMP(int _count)
    {
        currentMp -= _count;
        if (currentMp <= 0)
            Debug.Log("캐릭터의 mp가 0이 되었습니다.");
    }

    public void DecreaseStamina(float _count)
    {
        spUsed = true;
        currentSpRechargeTime = 0;

        if (currentSp - _count > 0)
            currentSp -= _count;
        else
            currentSp = 0;
    }

    public float GetCurrentSP()
    {
        return currentSp;
    }

    public int GetAtk()
    {
        return atk;
    }
}

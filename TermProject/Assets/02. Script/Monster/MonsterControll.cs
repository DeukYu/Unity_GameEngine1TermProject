using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MonsterControll : MonoBehaviour
{
    public enum CurrentState { idle, trace, attack, attackWait, hit, dead};
    public CurrentState curState = CurrentState.idle;

    private Transform Monstertransform;
    private Transform playerTransform;
    private GameObject player;
    private NavMeshAgent nvAgent;

    private float StartHP = 100;
    private float HP;

    public GameObject hpBar;
    // 데미지 텍스트
    public GameObject DamageText;
    public GameObject TextPos;

    //private Animator anim;

    //// 추적 사정거리
    public float traceDist = 15.0f;
    //// 공격 사정거리
    public float attackDist = 5.0f;

    //// 사망여부
    private bool isDead = false;

    // 애니메이션
    Animator animator;

    private void Start()
    {
        //
        HP = StartHP;
        //
        Monstertransform = gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().Monster = this.gameObject;
        nvAgent = gameObject.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        StartCoroutine(CheckState());
        StartCoroutine(CheckStateAction());
    }

    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(playerTransform.position, transform.position);
         
            if (dist <= attackDist)
            {
                curState = CurrentState.attack;
            }
            else if (dist <= traceDist)
            {
                curState = CurrentState.trace;
            }
            else
            {
                curState = CurrentState.idle;
            }
        }
    }

    IEnumerator CheckStateAction()
    {
        while (!isDead)
        {
            //print(curState);
            //nvAgent.SetDestination(playerTransform.position);
            switch (curState)
            {
                case CurrentState.idle:
                    nvAgent.isStopped = true;
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isAttacking", false);
                    //anim.CrossFade(anims.idle.name, 0.3f);
                    break;
                case CurrentState.trace:
                    nvAgent.destination = playerTransform.position;    
                    nvAgent.isStopped = false;
                    animator.SetBool("isWalking", true);
                    animator.SetBool("isAttacking", false);
                    //anim.CrossFade(anims.walk.name, 0.3f);
                    break;
                case CurrentState.attack:
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isAttacking", true);
                    //animator.SetBool("isAttacking", true);
                    // animator.SetInteger("AttackMotion", Random.Range(0, 1));
                    //anim.CrossFade(anims.attack.name, 0.3f);
                    break;
            }
            yield return null;
        }
    }

    void Animation_Update()
    {
        switch(curState)
        {
            case CurrentState.idle:
                animator.SetBool("isWalking", false);
                break;
            case CurrentState.trace:
                animator.SetBool("isWalking", true);
                break;
            case CurrentState.attack:
                animator.SetBool("isAttacking", true);
                animator.SetInteger("AttackMotion", Random.Range(0, 1));
                break;
            case CurrentState.dead:
                break;
        }
    }

    public void GetDamage(int damage)
    {
        GameObject dmgText = Instantiate(DamageText, TextPos.transform.position, Quaternion.identity);
        dmgText.GetComponent<Text>().text = damage.ToString();
        HP -= damage;
        hpBar.GetComponent<Image>().fillAmount = HP / StartHP;
        Destroy(dmgText, 1f);

        if(HP <= 0)
        {
            curState = CurrentState.dead;
            Destroy(gameObject, 2f);
            player.GetComponent<PlayerController>().Monster = null;
        }

        animator.SetTrigger("Hit");
    }

    public void Damage(int _dmg, Vector3 _targetPos)
    {

    }
}

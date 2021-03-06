﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float _CameraRotationX = 0f;
    private float _CameraRotationY = 0f;

    private float applySpeed;
    private float walkSpeed = 5f;
    private float runSpeed = 20f;

    float jumpForce = 5f;

    // [민감도]
    float LookSensitivity = 3f;

    // [카메라 회전 리미트 값]
    float CameraRotationLimit = 60f;

    private bool isWalk = false;
    private bool isRun = false;
    private bool isGround = true;
    private bool isAttack = false;

    Rigidbody Rigid;
    Animator animator;

    private RaycastHit hitInfo;

    Collider col;

    public Camera cam;
    public GameObject camera_Rotation;

    float _moveDirX;
    float _moveDirZ;

    private StatusController theStatusController;
    [SerializeField]
    private CloseWeapon currentCloseWeapon;

    public GameObject Monster;

    // [대화 관련]
    [SerializeField]
    private float talkRange;

    private bool TalkActivated = false; 

    // Start is called before the first frame update
    void Start()
    {
        Rigid = gameObject.GetComponent<Rigidbody>();
        col = gameObject.GetComponent<Collider>();
        animator = GetComponent<Animator>();
        applySpeed = walkSpeed;
        theStatusController = FindObjectOfType<StatusController>();
    }

    // Update is called once per frame
    void Update()
    {
        TryRun();
        TryAttack();
        Move();
        CheckNpc();
        TryAction();

        Camera_Rotation();

        Animation_Update();
        
    }

    void Damage(int damage)
    {

    }

    private void CheckNpc()
    {
        //if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, talkRange, layerMask))
    }

    private void TryAction()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {

        }
    }

    void Move()
    {
        _moveDirX = Input.GetAxisRaw("Horizontal");
        _moveDirZ = Input.GetAxisRaw("Vertical");

        if (!(_moveDirZ == 0 && _moveDirX == 0))
        {
            Rigid.MovePosition(transform.position + transform.forward * Time.deltaTime * applySpeed);
        }

        Quaternion Camera_rotation = cam.transform.rotation;
        Camera_rotation.x = 0;
        Camera_rotation.z = 0;

        if(_moveDirZ == 1 && _moveDirX == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Camera_rotation * Quaternion.Euler(0, 0, 0), applySpeed * Time.deltaTime);
        }
        else if (_moveDirZ == -1 && _moveDirX == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Camera_rotation * Quaternion.Euler(0, 180, 0), applySpeed * Time.deltaTime);
        }
        else if (_moveDirX == 1 && _moveDirZ == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Camera_rotation * Quaternion.Euler(0, 90, 0), applySpeed * Time.deltaTime);
        }
        else if (_moveDirX == -1 && _moveDirZ == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Camera_rotation * Quaternion.Euler(0, 270, 0), applySpeed * Time.deltaTime);
        }

        if (_moveDirZ == 1 && _moveDirX == 1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Camera_rotation * Quaternion.Euler(0, 45, 0), applySpeed * Time.deltaTime);
        }
        else if (_moveDirZ == 1 && _moveDirX == -1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Camera_rotation * Quaternion.Euler(0, 315, 0), applySpeed * Time.deltaTime);
        }
        else if (_moveDirZ == -1 && _moveDirX == 1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Camera_rotation * Quaternion.Euler(0, 135, 0), applySpeed * Time.deltaTime);
        }
        else if (_moveDirZ == -1 && _moveDirX == -1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Camera_rotation * Quaternion.Euler(0, 255, 0), applySpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        if(Monster != null)
        {
            Monster.GetComponent<MonsterControll>().GetDamage(theStatusController.GetAtk());
        }
    }

    void TryAttack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!isAttack)
            {
                StartCoroutine(HitCoroutine());
                StartCoroutine(AttackCoroutine());
            }
            //Attack();
        }
    }

    protected IEnumerator AttackCoroutine()
    {
        isAttack = true;

        yield return new WaitForSeconds(currentCloseWeapon.attackDelay - currentCloseWeapon.attackDelayA - currentCloseWeapon.attackDelayB);
        isAttack = false;
    }

    protected IEnumerator HitCoroutine()
    {
        if (hitInfo.transform.tag == "Monster")
        {
            int sumDamage = theStatusController.GetAtk() + currentCloseWeapon.damage;
            hitInfo.transform.GetComponent<MonsterControll>().Damage(sumDamage, transform.position);
        }
        else
            yield return null;
        yield return null;
    }

    //protected abstract IEnumerator HitCoroutine();

    protected bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentCloseWeapon.range))
            return true;
        return false;
    }

    void TryRun()
    {
        if(Input.GetKey(KeyCode.LeftShift) && theStatusController.GetCurrentSP() > 0)
        {
            Running();
        }
        if(Input.GetKey(KeyCode.LeftShift) || theStatusController.GetCurrentSP() <= 0)
        {
            RunningCancel();
        }
    }

    private void Running()
    {
        isRun = true;
        theStatusController.DecreaseStamina(10f * Time.deltaTime);
        applySpeed = runSpeed;
    }

    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }

    void Camera_Rotation()
    {
        float _YRotation = Input.GetAxis("Mouse X");
        float _XRotation = Input.GetAxis("Mouse Y");

        _CameraRotationX -= _XRotation * LookSensitivity;
        _CameraRotationY += _YRotation * LookSensitivity;

        _CameraRotationX = Mathf.Clamp(_CameraRotationX, -CameraRotationLimit, CameraRotationLimit);

        camera_Rotation.transform.rotation = Quaternion.Euler(camera_Rotation.transform.rotation.x + _CameraRotationX,
            camera_Rotation.transform.rotation.y + _CameraRotationY, 0);
    }

    void Animation_Update()
    {
        if (_moveDirX == 0 && _moveDirZ == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    // [상태 변수 값 반환]
    public bool GetRun() { return isRun; }
    public bool GetWalk() { return isWalk; }
    public bool GetIsGround() { return isGround; }
}

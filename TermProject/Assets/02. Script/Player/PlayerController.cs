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
    private float runSpeed = 10f;

    float jumpForce = 5f;
    float LookSensitivity = 3f;
    float CameraRotationLimit = 60f;

    float recoverTime = 0f;

    bool isRun = false;
    bool isGround = true;

    Rigidbody Rigid;
    Animator animator;

    Collider col;

    public Camera cam;
    public GameObject camera_Rotation;

    float _moveDirX;
    float _moveDirZ;

    private StatusController theStatusController;

    // Start is called before the first frame update
    void Start()
    {
        Rigid = gameObject.GetComponent<Rigidbody>();
        col = gameObject.GetComponent<Collider>();
        animator = GetComponent<Animator>();
        applySpeed = walkSpeed;
        //theStatusController = FindObjectOfType<StatusController>();
    }

    // Update is called once per frame
    void Update()
    {
        TryJump();
        TryRun();

        Move();

        Camera_Rotation();
        //Character_Jump();
        //Character_Attack();

        Animation_Update();
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

    void TryRun()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }

    private void TryJump()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.1f);

        if (Input.GetKey(KeyCode.Space) && isGround)
        {
               Jump();
        }
    }

    private void Jump()
    {
        Rigid.velocity = transform.up * jumpForce;
        //Rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void Running()
    {
        isRun = true;
        applySpeed = runSpeed;
    }

    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }

    //void Character_Jump()
    //{
    //    // Raycast(어디서, 어디에, 얼마만큼 쏠것인가?)
    //    isGround = Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.1f);

    //    if (isGround == true)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Space))
    //        {
    //            isJumping = true;
    //            if (isJumping == true)
    //            {
    //                Rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //                isJumping = false;
    //            }
    //        }
    //    }
    //}

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
}
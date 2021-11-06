using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController)), RequireComponent(typeof(LivingEntity))]
public class FPSController : MonoBehaviour
{
    [SerializeField] float walkingSpeed = 7.5f;
    [SerializeField] float runningSpeed = 11.5f;
    [SerializeField] float jumpSpeed = 8.0f;

    float maxSpeed;

    [SerializeField] float gravity = 20.0f;
    Camera playerCamera;
    [SerializeField] float lookSpeed = 2.0f;
    [SerializeField] float lookXLimit = 45.0f;

    ChangeFOV changeFOV;

    CharacterController characterController;
    LivingEntity livingEntity;


    Animator animator;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    bool canMove = true;

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        maxSpeed = Mathf.Max(walkingSpeed, runningSpeed);

        SetReferences(null, null);

        if (!animator)
            livingEntity.ModelSpawned += SetReferences;
    }

    void SetReferences(object sender, EventArgs e)
    {
        livingEntity ??= GetComponent<LivingEntity>();
        playerCamera ??= GetComponentInChildren<Camera>();
        characterController ??= GetComponent<CharacterController>();
        changeFOV ??= GetComponentInChildren<ChangeFOV>();
        animator = livingEntity?.Model ? livingEntity.Model.Animator : null;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedZ = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;

        if (isRunning)
        {
            changeFOV.SmoothChangeFOV(90, .15f);
        }
        else
        {
            changeFOV.SmoothChangeFOV(60, .2f);
        }

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedZ);


        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
            if (animator)
                animator.SetTrigger("jumping");
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }



        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        UpdateAnimatorParameters(curSpeedX / maxSpeed, curSpeedZ / maxSpeed);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    void UpdateAnimatorParameters(float velocityX, float velocityZ)
    {
        if (!animator)
            return;

        animator.SetFloat("velocityX", velocityX);
        animator.SetFloat("velocityZ", velocityZ);
    }
}

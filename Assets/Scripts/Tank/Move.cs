﻿using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Assets.Scripts.Tank
{
    public class Move : MonoBehaviour
    {
        enum MoveState { NOT_MOVING, MOVING };
        [Header("Settings")]
        [SerializeField] private float forwardSpeed = 1f;
        [SerializeField] private float additionalForwardSpeed = 0;
        [SerializeField] private float moveSmoothTime = 1f;
        [SerializeField] private float rotationSmoothTime = 1f;
        [SerializeField] private float rotaionSpeed = 1f;
        [SerializeField] private float rotationTolerance = 5;

        [Header("Debug")]
         MoveState moveState = MoveState.NOT_MOVING;
         private Vector2 inputMovementVector;
         private Vector3 targetDirection;
         private Vector3 currentDirection;
         private Camera mainCamera;
         private Vector3 currentDirctionVel;
         private float directionSmoothTime;
         private new Rigidbody rigidbody;

        private float targetDirectionDiffInAngle;
        private float targetDirectionDot;
        private int targetDirectionDotInt;
        private float targetRightDot;
        private int targetRightDotInt;
        private float currentSpeedVelocity;
        private float currentForwardSpeed;
        private float currentDirectionDotValueVelocity;
        private float currentDirectionDotValue;
        private float targetRotationTorque;
        private float currentRotationTorqueVel;
        private float currentRotationTorque;

        private void Awake()
        {
            mainCamera = Camera.main;
            rigidbody = GetComponent<Rigidbody>();
        }


        private void Update()
        {
            SetState();
            switch (moveState)
            {   
                case MoveState.NOT_MOVING:
                    break;
                case MoveState.MOVING:
                    CalcRightDot();
                    CalcDirectionDot();
                    CalcDirectionDiff();

                    updateDirection();

                    RotateTank();
                    break;
                default:
                    break;
            }
            MoveTank();
        }

        public void SetSpeed(float value) => additionalForwardSpeed = value;


        private void SetState()
        {
            if (inputMovementVector.magnitude > 0)
            {
                moveState = MoveState.MOVING;
            } else
            {
                moveState = MoveState.NOT_MOVING;
            }
        }

        private void MoveTank()
        {
            currentForwardSpeed = Mathf.SmoothDamp(
                currentForwardSpeed, 
                forwardSpeed * inputMovementVector.magnitude, 
                ref currentSpeedVelocity, 
                moveSmoothTime
                );
            currentDirectionDotValue = Mathf.SmoothDamp(
                currentDirectionDotValue,
                targetDirectionDotInt * inputMovementVector.magnitude, 
                ref currentDirectionDotValueVelocity, 
                moveSmoothTime
                );
            
            rigidbody.velocity = transform.forward * currentDirectionDotValue * (currentForwardSpeed + additionalForwardSpeed);
        }
        private void RotateTank()
        {
            float rotationSmootherValue = Mathf.Lerp(1, 0, targetDirectionDiffInAngle / 90);

            targetRotationTorque = rotaionSpeed;
            if(targetDirectionDiffInAngle < rotationTolerance)
            {
                targetRotationTorque = 0;
            }
            currentRotationTorque = Mathf.SmoothDamp(
                currentRotationTorque, 
                targetRotationTorque * inputMovementVector.magnitude, 
                ref currentRotationTorqueVel, 
                rotationSmoothTime
                );
            float torque =  currentRotationTorque  * targetRightDotInt * targetDirectionDotInt;

            rigidbody.AddRelativeTorque(new(0, torque, 0),ForceMode.VelocityChange);
             
        }

    

        private void CalcDirectionDot()
        {
            targetDirectionDot = Vector3.Dot(transform.forward, targetDirection);
            if (targetDirectionDot < 0)
            {
                targetDirectionDotInt = -1;
            }
            else
            {
                targetDirectionDotInt = 1;
            }
        }
        private void CalcRightDot()
        {
            targetRightDot = Vector3.Dot(transform.right, targetDirection);
            if (targetRightDot < 0)
            {
                targetRightDotInt = -1;
            }
            else
            {
                targetRightDotInt = 1;
            }
        }

        private void CalcDirectionDiff()
        {
            targetDirectionDiffInAngle = Vector3.Angle(transform.forward* targetDirectionDotInt, targetDirection);

        }

        public void OnMove(InputAction.CallbackContext context)
        {
            inputMovementVector = context.ReadValue<Vector2>();
        }

        private void updateDirection()
        {

            targetDirection = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0) * new Vector3(inputMovementVector.x, 0, inputMovementVector.y);
            currentDirection = Vector3.SmoothDamp(currentDirection, targetDirection, ref currentDirctionVel, directionSmoothTime);

            Debug.DrawRay(transform.position, targetDirection, Color.green);
              


        }
    }
}
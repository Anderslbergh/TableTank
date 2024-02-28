using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Tank
{
    public class Aim : MonoBehaviour
    {
        [Header("Settings")]
        //[SerializeField] private float directionSmoothTime = 1f;
        [SerializeField] private float directionSpeed = 1f;
        [SerializeField] private float additionaldirectionSpeed = 0;

        [SerializeField] public Transform turret;

        [Header("Debug")]
        private Camera mainCamera;
        private Vector2 inputMovementVector = Vector2.one;
        private Vector3 targetDirectionOnPlane;
        private Vector3 tempTransformRotation;
        private Vector3 targetDirectionOnTransform;
        private Quaternion targetLookAtRotation;
        private Quaternion currentLookAtRotation;
        private bool doUpdate;

        private void Awake()
        {
            mainCamera = Camera.main;
        }
        public void OnAim(InputAction.CallbackContext context)
        {
            doUpdate = true;
            if(context.ReadValue<Vector2>().magnitude > 0.2f)
            {
                inputMovementVector = context.ReadValue<Vector2>();
            }
        }

        public void SetSpeed(float value) => additionaldirectionSpeed = value;


        private void Update()
        {
            RotaionOnTransform();
            doUpdate = false;
        }

        private void RotaionOnTransform()
        {

            targetDirectionOnPlane = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0) * new Vector3(inputMovementVector.x, 0, inputMovementVector.y).normalized;
            tempTransformRotation = transform.rotation.eulerAngles;
            tempTransformRotation.y = 0;
            targetDirectionOnTransform = Quaternion.Euler(tempTransformRotation) * targetDirectionOnPlane;

            targetLookAtRotation = Quaternion.LookRotation(targetDirectionOnTransform);
            if (doUpdate)
            {
                currentLookAtRotation = Quaternion.Lerp(currentLookAtRotation, targetLookAtRotation, Time.deltaTime * (directionSpeed + additionaldirectionSpeed));
            }
            turret.localRotation = Quaternion.Euler(turret.localRotation.x,
                currentLookAtRotation.eulerAngles.y - transform.rotation.eulerAngles.y,
                turret.localRotation.z);

            Debug.DrawRay(turret.position, targetDirectionOnTransform, Color.yellow);
            Debug.DrawRay(turret.position, targetDirectionOnPlane, Color.red);

        }


    }
}
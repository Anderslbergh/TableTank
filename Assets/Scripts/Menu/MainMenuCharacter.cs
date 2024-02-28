using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Menu
{
    public class MainMenuCharacter : MonoBehaviour
    {
        public RuntimeAnimatorController runtimeAnimatorController;

        public Transform playerPrefab;

        public enum State { Walking, Posing};
        public State characterState = State.Walking;

        Animator animator;
        NavMeshAgent agent;
        MainCharacterSelectionController characterSelectionController;
        public float animationSpeedMultiplier;
        private float rotationSpeed = 10f;

        private void Awake()
        {
            characterSelectionController = FindObjectOfType<MainCharacterSelectionController>();    
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = runtimeAnimatorController;
        }

        public void SetDestination(Vector3 destination) {
            agent.SetDestination(destination);
        }

        private void LateUpdate()
        {
            if (Vector3.Distance(transform.position, agent.destination) <= .4f)
            {
                characterState = State.Posing;
                agent.isStopped = true;

            }
            else
            {
                characterState = State.Walking;
                agent.isStopped =false ;
            }

            float vel = characterState == State.Walking ? agent.velocity.magnitude * animationSpeedMultiplier : 0;
            animator.SetFloat("vel", vel);

            if(characterState == State.Posing)
            {
                //agent.ResetPath();

                FaceTheCamera();
            }
        }

        private void FaceTheCamera()
        {
            Vector3 rotationVector = Camera.main.transform.position - transform.position;
            rotationVector.y = 0;
            if (Vector3.Angle(rotationVector, Camera.main.transform.forward) > 10)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotationVector), Time.deltaTime * rotationSpeed);

            }
        }
    }
}
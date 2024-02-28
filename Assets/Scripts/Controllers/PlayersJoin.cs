using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Controllers
{
    public class PlayersJoin : MonoBehaviour
    {

        CinemachineTargetGroup targetGroup;
        private LevelController currentLevelController;

        private void Start()
        {
            targetGroup = FindObjectOfType<CinemachineTargetGroup>();
            currentLevelController = FindAnyObjectByType<LevelController>();
        }

        private void Update()
        {
            if(currentLevelController != null)
            {
                LevelController[] levelControllers = FindObjectsOfType<LevelController>();
                foreach(LevelController levelController in levelControllers)
                {
                    if (levelController.gameObject.activeSelf)
                    {
                        currentLevelController = levelController;
                        break;
                    }
                }

            }
        }

        public void OnPlayerJoined(PlayerInput input)
        {
            input.transform.parent = transform;
            input.gameObject.name = $"Player_{input.playerIndex}";


            targetGroup.AddMember(input.transform, 10, 2);
            input.transform.position = currentLevelController.getRandomSpawnPoints();

            input.GetComponent<Player>().initMenuPlayer();

        }


        public void OnPlayerLeft(PlayerInput input)
        {
            targetGroup.RemoveMember(input.transform);


        }
    }
}
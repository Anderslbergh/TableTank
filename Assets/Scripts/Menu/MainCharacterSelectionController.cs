using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Assets.Scripts.Menu.MainCharacterSelectionController;

namespace Assets.Scripts.Menu
{
    public class MainCharacterSelectionController : MonoBehaviour
    {
        [System.Serializable]
        public struct SelectedCharacters
        {
            public int playerIndex;
            public Transform character;
        }
        public List<Transform> playerPosition = new List<Transform>();
        public List<Light> playerLight = new List<Light>();
        public List<Transform> characters = new List<Transform>();
        public List<Transform> availableCharacter = new List<Transform>();
        public List<SelectedCharacters> selectedCharacters = new List<SelectedCharacters>();

        public Transform spawnPoint;
        public Transform QueuePoint;

        public void SpawnCharacters()
        {
            foreach(Transform character in characters)
            {
                Transform spawnedCharacter = Instantiate(character, spawnPoint);
                AddToQueue(spawnedCharacter);

            }
        }

        public Light GetPlayerLight(int playerIndex) => playerLight[playerIndex];

        void AddToQueue(Transform transform, bool first = false)
        {
            if (first)
            {
                availableCharacter.Insert(0, transform);
            }
            else
            {
                availableCharacter.Add(transform);
            }
            Vector3 randomPos = Random.insideUnitSphere * 4;
            randomPos.y = spawnPoint.position.y;
            transform.GetComponent<MainMenuCharacter>().SetDestination(QueuePoint.position + randomPos);

        }

        public void OptOut(int playerIndex)
        {
            int exisingCharacterId = selectedCharacters.FindIndex(character => character.playerIndex == playerIndex);
            if (exisingCharacterId >= 0)
            {
                AddToQueue(selectedCharacters[exisingCharacterId].character);

                selectedCharacters.RemoveAt(exisingCharacterId);
            }
        }

        public Transform SelectNextCharacter(int playerIndex, bool next = true)
        {
            int exisingCharacterId = selectedCharacters.FindIndex(character => character.playerIndex == playerIndex);
            
            Transform selectedCharacter = availableCharacter[0];
            if (!next)
            {
                selectedCharacter = availableCharacter[availableCharacter.Count -1 ];

            }
            availableCharacter.RemoveAt(0);


            if (exisingCharacterId >= 0)
            {
                AddToQueue(selectedCharacters[exisingCharacterId].character, !next);

                selectedCharacters.RemoveAt(exisingCharacterId);
            }
            selectedCharacters.Add(new SelectedCharacters {
                playerIndex = playerIndex, 
                character = selectedCharacter
            });

            selectedCharacter.GetComponent<MainMenuCharacter>().SetDestination(playerPosition[playerIndex].position);
            return selectedCharacter.GetComponent<MainMenuCharacter>().playerPrefab;
        }
    }
}
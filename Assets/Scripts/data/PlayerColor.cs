using System.Collections;
using UnityEngine;

namespace Assets.Scripts.data
{
    [CreateAssetMenu(fileName = "PlayerColor_", menuName = "ScriptableObjects/PlayerColor", order = 1)]

    public class PlayerColor : ScriptableObject
    {

        public Color color;
        public string name;
    }
}
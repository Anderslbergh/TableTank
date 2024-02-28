using System.Collections;
using UnityEngine;

namespace Assets.Scripts.data
{
    [CreateAssetMenu(fileName = "Powerup_", menuName = "ScriptableObjects/PowerUp", order = 1)]
    public class PowerUp : ScriptableObject
    {
        public enum PowerUpWeapon
        {
            NONE,
            HOMING,
            TRIPPLE
        }
        public enum PowerUpType
        {
            SPEED,
            SHIELD,
            WEAPON,
            MAGNETIC_FIELD,
            REPULSIVE_FIELD,
            TELEPORTATION,
            EXPLOSION,
            GRAVITY_WELL,
            CLONE,
            EMP,
            REPAIR
        }
        public PowerUpType powerUpType;
        public PowerUpWeapon powerUpWeapon = PowerUpWeapon.NONE;
        public Transform powerUpRepresentationTransform;
        public float duration = 5;
        public float powerUpValue = 1;

    }
}
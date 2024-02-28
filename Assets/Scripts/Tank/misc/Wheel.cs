using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tank.misc
{
    public class Wheel : MonoBehaviour
    {
        Vector3 pos;
        Quaternion rot;
        [SerializeField] WheelCollider wheelCollider;
        private void Update()
        {
            wheelCollider.GetWorldPose(out pos, out rot);
            transform.position = pos;
            transform.rotation = rot * Quaternion.Euler(90, 90, 90);
        }
    }
}
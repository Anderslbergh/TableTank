using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tank.misc
{
    public class CenterOfGravity : MonoBehaviour
    {

        [SerializeField] Transform centerOfMass;
        // Use this for initialization
        void Start()
        {
            GetComponent<Rigidbody>().centerOfMass = centerOfMass.localPosition;
        }

    }
}
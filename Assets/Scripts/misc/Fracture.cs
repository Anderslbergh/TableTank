using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
//using Parabox.CSG;

namespace Assets.Scripts.misc
{
    public class Fracture : MonoBehaviour
    {
        [SerializeField] private Vector3 point;
        [SerializeField] private float impactRadius = 1;

        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                TriggerFire();

            }
        }

        private void TriggerFire()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;
            if(Physics.Raycast(ray,out hitPoint))
            {
                point = hitPoint.point;
                FractureObjects();
            }
        }

        private void FractureObjects()
        {
            Collider[] colliders = Physics.OverlapSphere(point, impactRadius);
            foreach (Collider collider in colliders)
            {
                FractureGameObject(collider.gameObject);
            }
        }

        private void FractureGameObject(GameObject gameObject)
        {
            
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //sphere.transform.localScale = Vector3.one * impactRadius;
            sphere.transform.position = point;
            //Model result = CSG.Subtract(gameObject, sphere);

            //gameObject.GetComponent<MeshFilter>().mesh = result.mesh;
            //gameObject.GetComponent<MeshRenderer>().materials = result.materials.ToArray();
            
            //composite.name = $"{gameObject.name}_fracture";
            //composite.AddComponent<MeshFilter>().sharedMesh = result.mesh;
            //composite.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();

            //composite.transform.position = gameObject.transform.position;
            //composite.transform.rotation = gameObject.transform.rotation;

            //Destroy(sphere);
            //Destroy(gameObject);
        }

        //private void OnDrawGizmos()
        //{
        //    if (point != null)
        //    {
        //        Gizmos.DrawSphere(point, impactRadius);
        //    }
        //}
    }
}
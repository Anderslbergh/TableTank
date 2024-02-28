using System.Collections;
using UnityEngine;

namespace Assets.Scripts.PowerUp
{
    public class Explosion : PowerUpBase, PowerUpInterface
    {
        [SerializeField] private Transform explosionPrefab;
        private Transform instantiatedObject;
        [SerializeField] private float forceRadius = 5;
        [SerializeField] private float forceAtCenter = 10f;


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + Vector3.up * -1, forceRadius);
        }

        public override IEnumerator MakeHappen()
        {
            instantiatedObject = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Collider[] colliders = Physics.OverlapSphere(transform.position, forceRadius);

            foreach (Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponentInParent<Rigidbody>();
                if(rb != null)
                {
                    rb.AddExplosionForce(forceAtCenter, transform.position + Vector3.up*-1, forceRadius);
                }
            }

            yield return new WaitForSeconds(powerUpData.duration);
            UnMakeHappen();
        }

        public override void UnMakeHappen()
        {
            Destroy(instantiatedObject.gameObject);
            base.UnMakeHappen();
        }
    }
}
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.PowerUp
{
    public class GravityWell : PowerUpBase, PowerUpInterface
    {
        [SerializeField] private Transform blackHolePrefab;
        private Vector3 prevGravity;
        private Rigidbody[] rbs;
        private bool useGravityWell;
        private Vector3 holePosition;
        private Transform instantiatedObject;
        [SerializeField] private float forceMultiplier = 5;
        [SerializeField] private float forceSmoothTime = 0.5f;
        private float currentForceVel;
        private float targetForce;
        [SerializeField] private float currentForce;

        private Vector3 currentGravityVel;
        private Vector3 targetGravity;
        [SerializeField] private Vector3 currentGravity;

        private void Awake()
        {
            currentGravity = Physics.gravity;
            prevGravity = Physics.gravity;
            targetGravity = Physics.gravity;

        }

        private void Update()
        {
            currentForce = Mathf.SmoothDamp(currentForce, targetForce, ref currentForceVel, forceSmoothTime);
            currentGravity = Vector3.SmoothDamp(currentGravity, targetGravity, ref currentGravityVel, forceSmoothTime);
            
            if(prevGravity != currentGravity)
            {
                Physics.gravity = currentGravity;
            }

            if (useGravityWell)
            {
                foreach (Rigidbody rb in rbs)
                {
                    if(rb == null) { continue; }

                    Vector3 direction = (holePosition - rb.position).normalized;
                    float distance = Vector3.Distance(holePosition, rb.position);

                    rb.AddForce((direction *  currentForce)/ distance, ForceMode.Force);
                }
            }
        }

        public override IEnumerator MakeHappen()
        {
            rbs = FindObjectsOfType<Rigidbody>();
            useGravityWell = true;
            targetGravity = Vector3.zero;
            targetForce = forceMultiplier;

            Camera camera = Camera.main;

            int margin = 100;
            int startX = margin;
            int startY = margin;
            int endX = Screen.width - margin;
            int endY = Screen.height - margin;
            //Vector3 pointOnScreen = camera.ScreenToWorldPoint(new(Random.Range(startX, endX), Random.Range(startY, endY), 0));
            Ray rayOnScreen = camera.ScreenPointToRay(new(Random.Range(startX, endX), Random.Range(startY, endY), 0));
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOnScreen, out hitInfo))
            {   
                holePosition = hitInfo.point + Vector3.up * 2;
                instantiatedObject = Instantiate(blackHolePrefab, holePosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(powerUpData.duration);
            UnMakeHappen();
        }

        public override void UnMakeHappen()
        {
            targetGravity = prevGravity;
            targetForce = 0;
            Destroy(instantiatedObject.gameObject);
            useGravityWell = false;
            base.UnMakeHappen();
        }
    }
}
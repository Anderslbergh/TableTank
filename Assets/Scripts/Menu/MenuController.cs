using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class MenuController : MonoBehaviour
    {
        public RectTransform mainMenu;
        private RectTransform rectTransform;

        [Header("Menu position")]
        [SerializeField] float onScreenX = 100;
        [SerializeField] float offScreenX = -300;
        private float currentLerpTime;
        [SerializeField] private float xPosTarget;
        private float lerpTime = .3f;
        private float xPosVel;
        [SerializeField] private float currentXPos;

        virtual public void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            currentXPos = rectTransform.position.x;
        }

        void LateUpdate()
        {
            if (xPosTarget != currentXPos)
            {
                //currentXPos = Mathf.SmoothDamp(currentXPos, xPos, ref xPosVel, smoothTimeXpos); // Anvädner timescale.. dåligt!
                float startX = 0;
                float endX = 0;

                if(xPosTarget == onScreenX)
                {
                    startX = offScreenX;
                    endX = onScreenX;
                } else
                {
                    startX = onScreenX;
                    endX = offScreenX;
                }

                currentLerpTime += Time.unscaledDeltaTime;
                if (currentLerpTime > lerpTime)
                {
                    currentLerpTime = lerpTime;
                }

                float t = currentLerpTime / lerpTime;
                t = t * t * t * (t * (6f * t - 15f) + 10f);
                currentXPos = Mathf.Lerp(startX, endX, t);
                Vector3 tempPos = rectTransform.position;
                tempPos.x = currentXPos;
                rectTransform.position = tempPos;
            }
            if(currentXPos == offScreenX)
            {
                gameObject.SetActive(false);
            }
        }


        virtual public void Hide()
        {
            currentLerpTime = 0f;
            xPosTarget = offScreenX;
        }
        virtual public void Show()
        {
            gameObject.SetActive(true);
            currentLerpTime = 0f;
            xPosTarget = onScreenX;
        }
    }
}
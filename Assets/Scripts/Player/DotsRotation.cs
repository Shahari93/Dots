using UnityEngine;
using CandyCoded.HapticFeedback;

namespace Dots.GamePlay.Player
{
    public class DotsRotation : MonoBehaviour
    {
        [SerializeField] float rotationDegree;
        [SerializeField] float rotationSpeed;

        void Update()
        {
            RotateDotsAround(rotationDegree);
            CheckForTouchInput();
        }

        void CheckForTouchInput()
        {
            if (Input.touchCount != 1)
            {
                return;
            }
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began)
            {
                return;
            }

            if (SettingMenuPresenter.IsHapticOn)
            {
                HapticFeedback.MediumFeedback();
            }

            rotationDegree *= -1f;
        }

        void RotateDotsAround(float degree)
        {
            transform.RotateAround(transform.position, transform.forward, degree * Time.deltaTime * rotationSpeed);
        }
    }
}
using UnityEngine;
using CandyCoded.HapticFeedback;

namespace Dots.GamePlay.Player
{
    /// <summary>
    /// This class is responsible for the player dots rotation
    /// Using the RotateAround we keep the dots rotating all the time
    /// And if the player tap on the screen we change the rotation direction
    /// </summary>
    public class DotsRotation : MonoBehaviour
    {
        [SerializeField] float rotationDegree;
        [SerializeField] float rotationSpeed;

        void Update()
        {
            RotateDotsAround(rotationDegree);
            CheckForTouchInput();
        }

        /// <summary>
        /// Checking for player input
        /// If the player touched the screen using only 1 finger
        /// We change the rotation direction
        /// </summary>
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

        /// <summary>
        /// Keeps rotating all the time the game is on
        /// </summary>
        /// <param name="degree"> the rotation degree for the dots</param>
        void RotateDotsAround(float degree)
        {
            transform.RotateAround(transform.position, transform.forward, degree * Time.deltaTime * rotationSpeed);
        }
    }
}
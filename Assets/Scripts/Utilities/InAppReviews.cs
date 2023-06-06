using UnityEngine;
using Google.Play.Review;
using System.Collections;

namespace Dots.Utils.Reviews
{
    public class InAppReviews : MonoBehaviour
    {
        private ReviewManager _reviewManager;
        private PlayReviewInfo _playReviewInfo;

        int loseCount;

        private void Start()
        {
            loseCount = PlayerPrefs.GetInt("LoseCount");
            if (loseCount == 5 || loseCount == 8)
            {
                StartCoroutine(RequestReview());
            }
        }

        private IEnumerator RequestReview()
        {
            _reviewManager = new ReviewManager();

            // Request a reviewInfo Object
            var requestFlowOperation = _reviewManager.RequestReviewFlow();
            yield return requestFlowOperation;
            if (requestFlowOperation.Error != ReviewErrorCode.NoError)
            {
                // Log error. For example, using requestFlowOperation.Error.ToString().
                yield break;
            }

            _playReviewInfo = requestFlowOperation.GetResult();

            // Launch the InApp Review Flow
            var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
            yield return launchFlowOperation;
            _playReviewInfo = null; // Reset the object
            if (launchFlowOperation.Error != ReviewErrorCode.NoError)
            {
                // Log error. For example, using requestFlowOperation.Error.ToString().
                yield break;
            }
        }
    } 
}
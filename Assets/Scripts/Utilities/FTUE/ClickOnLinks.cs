// Add IPointerClickHandler interface to let Unity know you want to
// catch and handle clicks (or taps on Mobile)
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Dots.Utils.FTUE;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

namespace Dots.Utils.GDPR
{
    public class ClickOnLinks : MonoBehaviour, IPointerClickHandler
    {
        // URLs to open when links clicked
        private const string PRIVACY_POLICY = "https://sites.google.com/view/dots-privacy-policy/home";
        private const string TERMS_AND_CONDITIONS = "https://sites.google.com/view/dots-terms-conditions/home";

        [SerializeField, Tooltip("The UI GameObject having the TextMesh Pro component.")]
        private TMP_Text text = default;

        [SerializeField] Image gdprPanel;

        private void Awake()
        {
            AcceptGDPR.DidAcceptGdpr = Convert.ToBoolean(PlayerPrefs.GetInt("AcceptGDPR"));
            if (!AcceptGDPR.DidAcceptGdpr)
            {
                gdprPanel.gameObject.SetActive(true);
            }
            else
            {
                gdprPanel.gameObject.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

        }

        // Callback for handling clicks.
        public void OnPointerClick(PointerEventData eventData)
        {
            // First, get the index of the link clicked. Each of the links in the text has its own index.
            var linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);

            // As the order of the links can vary easily (e.g. because of multi-language support),
            // you need to get the ID assigned to the links instead of using the index as a base for our decisions.
            // you need the LinkInfo array from the textInfo member of the TextMesh Pro object for that.
            var linkId = text.textInfo.linkInfo[linkIndex].GetLinkID();

            // Now finally you have the ID in hand to decide what to do. Don't forget,
            // you don't need to make it act like an actual link, instead of opening a web page,
            // any kind of functions can be called.
            var url = linkId switch
            {
                "MyLinkID0" => TERMS_AND_CONDITIONS,
                "MyLinkID1" => PRIVACY_POLICY,
                _ => throw new System.NotImplementedException()
            };

            // Let's see that web page!
            Application.OpenURL(url);
        }
    } 
}
// Add IPointerClickHandler interface to let Unity know you want to
// catch and handle clicks (or taps on Mobile)
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOnLinks : MonoBehaviour, IPointerClickHandler
{
    // URLs to open when links clicked
    private const string PRIVACY_POLICY = "https://sites.google.com/view/dots-privacy-policy/home";
    private const string TERMS_AND_CONDITIONS = "https://sites.google.com/view/dots-terms-conditions/home";

    [SerializeField, Tooltip("The UI GameObject having the TextMesh Pro component.")]
    private TMP_Text text = default;

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
            "MyLinkID0" => PRIVACY_POLICY,
            "MyLinkID1" => TERMS_AND_CONDITIONS,
            _ => throw new System.NotImplementedException()
        };

        Debug.Log($"URL clicked: linkInfo[{linkIndex}].id={linkId}   ==>   url={url}");

        // Let's see that web page!
        Application.OpenURL(url);
    }
}
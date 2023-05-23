using UnityEngine;
using UnityEngine.UI;

namespace Dots.Settings.UI
{
    public class OpenSettingsPanel : MonoBehaviour
    {
        private Button settingsButton;
        [SerializeField] Button closeButton;
        [SerializeField] Image panel;

        private void Awake()
        {
            settingsButton = GetComponent<Button>();
            settingsButton.onClick.AddListener(OpenSettings);

            closeButton.onClick.AddListener(CloseSettings);
            panel.gameObject.SetActive(false);
        }

        private void OpenSettings()
        {
            panel.gameObject.SetActive(true);
        }

        private void CloseSettings()
        {
            panel.gameObject.SetActive(false);
        }
    }
}
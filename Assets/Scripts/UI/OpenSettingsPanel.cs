using Dots.Audio.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Dots.Settings.UI
{
    public class OpenSettingsPanel : MonoBehaviour
    {
        private Button settingsButton;
        [SerializeField] Button closeButton;
        [SerializeField] Image panel;
        [SerializeField] Image darkPanel;

        private void Awake()
        {
            settingsButton = GetComponent<Button>();
            settingsButton.onClick.AddListener(OpenSettings);
            closeButton.onClick.AddListener(CloseSettings);

            panel.gameObject.SetActive(false);
            darkPanel.gameObject.SetActive(false);
        }

        private void OpenSettings()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            darkPanel.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
        }

        private void CloseSettings()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            darkPanel.gameObject.SetActive(false);
            panel.gameObject.SetActive(false);
        }
    }
}
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Dots.Audio.Manager;

namespace Dots.Settings.UI
{
    public class OpenSettingsPanel : MonoBehaviour
    {
        Button settingsButton;
        [SerializeField] Button closeButton;
        [SerializeField] Image panel;
        [SerializeField] Image darkPanel;

        void Awake()
        {
            settingsButton = GetComponent<Button>();
            settingsButton.onClick.AddListener(OpenSettings);
            closeButton.onClick.AddListener(CloseSettings);

            panel.gameObject.SetActive(false);
            darkPanel.gameObject.SetActive(false);
        }

        void OpenSettings()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            darkPanel.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            panel.GetComponent<RectTransform>().transform.DOScale(Vector3.one, 0.5f);
        }

        void CloseSettings()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            darkPanel.gameObject.SetActive(false);
            panel.GetComponent<RectTransform>().transform.DOScale(Vector3.zero, 0.5f).OnComplete(()=> panel.gameObject.SetActive(false));
        }

        void OnDestroy()
        {
            settingsButton.onClick.RemoveListener(OpenSettings);
            closeButton.onClick.RemoveListener(CloseSettings);
        }
    }
}
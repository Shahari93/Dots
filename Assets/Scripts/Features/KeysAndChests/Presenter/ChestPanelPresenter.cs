using System;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Dots.Feature.KeyAndChest.Key.Model;
using Dots.Feature.KeyAndChest.Chest.Tap;
using TMPro;
using Dots.Ads.Init;

namespace Dots.Feature.KeyAndChest.Chest.Panel
{
    public class ChestPanelPresenter : MonoBehaviour
    {
        [SerializeField] Image chestBackground;
        [SerializeField] GameObject chestPanel;
        [SerializeField] Button continueTextButton;
        [SerializeField] TMP_Text tapToOpenText;

        public static event Action OnTapOnContinueButton;
        public static event Action OnContinueShowed;

        void OnEnable()
        {
            TapOnChest.OnTapOnChest += ShowContinueText;
            IronSourceInit.OnWatchedExtraKeys += UpdateChestPanelViewAfterRV;
            continueTextButton.onClick.AddListener(DisableChestPanel);
        }

        void Awake()
        {
            chestBackground.gameObject.SetActive(CheckIfShouldShowPanel());
            chestPanel.SetActive(CheckIfShouldShowPanel());
        }

        bool CheckIfShouldShowPanel()
        {
            return KeysModel.TotalKeys >= 3;
        }

        async void ShowContinueText(int keys)
        {
            if (keys - 1 == 0)
            {
                tapToOpenText.gameObject.SetActive(CheckIfShouldShowPanel());

                await Task.Delay(1000);
                continueTextButton.gameObject.SetActive(true);
                OnContinueShowed?.Invoke();
            }
        }

        void UpdateChestPanelViewAfterRV(int keys)
        {
            tapToOpenText.gameObject.SetActive(CheckIfShouldShowPanel());
            continueTextButton.gameObject.SetActive(false);
        }

        public void DisableChestPanel()
        {
            chestBackground.gameObject.SetActive(CheckIfShouldShowPanel());
            chestPanel.SetActive(CheckIfShouldShowPanel());
            OnTapOnContinueButton?.Invoke();
        }

        void OnDisable()
        {
            TapOnChest.OnTapOnChest -= ShowContinueText;
            IronSourceInit.OnWatchedExtraKeys -= UpdateChestPanelViewAfterRV;
            continueTextButton.onClick.RemoveListener(DisableChestPanel);
        }
    }
}
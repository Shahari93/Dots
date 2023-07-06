using System;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Dots.Feature.KeyAndChest.Key.Model;
using Dots.Feature.KeyAndChest.Chest.Tap;
using TMPro;

namespace Dots.Feature.KeyAndChest.Chest.Panel
{
    public class ChestPanelPresenter : MonoBehaviour
    {
        [SerializeField] GameObject chestPanel;
        [SerializeField] Button continueTextButton;
        [SerializeField] TMP_Text tapToOpenText;

        public static event Action OnTapOnContinueButton;

        void OnEnable()
        {
            TapOnChest.OnTapOnChest += ShowContinueText;
            continueTextButton.onClick.AddListener(DisableChestPanel);
        }

        void Awake()
        {
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
                tapToOpenText.gameObject.SetActive(false);

                await Task.Delay(1000);
                continueTextButton.gameObject.SetActive(true);
            }
        }

        public void DisableChestPanel()
        {
            chestPanel.SetActive(CheckIfShouldShowPanel());
            OnTapOnContinueButton?.Invoke();
        }

        void OnDisable()
        {
            TapOnChest.OnTapOnChest -= ShowContinueText;
            continueTextButton.onClick.RemoveListener(DisableChestPanel);
        }
    }
}
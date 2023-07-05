using UnityEngine;
using Dots.Feature.KeyAndChest.Key.Model;

namespace Dots.Feature.KeyAndChest.Chest.Panel
{
    public class OpenChestPanel : MonoBehaviour
    {
        [SerializeField] GameObject chestPanel;
        void Awake()
        {
            chestPanel.SetActive(CheckIfShouldShowPanel());
        }

        private bool CheckIfShouldShowPanel()
        {
            return KeysModel.TotalKeys >= 3;
        }
    }
}
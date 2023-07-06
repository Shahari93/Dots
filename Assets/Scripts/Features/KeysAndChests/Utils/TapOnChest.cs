using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Dots.Feature.KeyAndChest.Key.Model;
using Dots.Feature.KeyAndChest.Prizes.Holder;

namespace Dots.Feature.KeyAndChest.Chest.Tap
{
    public class TapOnChest : MonoBehaviour
    {
        [SerializeField] GraphicRaycaster m_Raycast;
        [SerializeField] EventSystem m_EventSystem;
        [SerializeField] RectTransform canvasRect;
        PointerEventData m_PointerEventData;

        private static int totalCoinsFromChests;
        public static int TotalCoinsFromChests { get => totalCoinsFromChests; set => totalCoinsFromChests = value; }

        public static event Action<int> OnTapOnChest;

        void Update()
        {
            CheckIfPlayerTappedOnChest();
        }

        void CheckIfPlayerTappedOnChest()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    m_PointerEventData = new PointerEventData(m_EventSystem);
                    m_PointerEventData.position = touch.position;
                    List<RaycastResult> results = new List<RaycastResult>();
                    m_Raycast.Raycast(m_PointerEventData, results);
                    if (results.Count > 0 && results[0].gameObject.name.Contains("Chest_") && KeysModel.TotalKeys > 0)
                    {
                        // Send event with the name/id of the chest
                        OnTapOnChest?.Invoke(KeysModel.TotalKeys);
                        KeysModel.Instance.DecreaseKeysValue();

                        //TODO: Refactor this
                        Image resultImage = results[0].gameObject.GetComponent<Image>();
                        ChestPrizeHolder chestPrizeHolder = results[0].gameObject.GetComponent<ChestPrizeHolder>();

                        resultImage.raycastTarget = false;
                        resultImage.sprite = chestPrizeHolder.prizeSO.prizeImage;

                        chestPrizeHolder.prizeText.gameObject.SetActive(true);
                        totalCoinsFromChests += chestPrizeHolder.prizeSO.prizeAmount;
                    }
                }
            }
        }
    }
}
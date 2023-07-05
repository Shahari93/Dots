using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Dots.Feature.KeyAndChest.Key.Model;
using System;
using Dots.Feature.KeyAndChest.Prizes.Holder;

namespace Dots.Feature.KeyAndChest.Chest.Tap
{
    public class TapOnChest : MonoBehaviour
    {
        [SerializeField] GraphicRaycaster m_Raycast;
        [SerializeField] EventSystem m_EventSystem;
        [SerializeField] RectTransform canvasRect;
        PointerEventData m_PointerEventData;

        public static event Action<int> OnTapOnChest;

        private void Update()
        {
            CheckIfPlayerTappedOnChest();
        }

        private void CheckIfPlayerTappedOnChest()
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
                        results[0].gameObject.GetComponent<Image>().raycastTarget = false;
                        results[0].gameObject.GetComponent<Image>().sprite = results[0].gameObject.GetComponent<ChestPrizeHolder>().prizeSO.prizeImage;
                    }
                }
            }
        }
    }
}
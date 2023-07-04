using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Dots.Feature.KeyAndChest.Chest.Tap
{
    public class TapOnChest : MonoBehaviour
    {
        [SerializeField] GraphicRaycaster m_Raycaster;
        [SerializeField] EventSystem m_EventSystem;
        [SerializeField] RectTransform canvasRect;
        PointerEventData m_PointerEventData;


        void Update()
        {

            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the game object
            m_PointerEventData.position = this.transform.localPosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            if (results.Count > 0) Debug.Log("Hit " + results[0].gameObject.name);

        }

        //Camera mainCam;

        //private void Awake()
        //{
        //    mainCam = GetComponent<Camera>();
        //}

        //private void Update()
        //{
        //    CheckIfPlayerTappedOnChest();
        //}

        //private void CheckIfPlayerTappedOnChest()
        //{
        //    if (Input.touchCount == 1)
        //    {
        //        Touch touch = Input.GetTouch(0);
        //        Ray ray = mainCam.ScreenPointToRay(touch.position);
        //        if (touch.phase == TouchPhase.Began)
        //        {
        //            CheckForRaycast(ray);
        //        }
        //    }
        //}

        //private void CheckForRaycast(Ray ray)
        //{
        //    RaycastHit hit = new RaycastHit();
        //    // Does the ray intersect any objects excluding the player layer
        //    if (Physics.Raycast(transform.position, transform.forward, out hit))
        //    {
        //        Debug.Log("Did Hit: " + hit.collider.name);
        //    }
        //    else
        //    {
        //        Debug.Log("Did not Hit");
        //    }
        //}
    }
}
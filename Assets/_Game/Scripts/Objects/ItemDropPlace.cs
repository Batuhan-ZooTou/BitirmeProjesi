using _Game.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Objects
{
    public class ItemDropPlace : BoardingItem
    {
        [SerializeField] private BoardingItemType itemDropType;
        public override void Interact(Transform interactor)
        {
            if (interactionState == InteractionState.Interactable)
            {
                GameObject item = OnBoardingManager.Instance.TryGetItemFromPlayer(itemDropType);
                if (item != null)
                {
                    canInteract = false;
                    HideBoarding();
                    OnBoardingItemAction();
                    DeHighlight();
                    item.SetActive(true);
                    item.transform.SetParent(transform);
                    item.transform.localPosition = Vector3.zero;
                }

            }
        }
        private void ItemPickedUp(BoardingItemType _itemDropType)
        {
            if (itemDropType == _itemDropType)
            {
                ShowBoarding();
            }
        }
        public override void OnDisable()
        {
            OnBoardingManager.OnIngredientPickedUp -= ItemPickedUp;
        }
        public override void OnEnable()
        {
            OnBoardingManager.OnIngredientPickedUp += ItemPickedUp;
        }
    }
}
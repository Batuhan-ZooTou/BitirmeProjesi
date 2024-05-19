using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Managers;

namespace _Game.Scripts.Objects
{
    public class BoardingItem : Collectable
    {
        [SerializeField] private GameObject boardingSymbol;
        [SerializeField] private BoardingItemType boardingItemType;
        public override void Interact(Transform interactor)
        {
            if (interactionState == InteractionState.Interactable)
            {
                canInteract = false;
                OnBoardingItemAction();
                DeHighlight();
            }
        }
        public virtual void OnEnable()
        {
            OnBoardingManager.OnChefAsksForIngredients += ShowBoarding;
        }
        public virtual void OnDisable()
        {
            OnBoardingManager.OnChefAsksForIngredients -= ShowBoarding;
        }
        public void ShowBoarding()
        {
            boardingSymbol.SetActive(true);
            canInteract = true;
        }
        public void HideBoarding()
        {
            boardingSymbol.SetActive(false);
            canInteract = false;
        }
        protected void OnBoardingItemAction()
        {
            OnBoardingManager.OnBoardingItemInteracted?.Invoke(boardingItemType, gameObject);
            switch (boardingItemType)
            {
                case BoardingItemType.Ingredient1:
                    gameObject.SetActive(false);
                    break;
                case BoardingItemType.Ingredient2:
                    gameObject.SetActive(false);
                    break;
                case BoardingItemType.Ingredient3:
                    gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}
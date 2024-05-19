using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Extensions;
using System;

namespace _Game.Scripts.Managers
{
    public enum BoardingItemType
    {
        Ingredient1,
        Ingredient2,
        Ingredient3,
        ItemDropPlace,

    }
    public class OnBoardingManager : Singleton<OnBoardingManager>
    {
        public static Action<BoardingItemType, GameObject> OnBoardingItemInteracted;
        public static Action OnChefAsksForIngredients;
        public static Action<BoardingItemType> OnIngredientPickedUp;
        public static Action ChefStartsCooking;
        public Dictionary<BoardingItemType, GameObject> playerInventory = new Dictionary<BoardingItemType, GameObject>();

        private int chefIngredientCount;
        private bool chefAskedForIngredients;
        private void OnEnable()
        {
            OnBoardingItemInteracted += BoardingItemInteracted;
        }
        private void OnDisable()
        {
            OnBoardingItemInteracted -= BoardingItemInteracted;
        }
        private void BoardingItemInteracted(BoardingItemType type, GameObject item)
        {
            switch (type)
            {
                case BoardingItemType.Ingredient1:
                    OnIngredientPickedUp?.Invoke(type);
                    playerInventory.Add(type, item);
                    break;
                case BoardingItemType.Ingredient2:
                    playerInventory.Add(type, item);
                    OnIngredientPickedUp?.Invoke(type);
                    break;
                case BoardingItemType.Ingredient3:
                    playerInventory.Add(type, item);
                    OnIngredientPickedUp?.Invoke(type);
                    break;
                case BoardingItemType.ItemDropPlace:
                    chefIngredientCount++;
                    if (chefIngredientCount==3)
                    {
                        ChefStartsCooking?.Invoke();
                    }
                    break;
                default:
                    break;
            }
        }
        public GameObject TryGetItemFromPlayer(BoardingItemType itemToGet)
        {
            if (playerInventory.TryGetValue(itemToGet,out GameObject item))
            {
                return item;
            }
            return null;
        }
        public void ChefAsksForIngredients()
        {
            if (!chefAskedForIngredients)
            {
                OnChefAsksForIngredients?.Invoke();
                chefAskedForIngredients = true;
            }
        }
    }
}
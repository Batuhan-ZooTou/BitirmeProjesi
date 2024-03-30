using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Extensions;
using UnityEngine.InputSystem;
using _Game.Scripts.Managers;

namespace _Game.Scripts.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private Player player;
        [Header("Player Interaction")]
        [SerializeField] private float interactDistance;
        [SerializeField] private float highlightDistance;
        [SerializeField] private LayerMask interactables;
        [SerializeField] private IInteractable highlightedInteractable;
        private Transform mainCamera;
        private Ray ray;
        private RaycastHit[] hits = new RaycastHit[16];
        private void Start()
        {
            mainCamera = Camera.main.transform;
            player.inputManager = InputManager.Instance;
            player.inputManager.playerInputAsset.Player.Interact.started += Interact;
        }
        private void OnDisable()
        {
            player.inputManager.playerInputAsset.Player.Interact.started -= Interact;
        }
        private void Update()
        {
            ray = new Ray(mainCamera.position, mainCamera.forward);
            SearchForInteractables();
        }
        private void SearchForInteractables()
        {
            //cast ray to search Interactables
            int hitCount = Physics.RaycastNonAlloc(ray, hits, highlightDistance, interactables);
            //if there is none dehighlight last one
            if (hitCount == 0)
            {
                if (highlightedInteractable.IsNull())
                {
                    return;
                }
                highlightedInteractable.DeHighlight();
                highlightedInteractable = null;
                return;
            }
            IInteractable interactableToHighlight = null;
            float distanceToCloser = highlightDistance;
            //loop all hits
            for (int i = 0; i < hitCount; i++)
            {
                if (hits[i].transform.TryGetComponent(out interactableToHighlight))
                {
                    float distanceToPlayer = Mathf.Abs(Vector3.Distance(player.transform.position, hits[i].transform.position));
                    //get closer ones only
                    if (distanceToPlayer < distanceToCloser)
                    {
                        //found closer one
                        distanceToCloser = distanceToPlayer;
                    }
                }
            }
            if (interactableToHighlight.IsNull())
            {
                highlightedInteractable.DeHighlight();
                highlightedInteractable = null;
                return;
            }
            //if there is no previous one
            if (highlightedInteractable.IsNull())
            {
                highlightedInteractable = interactableToHighlight;
                //Highlight it
                highlightedInteractable.Highlight();
                //if close enough allow interaction
                if (distanceToCloser < interactDistance)
                {
                    highlightedInteractable.AllowInteraction();
                }
            }
            else
            {
                //new interactable
                if (highlightedInteractable != interactableToHighlight)
                {
                    highlightedInteractable.DeHighlight();
                    highlightedInteractable = interactableToHighlight;
                    //Highlight it
                    highlightedInteractable.Highlight();
                    //if close enough allow interaction
                    if (distanceToCloser < interactDistance)
                    {
                        highlightedInteractable.AllowInteraction();
                    }
                }
                //same interactable
                else
                {
                    //if close enough allow interaction
                    if (distanceToCloser < interactDistance)
                    {
                        highlightedInteractable.AllowInteraction();
                    }
                    else
                    {
                        highlightedInteractable.DisAllowInteraction();
                    }
                }
            }
        }
        private void Interact(InputAction.CallbackContext context)
        {
            //check if there is highlighted one
            if (highlightedInteractable.IsNull())
            {
                return;
            }
            highlightedInteractable.Interact();
            //dehiglight
            highlightedInteractable = null;
        }
    }
}
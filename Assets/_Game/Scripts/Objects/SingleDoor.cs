using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace _Game.Scripts.Objects
{
    public class SingleDoor : Door
    {
        [SerializeField] private Transform door;
        [SerializeField] private DoorReferenceRotations doorPositions;
        [SerializeField] private float openDuration;
        public override void OpenDoor()
        {
            base.OpenDoor();
            transform.DOKill();
        }
        public override void CloseDoor()
        {
            base.CloseDoor();
            transform.DOKill();
            door.DOLocalRotate(doorPositions.closeTransform.localEulerAngles, openDuration).SetEase(Ease.OutQuad).OnComplete(() => { SetInteractablty(true); });
        }
        public void OpenInwards()
        {
            OpenDoor();
            door.DOLocalRotate(doorPositions.openInwardsTransform.localEulerAngles, openDuration).SetEase(Ease.OutQuad).OnComplete(() => { SetInteractablty(true); });
        }
        public void OpenOutwards()
        {
            OpenDoor();
            door.DOLocalRotate(doorPositions.openOutwardsTransform.localEulerAngles, openDuration).SetEase(Ease.OutQuad).OnComplete(() => { SetInteractablty(true); });
        }
        public override void Interact(Transform interactor)
        {
            if (interactionState == InteractionState.Interactable)
            {
                canInteract = false;
                DeHighlight();
                float direction = Vector3.SignedAngle(interactor.position - transform.position, transform.right, Vector3.up);
                switch (doorState)
                {
                    case DoorState.Open:
                        CloseDoor();
                        break;
                    case DoorState.Close:
                        if (direction >= 0)
                        {
                            OpenInwards();
                        }
                        else
                        {
                            OpenOutwards();
                        }
                        OpenDoor();
                        break;
                }
            }
        }
    }
}
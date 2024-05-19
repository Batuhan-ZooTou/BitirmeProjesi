using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using _Game.Scripts.Player;


namespace _Game.Scripts.Objects
{
     
    public class SlideDoor :Door
    {
        [SerializeField]private Transform rightDoor;
        [SerializeField]private Transform leftDoor;
        [SerializeField] private DoorReferencePositions rightDoorPositions;
        [SerializeField] private DoorReferencePositions leftDoorPositions;
        [SerializeField] private float openDuration;
        public override void Start()
        {
        }
        public override void OpenDoor()
        {
            base.OpenDoor();
            transform.DOKill();
            rightDoor.DOMove(rightDoorPositions.openTransform.position, openDuration).SetEase(Ease.OutQuad);
            leftDoor.DOMove(leftDoorPositions.openTransform.position, openDuration).SetEase(Ease.OutQuad).OnComplete(() => { SetInteractablty(true); });
        }
        public override void CloseDoor()
        {
            base.CloseDoor();
            transform.DOKill();
            rightDoor.DOMove(rightDoorPositions.closeTransform.position, openDuration).SetEase(Ease.OutQuad);
            leftDoor.DOMove(leftDoorPositions.closeTransform.position, openDuration).SetEase(Ease.OutQuad).OnComplete(()=> { SetInteractablty(true);});
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                OpenDoor();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                CloseDoor();
            }
        }

    }
}
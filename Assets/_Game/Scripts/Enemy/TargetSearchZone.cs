using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Player;
namespace _Game.Scripts.Enemy
{
    public class TargetSearchZone : MonoBehaviour
    {
        [SerializeField] private bool hasTarget;
        [SerializeField] private Transform currentTarget;
        private PlayerMovement targetMovement;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out targetMovement))
            {
                hasTarget = true;
                currentTarget = targetMovement.transform;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out targetMovement))
            {
                hasTarget = false;
                currentTarget = null;
            }
        }
        public Vector3 GetTargetCalculatedForwardPosition()
        {
            return targetMovement.GetForwardPosition();
        }
        public Transform GetTarget()
        {
            return currentTarget;
        }
        public bool HasTarget()
        {
            return hasTarget;
        }
    }
}
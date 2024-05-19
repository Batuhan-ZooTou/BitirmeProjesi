using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Extensions;
using _Game.Scripts.Enemy;
using _Game.Scripts.Combat;

namespace _Game.Scripts.Helper
{
    public class RagdollController : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] ragdollPieces;
        [SerializeField] private CoreAnimationController animationController;
        [ButtonGroup("Ragdoll")]
        public void FillRagdollPieces()
        {
            ragdollPieces = GetComponentsInChildren<Rigidbody>();
        }
        [ButtonGroup("Ragdoll")]
        public void DeActivateRagdollPieces()
        {
            foreach (var piece in ragdollPieces)
            {
                piece.isKinematic = true;
                piece.useGravity = false;
            }
        }
        [ButtonGroup("Ragdoll")]
        public void ActivateRagdollPieces()
        {
            animationController.SetAnimator(false);
            foreach (var piece in ragdollPieces)
            {
                piece.isKinematic = false;
                piece.useGravity = true;
            }
        }
        public void ActivateWithForce(Vector3 direction,float power)
        {
            animationController.SetAnimator(false);
            foreach (var piece in ragdollPieces)
            {
                piece.isKinematic = false;
                piece.useGravity = true;
            }
            ragdollPieces[Random.Range(0, ragdollPieces.Length)].AddForce(direction*power*10,ForceMode.Impulse);
        }
    }
}
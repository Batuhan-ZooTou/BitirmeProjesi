using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Extensions;
using _Game.Scripts.Enemy;

namespace _Game.Scripts.Helper
{
    public class RagdollController : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] ragdollPieces;
        [SerializeField] private EnemyAnimationController enemy;
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
            enemy.Animator.enabled = false;
            foreach (var piece in ragdollPieces)
            {
                piece.isKinematic = false;
                piece.useGravity = true;
            }
        }
        public void ActivateWithForce(Vector3 direction,float power)
        {
            enemy.Animator.enabled = false;
            foreach (var piece in ragdollPieces)
            {
                piece.isKinematic = false;
                piece.useGravity = true;
            }
            ragdollPieces[Random.Range(0, ragdollPieces.Length)].AddForce(direction*power*10,ForceMode.Impulse);
        }
    }
}
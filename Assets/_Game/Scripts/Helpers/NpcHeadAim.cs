using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using _Game.Scripts.Player;
using _Game.Scripts.Extensions;
using DG.Tweening;
namespace _Game.Scripts.Helper
{
    public class NpcHeadAim : MonoBehaviour
    {
        [SerializeField] private Rig rig;
        [SerializeField] private Transform lookPoint;
        [SerializeField] private float lookTime = 1;
        private Transform lookTarget;
        public void LookAtTarget(Vector3 target)
        {
            lookPoint.position = target;
        }
        private void Update()
        {
            if (lookTarget!=null)
            {
                LookAtTarget(lookTarget.position);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerInteractor player))
            {
                lookTarget = player.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head);
                transform.DOKill();
                DOVirtual.Float(0, 1, lookTime, (float lerp) =>
                {
                    rig.weight = lerp;
                });
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerInteractor player))
            {
                lookTarget = null;
                transform.DOKill();
                DOVirtual.Float(1, 0, lookTime, (float lerp) =>
                {
                    rig.weight = lerp;
                });
            }
        }
    }
}
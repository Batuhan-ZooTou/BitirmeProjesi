using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Objects;
using _Game.Scripts.Extensions;
namespace _Game.Scripts.Enemy
{
    public class RangedEnemyCombat : EnemyCombat
    {
        [SerializeField] private ProjectileThrower projectileThrower;
        [SerializeField] private Transform projectileReleasePosition;
        public bool ShouldMoveAwayFromTarget()
        {
            return Vector3.Distance(enemy.currentLookTarget.position, transform.position)< targetReachRange;
        }
        public override bool CanReachToTarget()
        {
            return Vector3.Distance(enemy.currentLookTarget.position, transform.position) < projectileThrower.maxThrowRange;
        }
        public override void Attack()
        {
            base.Attack();
            if (canAttack)
            {
                float distance = Vector3.Distance(enemy.currentLookTarget.position, transform.position);
                float offset = distance.ClampedRemap(0, projectileThrower.maxThrowRange, 0, 1);
                projectileThrower.ThrowProjectile(projectileReleasePosition.position, enemy.currentLookTarget.position+(enemy.targetSearchZone.GetTargetCalculatedForwardPosition()* offset));
                canAttack = false;
            }
        }
    }
}
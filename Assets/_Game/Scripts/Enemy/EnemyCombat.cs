using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Enemy
{
    public class EnemyCombat : MonoBehaviour
    {
        [SerializeField] protected Enemy enemy;
        [SerializeField] protected float attackInterval;
        [SerializeField] protected float targetReachRange;
        [SerializeField] protected bool canAttack;
        private WaitForSeconds attackRoutineInterval;
        private void Start()
        {
            attackRoutineInterval = new WaitForSeconds(attackInterval);
        }
        public virtual bool CanReachToTarget()
        {
            return Vector3.Distance(enemy.currentLookTarget.position, transform.position) < targetReachRange;
        }
        public virtual void Attack()
        {
            if (!canAttack)
                return;
            StartCoroutine(ResetAttackState());
        }
        public virtual IEnumerator ResetAttackState()
        {
            yield return attackRoutineInterval;
            canAttack = true;
        }

    }
}
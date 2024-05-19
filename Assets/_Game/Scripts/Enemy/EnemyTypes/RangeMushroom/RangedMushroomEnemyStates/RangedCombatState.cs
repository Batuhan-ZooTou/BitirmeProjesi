using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Enemy
{
    public class RangedCombatState : IState
    {
        private RangedMushroomEnemy _agent;
        private RangedEnemyCombat rangedCombat;
        public RangedCombatState(RangedMushroomEnemy agent)
        {
            _agent = agent;
            rangedCombat = _agent.combat as RangedEnemyCombat;

        }
        public void Update()
        {
            if (rangedCombat.ShouldMoveAwayFromTarget())
            {
                Vector3 dir = _agent.transform.position - _agent.currentLookTarget.position;
                _agent.movement.ContinueMovement();
                _agent.movement.MoveTowardsTarget(_agent.transform.position+dir, _agent.currentLookTarget.position);
            }
            else if (rangedCombat.CanReachToTarget())
            {
                _agent.movement.StopMovement();
                _agent.movement.MoveTowardsTarget(_agent.currentDestination.position, _agent.currentLookTarget.position);
                rangedCombat.Attack();
            }
        }
        public void FixedUpdate()
        {
        }

        public void OnEnter()
        {
            Debug.Log("Attacking", _agent.gameObject);
        }

        public void OnExit()
        {

        }
    }
}
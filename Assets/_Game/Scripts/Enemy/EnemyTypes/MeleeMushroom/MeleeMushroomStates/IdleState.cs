using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Enemy
{
    public class IdleState : IState
    {
        MeleeMushroomAgent _agent;
        public IdleState(MeleeMushroomAgent agent)
        {
            _agent = agent;
        }
        public void OnEnter()
        {
            Debug.Log("idle", _agent.gameObject);

        }
        public void OnExit()
        {
        }

        public void Update()
        {
        }

        public void FixedUpdate()
        {
        }
    }
}
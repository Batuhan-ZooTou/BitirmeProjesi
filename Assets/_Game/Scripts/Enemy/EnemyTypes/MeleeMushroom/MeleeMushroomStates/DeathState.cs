using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Enemy
{
    public class DeathState : IState
    {
        MeleeMushroomAgent _agent;
        public DeathState(MeleeMushroomAgent agent)
        {
            _agent = agent;
        }
        public void OnEnter()
        {
            Debug.Log("dead", _agent.gameObject);
        }
        
        public void FixedUpdate()
        {
        }
        public void OnExit()
        {
        }
        public void Update()
        {
        }
    }
}
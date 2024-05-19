using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Enemy
{
    public class DeathState : IState
    {
        Enemy _agent;
        public DeathState(Enemy agent)
        {
            _agent = agent;
        }
        public void OnEnter()
        {
            Debug.Log("dead", _agent.gameObject);
            _agent.hitbox.enabled = false;
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
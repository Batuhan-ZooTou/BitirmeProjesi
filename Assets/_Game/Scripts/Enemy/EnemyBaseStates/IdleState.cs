using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Enemy
{
    public class IdleState : IState
    {
        Enemy _agent;
        public IdleState(Enemy agent)
        {
            _agent = agent;
        }
        public void OnEnter()
        {

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
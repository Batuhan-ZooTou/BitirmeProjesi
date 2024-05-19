using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Enemy
{
    public class PatrolAreaState : IState
    {
        MeleeMushroomAgent _meleeMushroomAgent;
        public PatrolAreaState(MeleeMushroomAgent meleeMushroomAgent)
        {
            _meleeMushroomAgent = meleeMushroomAgent;
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
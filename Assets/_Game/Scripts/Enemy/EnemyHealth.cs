using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Combat;
namespace _Game.Scripts.Enemy
{
    public class EnemyHealth : CoreHealth
    {
        [SerializeField]Enemy enemy;
        public override void OnDeath()
        {
        }
    }
}
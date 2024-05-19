using _Game.Scripts.Combat;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerHealth : CoreHealth
    {
        [SerializeField]private Player player;
        public override void DeathFeedback()
        {
            
        }
    }
}
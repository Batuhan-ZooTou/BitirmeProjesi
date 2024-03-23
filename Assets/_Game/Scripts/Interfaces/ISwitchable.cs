using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Interfaces
{
    public interface ISwitchable
    {
        public bool IsActive { get; }
        public void Activate();
        public void Deactivate();
    }
}
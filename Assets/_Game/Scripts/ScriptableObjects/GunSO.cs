using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using _Game.Scripts.Objects;

namespace _Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Guns",menuName ="Guns/Gun")]
    public class GunSO : ScriptableObject
    {
        [Title("Projectile Values")]
        public string projectileId;
        [field: SerializeField] public Projectile projectilePrefab { get; private set; }
        [field: SerializeField] public float baseDamage { get; private set; }
        [field: SerializeField] public float baseTravelSpeed { get; private set; }
        [field: SerializeField] public float baseMaxRange { get; private set; }

        [field: SerializeField] public int defaultCapacity { get; private set; }
        [field: SerializeField] public int maxSize { get; private set; }

        [Title("Gun Values")]
        public string GunId;
        [field: SerializeField] public float baseRateOfFire { get; private set; }
        [Tooltip("Can hold to fire")]
        [field: SerializeField] public bool isSingleShot { get; private set; }
    }
}
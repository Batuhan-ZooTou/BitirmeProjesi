using _Game.Scripts.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using _Game.Scripts.Interfaces;

namespace _Game.Scripts.Objects
{
    public class Projectile : MonoBehaviour
    {
        public IObjectPool<Projectile> projectilePool { private get; set; }
        [SerializeField] private Rigidbody projectileRigidbody;
        public float baseDamage { private get;  set; }
        public float baseTravelSpeed { private get;  set; }
        public float baseMaxRange { private get; set; }
        public Vector3 spawnPoint { private get; set; }
        public Vector3 travelDirection { private get; set; }
        public void SetProjectileValues(float _baseDamage, float _baseTravelSpeed, float _baseMaxRange)
        {
            baseDamage = _baseDamage;
            baseTravelSpeed = _baseTravelSpeed;
            baseMaxRange = _baseMaxRange;
        }
        void FixedUpdate()
        {
            if (Vector3.Distance(spawnPoint, transform.position) < baseMaxRange)
            {
                projectileRigidbody.velocity = new Vector3(travelDirection.x * baseTravelSpeed, travelDirection.y * baseTravelSpeed, travelDirection.z * baseTravelSpeed);
            }
            else
            {
                projectilePool.Release(this);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(baseDamage,travelDirection);
            }
            projectilePool.Release(this);
        }
    }
}
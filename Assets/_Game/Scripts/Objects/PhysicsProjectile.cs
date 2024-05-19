using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Helper;
using _Game.Scripts.Extensions;
using UnityEngine.Pool;
using _Game.Scripts.Interfaces;

namespace _Game.Scripts.Objects
{
    public class PhysicsProjectile : MonoBehaviour
    {
        public IObjectPool<PhysicsProjectile> projectilePool { private get; set; }
        public float baseDamage { private get; set; }
        public float baseTravelSpeed { private get; set; }
        [SerializeField] private TrailRenderer trailRenderer;
        [SerializeField] private Collider hitbox;
        float height;
        float lerpTime;
        float travelTime;
        bool hitSomething;
        Vector3 _startPosition;
        Vector3 _endPosition;
        QuadraticCurve _trajectory;
        Vector3 stuckRotation;
        public void ThrowProjectile(QuadraticCurve trajectory, Vector3 startPosition, Vector3 endPosition)
        {
            _trajectory = trajectory;
            _startPosition = startPosition;
            _endPosition = endPosition;
            travelTime = Mathf.Abs(Vector3.Distance(startPosition, endPosition)) / baseTravelSpeed;
            lerpTime = 1;
            hitSomething = false;
            hitbox.enabled = true;
        }
        public void SetProjectileValues(float _baseDamage, float _baseTravelSpeed, float _height)
        {
            height = _height;
            baseDamage = _baseDamage;
            baseTravelSpeed = _baseTravelSpeed;
        }
        private void Update()
        {
            if (hitSomething)
                return;
            if (lerpTime > 0)
            {
                stuckRotation = transform.eulerAngles;
                lerpTime -= Time.deltaTime / travelTime;
                transform.position = _trajectory.Evaluate(_startPosition, _endPosition, height, 1 - lerpTime);
                transform.forward = _trajectory.Evaluate(_startPosition, _endPosition, height, 1 - lerpTime + 0.0001f) - transform.position;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            hitSomething = true;
            if (other.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(baseDamage, transform.forward);
                trailRenderer.Clear();
                projectilePool.Release(this);
                return;
            }
            transform.eulerAngles = stuckRotation;
            hitbox.enabled = false;
            StartCoroutine(DeSpawn());
        }
        public IEnumerator DeSpawn()
        {
            yield return new WaitForSeconds(3);
            projectilePool.Release(this);
        }

    }
}
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Helper;
using _Game.Scripts.Extensions;
using UnityEngine.Pool;
using _Game.Scripts.Managers;

namespace _Game.Scripts.Objects
{
    public class ProjectileThrower : MonoBehaviour
    {
        public PhysicsProjectile prefab;
        public QuadraticCurve projectory;
        [SerializeField] private Collider colliderToIgnore;
        private IObjectPool<PhysicsProjectile> objectPool;
        [SerializeField] private float throwSpeed;
        [SerializeField] private float damage;
        [SerializeField, Range(0, 100)] private float accuracy;
        [SerializeField] private float defultHeight;
        [SerializeField] private float maxSpread;
        [SerializeField] public float maxThrowRange;
        private Vector3 releasePosition;
        private Vector3 targetPosition;
        private float adjustedHeight;
        private Transform _pooledObjectsHolder;

        private void Awake()
        {
            objectPool = new ObjectPool<PhysicsProjectile>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, false, 10, 100);
        }
        private PhysicsProjectile CreateProjectile()
        {
            PhysicsProjectile projectileInstance = Instantiate(prefab, _pooledObjectsHolder);
            projectileInstance.projectilePool = objectPool;
            projectileInstance.gameObject.SetActive(false);
            return projectileInstance;
        }
        private void Start()
        {
            _pooledObjectsHolder = PooledObjectsHolder.Instance.projectileHolder;
        }
        // invoked when returning an item to the object pool
        private void OnReleaseToPool(PhysicsProjectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }
        // invoked when retrieving the next item from the object pool
        private void OnGetFromPool(PhysicsProjectile projectile)
        {
            float distance = Vector3.Distance(releasePosition, targetPosition);
            float rng = Random.Range(0, 100);
            float spread = rng.ClampedRemap(accuracy, 100, 0, maxSpread);
            Vector2 accuratePoint = Random.insideUnitCircle * spread;
            adjustedHeight = distance.ClampedRemap(0, maxThrowRange, defultHeight, maxThrowRange / 2);
            projectile.transform.SetPositionAndRotation(releasePosition, Quaternion.LookRotation(targetPosition));
            Physics.IgnoreCollision(colliderToIgnore, projectile.GetComponent<Collider>(), true);
            projectile.SetProjectileValues(damage, throwSpeed, adjustedHeight);
            projectile.gameObject.SetActive(true);
            projectile.ThrowProjectile(projectory, releasePosition, targetPosition.WithAddX(accuratePoint.x).WithAddZ(accuratePoint.y));
            SoundManager.Instance.PlayOneShotSound(SoundType.Shot);
        }
        // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
        private void OnDestroyPooledObject(PhysicsProjectile projectile)
        {
            Destroy(projectile.gameObject);
        }
        public void ThrowProjectile(Vector3 _releasePosition, Vector3 _targetPosition)
        {
            releasePosition = _releasePosition;
            targetPosition = _targetPosition;
            objectPool.Get();
        }

    }
}
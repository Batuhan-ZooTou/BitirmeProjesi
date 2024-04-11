using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using _Game.Scripts.ScriptableObjects;
using _Game.Scripts.Objects;
using _Game.Scripts.Managers;

namespace _Game.Scripts.Player.Guns
{
    public class BasicGun : MonoBehaviour
    {
        [SerializeField]private GunSO gunData;
        [Tooltip("End point of gun where shots appear")]
        public Transform muzzlePosition;
        [Tooltip("Collider to bullet should not collide")]
        [SerializeField] private Collider colliderToIgnore;
        private IObjectPool<Projectile> objectPool;
        [SerializeField] private PlayerCombat holder;

        private float nextTimeToShoot;
        private Transform pooledObjectsHolder;
        private void Awake()
        {
            objectPool = new ObjectPool<Projectile>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, false, gunData.defaultCapacity, gunData.maxSize);
        }
        private void Start()
        {
            pooledObjectsHolder = PooledObjectsHolder.Instance.projectileHolder;
        }
        // invoked when creating an item to populate the object pool
        private Projectile CreateProjectile()
        {
            Projectile projectileInstance = Instantiate(gunData.projectilePrefab, pooledObjectsHolder);
            projectileInstance.projectilePool = objectPool;
            projectileInstance.gameObject.SetActive(false);
            return projectileInstance;
        }
        // invoked when returning an item to the object pool
        private void OnReleaseToPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }
        // invoked when retrieving the next item from the object pool
        private void OnGetFromPool(Projectile projectile)
        {
            Vector3 relativePos = holder.lookPosition.position - muzzlePosition.position;
            projectile.transform.SetPositionAndRotation(muzzlePosition.position, Quaternion.LookRotation(relativePos));
            projectile.spawnPoint = projectile.transform.position;
            projectile.travelDirection = projectile.transform.forward;
            Physics.IgnoreCollision(colliderToIgnore, projectile.GetComponent<Collider>(), true);
            projectile.SetProjectileValues(gunData.baseDamage, gunData.baseTravelSpeed, gunData.baseMaxRange);
            projectile.gameObject.SetActive(true);
            SoundManager.Instance.PlayOneShotSound(SoundType.Shot);
        }
        // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
        private void OnDestroyPooledObject(Projectile projectile)
        {
            Destroy(projectile.gameObject);
        }
        public void Shot()
        {
            objectPool.Get();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(muzzlePosition.position, muzzlePosition.forward*10);
        }
    }
}
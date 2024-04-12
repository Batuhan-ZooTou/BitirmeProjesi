using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using _Game.Scripts.ScriptableObjects;
using _Game.Scripts.Objects;
using _Game.Scripts.Managers;
using _Game.Scripts.Extensions;

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
        [SerializeField] private int bulletsLeft;
        [SerializeField] private GunActionState currentGunAction=GunActionState.Idle;
        private float _nextTimeToFire;
        private float _reloadTime;
        private Transform _pooledObjectsHolder;
        private void Awake()
        {
            objectPool = new ObjectPool<Projectile>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, false, gunData.defaultCapacity, gunData.maxSize);
        }
        private void Start()
        {
            _pooledObjectsHolder = PooledObjectsHolder.Instance.projectileHolder;
            _nextTimeToFire = 1 / (gunData.baseRateOfFire / 60);
            _reloadTime = gunData.reloadTime;
            bulletsLeft = gunData.magSize;
        }
        // invoked when creating an item to populate the object pool
        private Projectile CreateProjectile()
        {
            Projectile projectileInstance = Instantiate(gunData.projectilePrefab, _pooledObjectsHolder);
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
            Vector3 shootDirection = holder.lookPosition.position - muzzlePosition.position;
            shootDirection += new Vector3(
                Random.Range(-gunData.spreadAmount, gunData.spreadAmount),
                Random.Range(-gunData.spreadAmount, gunData.spreadAmount),
                Random.Range(-gunData.spreadAmount, gunData.spreadAmount));
            projectile.transform.SetPositionAndRotation(muzzlePosition.position, Quaternion.LookRotation(shootDirection));
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
            if (currentGunAction!=GunActionState.Idle)
                return;
            for (int i = 0; i < gunData.projectileCount; i++)
            {
                objectPool.Get();
            }
            bulletsLeft--;
            CheckForReload();
        }
        private void Update()
        {
            switch (currentGunAction)
            {
                case GunActionState.Idle:
                    break;
                case GunActionState.LoadingChamber:
                    LoadingNextChamber();
                    break;
                case GunActionState.Reloading:
                    if (bulletsLeft == gunData.magSize)
                    {
                        currentGunAction = GunActionState.Idle;
                    }
                    else
                    {
                        Reload();
                    }
                    break;
            }
        }
        public void SetAction(GunActionState actionToSet)
        {
            if (currentGunAction!= actionToSet)
            {
                currentGunAction = actionToSet;
            }
        }
        public bool CheckForReload()
        {
            if (bulletsLeft <= 0)
            {
                currentGunAction = GunActionState.Reloading;
                return true;
            }
            currentGunAction = GunActionState.LoadingChamber;
            return false;
        }
        public void Reload()
        {
            _reloadTime -= Time.deltaTime;
            if (_reloadTime<=0)
            {
                bulletsLeft = gunData.magSize;
                _reloadTime = gunData.reloadTime;
                currentGunAction = GunActionState.Idle;
            }
        }
        public void LoadingNextChamber()
        {
            _nextTimeToFire -= Time.deltaTime;
            if (_nextTimeToFire <= 0)
            {
                _nextTimeToFire = 1 / (gunData.baseRateOfFire / 60);
                currentGunAction = GunActionState.Idle;
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(muzzlePosition.position, muzzlePosition.forward*10);
        }
    }
}
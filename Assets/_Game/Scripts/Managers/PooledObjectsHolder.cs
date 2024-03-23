using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Extensions;
using UnityEngine.Pool;

namespace _Game.Scripts.Managers
{
    public class PooledObjectsHolder : Singleton<PooledObjectsHolder>
    {
        [Header("Projectiles")]
        public Transform projectileHolder;
        [Header("DamagePopups")]
        public DamagePopup damagePopupPrefab;
        public Transform damagePopupHolder;
        public IObjectPool<DamagePopup> damagePopupPool;
        protected override void Awake()
        {
            base.Awake();
            damagePopupPool = new ObjectPool<DamagePopup>(CreateDamagePopup, OnTakeFromPoolDamagePopup, OnReturnedToPoolDamagePopup, null, true, 100);
        }
        DamagePopup CreateDamagePopup()
        {
            DamagePopup damagePopup = Instantiate(damagePopupPrefab, transform.position, transform.rotation, damagePopupHolder);
            damagePopup.damagePopupPool = damagePopupPool;
            damagePopup.gameObject.SetActive(false);
            return damagePopup;
        }
        void OnTakeFromPoolDamagePopup(DamagePopup damagePopup)
        {
            damagePopup.gameObject.SetActive(true);
        }
        void OnReturnedToPoolDamagePopup(DamagePopup damagePopup)
        {
            damagePopup.gameObject.SetActive(false);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using _Game.Scripts.Interfaces;
using _Game.Scripts.Extensions;
using _Game.Scripts.Managers;
using _Game.Scripts.Helper;
using System;

namespace _Game.Scripts.Combat
{
    public abstract class CoreHealth : MonoBehaviour, IDamagable
    {
        [field: SerializeField] public float maxHealth { get; set; }
        [field: SerializeField] public float currentHealth { get; set; }
        [field: SerializeField] public bool isDead { get; set; }
        [field: SerializeField] public bool isInvincible { get; set; }
        [field: SerializeField] public Slider healthBar { get; set; }
        [field: SerializeField] public Slider damageTakenBar { get; set; }
        [field: SerializeField] public TextMeshProUGUI currentHealthText { get; set; }
        [field: SerializeField] public Gradient healthBarColorGradient { get; set; }
        [field: SerializeField] public Image healthBarColor { get; set; }
        [field: SerializeField] public SkinnedMeshRenderer skinnedMeshRenderer { get; set; }
        [SerializeField] private RagdollController ragdollController;
        public event Action OnDead;

        private void Start()
        {
            currentHealth = maxHealth;
            if (!ReferenceEquals(healthBar, null))
            {
                healthBar.maxValue = currentHealth;
                damageTakenBar.maxValue = currentHealth;
                healthBar.value = currentHealth;
                damageTakenBar.value = currentHealth;
                healthBarColor.color = healthBarColorGradient.Evaluate(1f);
                currentHealthText.text = currentHealth.ToString("0");
            }
        }
        public void TakeDamage(float damage,Vector3 direction)
        {
            if (isDead)
                return;
            if (isInvincible)
                return;
            currentHealth -= damage;
            DamagePopup(damage);
            HitFeedback();
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
                ragdollController.ActivateWithForce(direction,damage);
                DeathFeedback();
            }
            if (!ReferenceEquals(healthBar,null))
            {
                UpdateHealthBar();
            }
        }
        public void HitFeedback()
        {
            transform.DOKill();
            DOVirtual.Float(0, 1, 0.1f, (float hitColor) => 
            {
                skinnedMeshRenderer.material.SetFloat("_FlashAmount", hitColor); 
            }).OnComplete(()=> 
            {
                DOVirtual.Float(1, 0, 0.1f, (float hitColor) =>
                {
                    skinnedMeshRenderer.material.SetFloat("_FlashAmount", hitColor);
                });
            });
        }
        public virtual void DeathFeedback()
        {
            DOVirtual.Float(0, 1, 1f, (float disolve) =>
            {
                skinnedMeshRenderer.material.SetFloat("_DissolveAmount", disolve);
            }).SetDelay(2f).OnComplete(()=> {
                Destroy(transform.gameObject);
            });
        }
        public virtual void OnDeath()
        {

        }
        public void DamagePopup(float damage)
        {
            DamagePopup damagePopup = PooledObjectsHolder.Instance.damagePopupPool.Get();
            damagePopup.SetDamagePopup(damage, transform.position);
        }
        public void UpdateHealthBar()
        {
            if (isDead)
            {
                healthBar.transform.parent.gameObject.SetActive(false);
                transform.DOKill();
                OnDead?.Invoke();
                OnDeath();
                return;
            }
            healthBar.value = currentHealth;
            DOTween.To(() => damageTakenBar.value, x => damageTakenBar.value = x, currentHealth, 0.35f);
            healthBarColor.color = healthBarColorGradient.Evaluate(healthBar.normalizedValue);
            currentHealthText.text = currentHealth.ToString("0");
        }
        public void GetKnockback()
        {

        }
    }
}
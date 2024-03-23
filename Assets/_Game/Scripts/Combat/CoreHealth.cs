using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using _Game.Scripts.Interfaces;
using _Game.Scripts.Extensions;
using _Game.Scripts.Managers;

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
        public void TakeDamage(float damage)
        {
            if (isDead)
                return;
            if (isInvincible)
                return;
            currentHealth -= damage;
            DamagePopup damagePopup = PooledObjectsHolder.Instance.damagePopupPool.Get();
            damagePopup.SetDamagePopup(damage, transform.position);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
            if (!ReferenceEquals(healthBar,null))
            {
                UpdateHealthBar();
            }
        }
        public void UpdateHealthBar()
        {
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
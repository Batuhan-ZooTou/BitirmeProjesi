using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace _Game.Scripts.Interfaces
{
    public interface IDamagable
    {
        public float maxHealth { get; set; }
        public float currentHealth { get; set; }
        public bool isDead { get; set; }
        public bool isInvincible { get; set; }
        public Slider healthBar { get; set; }
        public Slider damageTakenBar { get; set; }
        public TextMeshProUGUI currentHealthText { get; set; }
        public Gradient healthBarColorGradient { get; set; }
        public Image healthBarColor { get; set; }
        public SkinnedMeshRenderer skinnedMeshRenderer { get; set; }
        public void TakeDamage(float damage, Vector3 direction);
        public void GetKnockback();
        public void DamagePopup(float damage);
        public void HitFeedback();
        public void UpdateHealthBar();
        public void DeathFeedback();
    }
}
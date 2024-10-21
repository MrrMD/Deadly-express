using System.Collections;
using UnityEngine;

namespace Player.AbilitySystem
{
    public abstract class TimedEffectAbility : Ability
    {
        public float effectDuration; 

        public TimedEffectAbility(string name, Sprite icon, float cooldown, float duration) : base()
        {
            AbilityName = name;
            AbilityIcon = icon;
            CooldownTime = cooldown;
            effectDuration = duration;
        }

        public override void ActivateAbility()
        {
            if (IsAbilityReady())
            {
                StartCoroutine(ApplyEffectCoroutine());
                lastUsedTime = Time.time; 
            }
            else
            {
                Debug.Log($"{AbilityName} не готова. Время до готовности: {lastUsedTime + CooldownTime - Time.time}");
            }
        }

        private IEnumerator ApplyEffectCoroutine()
        {
            ApplyEffect();  
            yield return new WaitForSeconds(effectDuration); 
            RemoveEffect();
        }

        protected abstract void ApplyEffect();
        protected abstract void RemoveEffect();
    }
}
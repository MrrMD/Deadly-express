using UnityEngine;

namespace Player.AbilitySystem
{
    public class DamageReductionAbility : TimedEffectAbility
    {
        private Player _player;
        private float damageReductionFactor;
        private TimedEffectAbility _timedEffectAbilityImplementation;

        public DamageReductionAbility(Player player, float cooldown, float duration, float reductionFactor)
            : base("Damage Reduction", null, cooldown, duration)
        {
            _player = player;
            damageReductionFactor = reductionFactor;
        }

        protected override void ApplyEffect()
        {
            _player.HealthSystem.DamageMultiplier -= damageReductionFactor; 
            Debug.Log("Урон снижен на " + damageReductionFactor);
        }

        protected override void RemoveEffect()
        {
            _player.HealthSystem.DamageMultiplier += damageReductionFactor;
            Debug.Log("Эффект снижения урона завершён");
        }

        protected override void UseAbility()
        {
            throw new System.NotImplementedException();
        }
    }
}
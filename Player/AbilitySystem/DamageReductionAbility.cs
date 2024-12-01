using UnityEngine;

namespace Player.AbilitySystem
{
    public class DamageReductionAbility : TimedEffectAbility
    {
        private Player _player;
        private static string abilityName = "Damage Reduction";
        private static float damageReductionFactor = 0.20f;
        private static float cooldown = 180f;
        private static float duration = 15f;

        public DamageReductionAbility(Player player)
            : base(abilityName, null, cooldown, duration)
        {
            _player = player;
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
            ActivateAbility();
        }
    }
}
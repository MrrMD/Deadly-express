using UnityEngine;

namespace Player.AbilitySystem
{
    public abstract class Ability : MonoBehaviour
    {
        protected string AbilityName { get; set; }
        protected Sprite AbilityIcon { get; set; }
        protected float CooldownTime { get; set; }
        protected float lastUsedTime;

        protected bool IsAbilityReady()
        {
            return Time.time >= lastUsedTime + CooldownTime;
        }

        public virtual void ActivateAbility()
        {
            if (IsAbilityReady())
            {
                UseAbility();
                lastUsedTime = Time.time; 
            }
            else
            {
                Debug.Log($"{AbilityName} не готова. Время до готовности: {lastUsedTime + CooldownTime - Time.time}");
            }
        }
        
        protected abstract void UseAbility();
    }
}
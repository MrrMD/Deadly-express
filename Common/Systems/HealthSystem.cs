using Mirror;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Systems
{
    public class HealthSystem : NetworkBehaviour
    {
        [SyncVar]
        [SerializeField] private bool isAlive;

        [FormerlySerializedAs("health")]
        [SyncVar(hook = nameof(OnHealthChanged))]
        [SerializeField] private float currentHealth;
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private PlayerAnimator playerAnimator;

        public float DamageMultiplier { get; set; } = 1f;
    
        public bool IsAlive { get => isAlive; }

        private void Start()
        {
            currentHealth = maxHealth;
            isAlive = true;
        }

        [Command]
        public void CmdTakeDamage(float damage )
        {
            var finalDamage  = damage  * damage * DamageMultiplier;

            currentHealth -= finalDamage;
            
            Debug.Log($"Игрок получил {finalDamage} урона, текущее здоровье: {currentHealth}");
            
            if (currentHealth <= 0)
            {
                isAlive = false;
                TargetRpcPlayerDied();
            }
        }

        [Command]
        public void CmdHeal(float heal)
        {
            currentHealth += heal;
            Mathf.Clamp(currentHealth, 0, 100);
        }


        [TargetRpc]
        public void TargetRpcPlayerDied()
        {
            Debug.Log("Object" + this.gameObject.name + " is dead");
        }

        private void OnHealthChanged(float oldValue, float newValue)
        {
            Debug.Log($"Health updated from {oldValue} to {newValue}");
            UpdateHealthText();
        }

        private void UpdateHealthText()
        {
        
        }
    }
}

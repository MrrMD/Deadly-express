using Mirror;
using UnityEngine;

public class HealthSystem : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private bool isAlive;

    [SyncVar(hook = nameof(OnHealthChanged))]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private PlayerAnimator playerAnimator;
    public bool IsAlive { get => isAlive; }

    private void Start()
    {
        health = maxHealth;
        isAlive = true;
    }

    public float Health { get; }
    public float MaxHealth { get; }

    
    [Command]
    public void CmdTakeDamage(float value)
    {
        health -= value;
        isAlive = health > 0;

        if (!isAlive) CmdDie();
    }

    [Command]
    public void CmdHeal(float amount)
    {
        health += amount;
        Mathf.Clamp(health, 0, 100);
    }


    [Command]
    public void CmdDie()
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

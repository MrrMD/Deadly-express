using Mirror;
using Mirror.Examples.MultipleMatch;
using UnityEngine;

public class StaminaSystem : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnStaminaChanged))]
    [SerializeField] private float stamina;
    [SerializeField] private const float maxStamina = 100;
    [SerializeField] private Player player;
    [SerializeField] private PlayerAnimator playerAnimator;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        player = GetComponent<Player>();
        stamina = maxStamina;
    }

    [Command]
    public void CmdAddStamina(float amount)
    {
        stamina += amount;
        Mathf.Clamp(stamina, 0, 100);
    }

    private void OnStaminaChanged(float oldValue, float newValue)
    {
        Debug.Log($"Stamina updated from {oldValue} to {newValue}");
    }

    private void UpdateStaminaText()
    {
        
    }
}

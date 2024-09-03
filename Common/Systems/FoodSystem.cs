using Mirror;
using UnityEngine;

public class FoodSystem : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnFoodChanged))]
    [SerializeField] private float food;
    [SerializeField] private const float maxfood = 100;
    [SerializeField] private Player player;
    [SerializeField] private PlayerAnimator playerAnimator;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        player = GetComponent<Player>();
        food = maxfood;
    }

    [Command]
    public void CmdAddFood(float amount)
    {
        food += amount;
        Mathf.Clamp(food, 0, 100);
    }

    //[TargetRpc] возможно будет нужен и тут и снизу
    private void OnFoodChanged(float oldValue, float newValue)
    {
        Debug.Log($"Food updated from {oldValue} to {newValue}");
    }

    private void UpdateFoodText()
    {
       
    }
}

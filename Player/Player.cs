using Mirror;

using UnityEngine;

[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(PlayerItemSystem))]
[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(StaminaSystem))]
[RequireComponent(typeof(FoodSystem))]
public class Player : NetworkBehaviour, IEntity
{
    [SerializeField] private string player_name;
    [SerializeField] private Role role;
    [SerializeField] private bool isOffender;

    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerItemSystem playerItemSystem;

    [SerializeField] private PlayerCameraController playerCameraController;

    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private StaminaSystem staminaSystem;
    [SerializeField] private FoodSystem foodSystem;

    public Inventory Inventory { get => inventory;}

    public override void OnStartClient()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        base.OnStartClient();

        inventory = GetComponent<Inventory>();

        healthSystem = GetComponent<HealthSystem>();
        staminaSystem = GetComponent<StaminaSystem>();
        foodSystem = GetComponent<FoodSystem>();
    }

    public HealthSystem HealthSystem { get => healthSystem;}
    public StaminaSystem StaminaSystem { get => staminaSystem; }
    public FoodSystem FoodSystem { get => foodSystem; }
    public PlayerCameraController PlayerCameraController { get => playerCameraController; set => playerCameraController = value; }


    public Role Role
    {
        set { role = value; }
        get { return role; }
    }

}

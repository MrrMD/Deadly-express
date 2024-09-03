using Mirror;

using UnityEngine;

[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(PlayerItemSystem))]
[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(StaminaSystem))]
[RequireComponent(typeof(FoodSystem))]
public class Player : NetworkBehaviour, IEntity
{
    public static Player Instance { get; private set; }

    [SerializeField] private string player_name;
    [SerializeField] private Role role;
    [SerializeField] private bool isOffender;

    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerItemSystem playerItemSystem;

    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private StaminaSystem staminaSystem;
    [SerializeField] private FoodSystem foodSystem;

    public Inventory Inventory { get => inventory;}

    private void Awake()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        inventory = GetComponent<Inventory>();

        healthSystem = GetComponent<HealthSystem>();
        staminaSystem = GetComponent<StaminaSystem>();
        foodSystem = GetComponent<FoodSystem>();
    }

    public HealthSystem HealthSystem { get => healthSystem;}
    public StaminaSystem StaminaSystem { get => staminaSystem; }
    public FoodSystem FoodSystem { get => foodSystem; }

    public Role Role
    {
        set { role = value; }
        get { return role; }
    }
}

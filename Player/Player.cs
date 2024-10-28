using Common.Systems;
using InventorySystem;
using Mirror;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Inventory))]
    [RequireComponent(typeof(PlayerItemSystem))]
    [RequireComponent(typeof(HealthSystem))]
    [RequireComponent(typeof(StaminaSystem))]
    [RequireComponent(typeof(FoodSystem))]
    public class Player : NetworkBehaviour
    {
        [SerializeField] private string player_name;
        [SerializeField] private Role role;
        public PlayerData PlayerData { get; private set; }
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
        }

        private void Start()
        {
            inventory = GetComponent<Inventory>();

            healthSystem = GetComponent<HealthSystem>();
            staminaSystem = GetComponent<StaminaSystem>();
            foodSystem = GetComponent<FoodSystem>(); 
            //PlayerData = GetComponent<PlayerData>();
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

        public void TakeDamage(float value)
        {
            Debug.Log("Player is taking damage");
            healthSystem.CmdTakeDamage(value);
        }

    }
}

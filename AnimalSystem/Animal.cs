using AnimalSystem.AnimalStates;
using InventorySystem;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

namespace AnimalSystem
{
    public class Animal : NetworkBehaviour
    {
        [SerializeField] private AnimalData animalData;
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private Inventory inventory;
        private NavMeshAgent agent; // NavMeshAgent для перемещения волка
        [SerializeField] private Player.Player player; 
        [SerializeField] private AnimalState currentState;
        private Animator animator;
        private SphereCollider detectionCollider;
        
        public HealthSystem HealthSystem { get => healthSystem; }
        public NavMeshAgent Agent { get => agent; }
        public AnimalData AnimalData { get => animalData;}
        public Animator Animator { get => animator;}
        public Player.Player Player { get => player;}

        public override void OnStartServer()
        {
            base.OnStartServer();
            inventory = GetComponent<Inventory>();
            healthSystem = GetComponent<HealthSystem>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            detectionCollider = gameObject.AddComponent<SphereCollider>();
            detectionCollider.isTrigger = true;  
            detectionCollider.radius = animalData.DetectionRadius;  
            
            ChangeState(new PatrolState(this));
        }

        [Server]
        void Update()
        {
            currentState.Update();
        }
        
        [Server]
        public void ChangeState(AnimalState newState)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }
            Debug.Log("Change state to " + newState);
            currentState = newState;
            currentState.Enter();
        }

        [Server]
        private void OnTriggerEnter(Collider other)
        {
            if(currentState.GetType() == typeof(ChaseState)) return;
            
            if (other.GetComponent<Player.Player>())
            {
                player = other.GetComponent<Player.Player>();
                Debug.Log("Игрок обнаружен в радиусе!");
                ChangeState(new ChaseState(this));
            }
        }

        [Server]
        public bool IsPlayerInAttackRadius()
        {
            return Vector3.Distance(transform.position, player.transform.position) <= animalData.AttackRadius;
        }

        [Server]
        public void TakeDamage(float value, Player.Player from)
        {
            if (player == null)
            {
                player = from;
                ChangeState(new ChaseState(this));
            }
            healthSystem.CmdTakeDamage(value);
        }
        
    }
}
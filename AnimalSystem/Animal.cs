using InventorySystem;
using UnityEngine;
using UnityEngine.AI;

namespace AnimalSystem
{
    public class Animal : MonoBehaviour
    {
        [SerializeField] private AnimalData animalData;
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private Inventory inventory;
        private NavMeshAgent agent; // NavMeshAgent для перемещения волка
        [SerializeField] private Player player; 
        [SerializeField] private AnimalState currentState;
        private Animator animator;
        private SphereCollider detectionCollider;
        
        public HealthSystem HealthSystem { get => healthSystem; }
        public NavMeshAgent Agent { get => agent; }
        public AnimalData AnimalData { get => animalData;}
        public Animator Animator { get => animator;}
        public Player Player { get => player;}


        private void Start()
        {
            inventory = GetComponent<Inventory>();
            healthSystem = GetComponent<HealthSystem>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            detectionCollider = gameObject.AddComponent<SphereCollider>();
            detectionCollider.isTrigger = true;  
            detectionCollider.radius = animalData.DetectionRadius;  
            
            ChangeState(new PatrolState(this));
        }
        
        void Update()
        {
            currentState.Update();
        }
        
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

        private void OnTriggerEnter(Collider other)
        {
            if(currentState.GetType() == typeof(ChaseState)) return;
            
            if (other.GetComponent<Player>())
            {
                player = other.GetComponent<Player>();
                Debug.Log("Игрок обнаружен в радиусе!");
                ChangeState(new ChaseState(this));
            }
        }

        public bool IsPlayerInAttackRadius()
        {
            return Vector3.Distance(transform.position, player.transform.position) <= animalData.AttackRadius;
        }

        public void TakeDamage(float value, Player from)
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
using AnimalSystem;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animal))]
public class AnimalBehavior : NetworkBehaviour
{
    [Header("Behavior Settings")]
    [SerializeField] private float detectionRadius = 10f;

    [SerializeField] private float attackDistance = 2f; 
    //[SerializeField] private float movementSpeed = 2f;
    [SerializeField] private NavMeshAgent agent;  // NavMeshAgent для перемещения волка
    [SerializeField] private Transform player; 
    [SerializeField] private Animal animal;
    private bool isAggroed = false; 

    public void Start()
    {
        animal = GetComponent<Animal>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!animal.HealthSystem.IsAlive) return;

        agent.SetDestination(NetworkClient.localPlayer.transform.position);

    }

    private void DetectPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                player = hit.transform;
                isAggroed = true;
                break;
            }
        }
    }

    private void ChasePlayer()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRadius)
        {
            isAggroed = false;
            player = null;
            return;
        }

        if (distanceToPlayer <= attackDistance)
        {
            AttackPlayer();
        }
        else
        {
            // Здесь будет логика движения с использованием NavMesh или другого подхода
            agent.SetDestination(player.position);
        }
    }

    private void AttackPlayer()
    {
        // Логика атаки игрока
        Debug.Log("Атака игрока!");
        CmdAttackPlayer(player.GetComponent<Player>());
    }

    [Command]
    private void CmdAttackPlayer(Player targetPlayer)
    {
        //Player.Instance.HealthSystem.CmdTakeDamage(animal.AtackValue);
    }

    public void Aggro()
    {
        isAggroed = true;
    }
}

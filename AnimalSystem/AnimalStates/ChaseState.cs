using AnimalSystem;

public class ChaseState : AnimalState
{
    public ChaseState(Animal animal) : base(animal) { }

    public override void Enter()
    {
        animal.Animator.SetBool("isRunning", true);
        animal.Agent.isStopped = false;
    }

    public override void Update()
    {
        animal.Agent.SetDestination(animal.Player.transform.position);

        if (animal.IsPlayerInAttackRadius())
        {
            animal.ChangeState(new AttackState(animal));
        }
    }

    public override void Exit()
    {
        animal.Animator.SetBool("isRunning", false);
        animal.Agent.ResetPath();
    }
}
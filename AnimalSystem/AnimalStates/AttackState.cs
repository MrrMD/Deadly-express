using System.Collections;
using AnimalSystem;
using UnityEngine;

public class AttackState : AnimalState
{
    private bool isAttacking = false;

    public AttackState(Animal animal) : base(animal) { }

    public override void Enter()
    {
        animal.Animator.SetTrigger("Attack");
        animal.Agent.isStopped = true;
    }

    public override void Update()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animal.StartCoroutine(AttackCooldown());
        }
    }

    public override void Exit()
    {
        animal.Agent.isStopped = false;
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(animal.AnimalData.AttackCooldown);

        if (animal.IsPlayerInAttackRadius())
        {
            animal.Player.TakeDamage(animal.AnimalData.AttackDamage);
        }

        isAttacking = false;

        if (!animal.IsPlayerInAttackRadius() && animal.Player.HealthSystem.IsAlive)
        {
            animal.ChangeState(new ChaseState(animal));
        }else if (!animal.Player.HealthSystem.IsAlive)
        {
            animal.ChangeState(new PatrolState(animal));    
        }
    }
}
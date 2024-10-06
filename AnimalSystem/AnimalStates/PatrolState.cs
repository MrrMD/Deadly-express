using UnityEngine;

namespace AnimalSystem
{
    public class PatrolState : AnimalState
    {
        private float timer;
        
        public PatrolState(Animal animal) : base(animal)
        {
        }

        public override void Enter()
        {
            animal.Animator.SetBool("isWalking", true);
            animal.Agent.isStopped = false;
            timer = animal.AnimalData.WanderTimer;  
            PatrolToRandomPoint();  
        }

        public override void Update()
        {
            timer += Time.deltaTime;
       
            if ((!animal.Agent.pathPending || animal.Agent.remainingDistance < 0.5f) && timer >= animal.AnimalData.WanderTimer)
            {
                PatrolToRandomPoint();
                timer = 0f;  
            }
        }

        public override void Exit()
        {
            animal.Animator.SetBool("isWalking", false);
        }

        private void PatrolToRandomPoint()
        {
            Vector3 randomPoint = RandomNavMeshLocation(animal.AnimalData.WanderRadius);
            animal.Agent.SetDestination(randomPoint);  
        }

        private Vector3 RandomNavMeshLocation(float radius)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;  
            randomDirection += animal.transform.position;  

            UnityEngine.AI.NavMeshHit hit;
            UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, radius, 1);
            return hit.position; 
        }
    }
}
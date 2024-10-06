namespace AnimalSystem
{
    public abstract class AnimalState
    {
        protected Animal animal;
        
        public AnimalState(Animal animal)
        {
            this.animal = animal;
        }

        public abstract void Update();

        public abstract void Enter();

        public abstract void Exit();  
    }
}
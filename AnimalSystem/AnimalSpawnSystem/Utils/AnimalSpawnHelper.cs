namespace AnimalSystem.AnimalSpawnSystem.Utils
{
    public class AnimalSpawnHelper
    {
        public static bool CanSpawnThisAnimal(AnimalSpawnPoint point, Animal animal)
        {
            var animalType = animal.AnimalData.AnimalType;
            if ((point.spawnableAnimals & animalType) != 0)
            {
                return true;  
            }

            return false; 
        }
    }
}
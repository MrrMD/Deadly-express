using System;

namespace AnimalSystem
{
    [Flags]
    public enum AnimalType
    {
        None = 0,       
        Wolf = 1 << 0,  
        Chicken = 1 << 1, 
        Bear = 1 << 2,  
        Fox = 1 << 3    
    }
}
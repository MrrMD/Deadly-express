using System;

namespace AnimalSystem
{
    [Flags]
    public enum AnimalType
    {
        None = 0,       // Никакие животные
        Wolf = 1 << 0,  // 1
        Chicken = 1 << 1, // 2
        Bear = 1 << 2,  // 4
        Fox = 1 << 3    // 8
    }
}
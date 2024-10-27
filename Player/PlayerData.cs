    using Mirror;
    using Player.RoleSystem;
    using UnityEngine;

    
    // Вынести все свойства в соответствующие компоненты
    // Тут это как седло на ослике
    
    namespace Player
    {
        public class PlayerData
        {
            public float Attack { get; } = 15f;
            public float AttackColdDown { get; } = 1f;
            private float _repairSpeedRate;
            public float AttackRadius { get; set; } = 1.5f;
            public float TrainStopRate { get; set; } = 1f;
            public float AnimalOpenSpeedRate { get; set; } = 1f;
            public float CockingSpeedRate { get; set; } = 1f;
            public float LuckyChance { get; set; } = 1f;
            public float AttackRate { get; set; } = 1f;
            public float HealRate { get; set; } = 1f;
            public float StepVolumeRate { get; set; } = 1f;
            public float RepairSpeedRate
            {
                get => _repairSpeedRate;
                set { _repairSpeedRate = value; }
            }
        }
    }

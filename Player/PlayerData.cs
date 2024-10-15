    using Mirror;
    using Player.RoleSystem;
    using UnityEngine;

    namespace Player
    {
        public class PlayerData
        {
            public float Attack { get; } = 15f;
            public float AttackColdDown { get; } = 1f;
            public float RepairSpeedRate { get; set; } = 1f;
            public float AttackRadius { get; set; } = 1.5f;
            public float TrainStopRate { get; set; } = 1f;
            public float AnimalOpenSpeedRate { get; set; } = 1f;
            public float CockingSpeedRate { get; set; } = 1f;
            public float LuckyChance { get; set; } = 1f;
            public float AttackRate { get; set; } = 1f;
            public float HealRate { get; set; } = 1f;
            public float StepVolumeRate { get; set; } = 1f;
            
        }
    }

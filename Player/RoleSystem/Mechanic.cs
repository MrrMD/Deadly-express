namespace Player.RoleSystem
{
    public class Mechanic : Role
    {
        private Player _player;

        private void Start()
        {
            RoleName = "Mechanic";
            AbilitySprite = null;
            
            _player = GetComponent<Player>();
            if (_player != null)
            {
                EditPlayerData();
            }
        }

        internal override void EditPlayerData()
        {
            if (_player.Role is Mechanic)
            {
                _player.PlayerData.RepairSpeedRate *= 1.20f;
            }
        }
        
        internal override void AbilityActivate()
        {
            if (_player.Role is Mechanic)
            {
                // Выявление неисправностей на 6 секунд.
            }
        }
    }
}
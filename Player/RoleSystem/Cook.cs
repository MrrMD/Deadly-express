namespace Player.RoleSystem
{
    public class Cook : Role
    {
        private Player _player;

        private void Start()
        {
            RoleName = "Cook";
            AbilitySprite = null;
            
            _player = GetComponent<Player>();
            if (_player != null)
            {
                EditPlayerData();
            }
        }

        internal override void EditPlayerData()
        {
            if (_player.Role is Cook)
            {
                _player.PlayerData.CockingSpeedRate *= 1.30f;
            }
        }
        
        internal override void AbilityActivate()
        {
            if (_player.Role is Cook)
            {
                //  
            }
        }
    }
}
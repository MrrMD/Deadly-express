namespace Player.RoleSystem
{
    public class Conductor : Role
    {
        private Player _player;

        private void Start()
        {
            RoleName = "Conductor";
            AbilitySprite = null;
            
            _player = GetComponent<Player>();
            if (_player != null)
            {
                EditPlayerData();
            }
        }

        internal override void EditPlayerData()
        {
            if (_player.Role is Conductor)
            {
                _player.PlayerData.StepVolumeRate *= 1.20f;
            }
        }
        
        internal override void AbilityActivate()
        {
            if (_player.Role is Conductor)
            {
                // Ключ от всего: Возможность взломать любой замок с 25% вероятностью
            }
        }
    }
}
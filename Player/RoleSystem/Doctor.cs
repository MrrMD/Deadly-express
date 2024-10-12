
namespace Player.RoleSystem
{
    public class Doctor : Role
    {
        private Player _player;

        private void Start()
        {
            RoleName = "Doctor";
            AbilitySprite = null;
            
            _player = GetComponent<Player>();
            if (_player != null)
            {
                EditPlayerData();
            }
        }

        internal override void EditPlayerData()
        {
            if (_player.Role is Doctor)
            {
                _player.PlayerData.HealRate *= 1.15f;
            }
        }

        internal override void AbilityActivate()
        {
            if (_player.Role is Doctor)
            {
                _player.HealthSystem.CmdHeal(_player.HealthSystem.MaxHealth * 0.30f);
            }
        }
    }
}


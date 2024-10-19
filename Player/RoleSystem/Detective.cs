namespace Player.RoleSystem
{
    public class Detective : Role
    {
        private Player _player;

        private void Start()
        {
            RoleName = "Detective";
            AbilitySprite = null;
            
            _player = GetComponent<Player>();
            if (_player != null)
            {
                EditPlayerData();
            }
        }

        internal override void EditPlayerData()
        {
            if (_player.Role is Detective)
            {
                _player.PlayerData.LuckyChance *= 1.20f;
            }
        }
        
        internal override void AbilityActivate()
        {
            if (_player.Role is Detective)
            {
                //  Обнаружение ловушек на 6 секунд, в радиусе 30 метров Кулдаун: 120 секунд
            }
        }
    }
}
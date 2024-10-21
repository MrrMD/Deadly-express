namespace Player.RoleSystem
{
    public class Sheriff : Role
    {
        private Player _player;

        private void Start()
        {
            RoleName = "Sheriff";
            AbilitySprite = null;
            
            _player = GetComponent<Player>();
            if (_player != null)
            {
                EditPlayerData();
            }
        }

        internal override void EditPlayerData()
        {
            if (_player.Role is Sheriff)
            {
                _player.PlayerData.AttackRate *= 1.10f;
            }
        }
        
        internal override void AbilityActivate()
        {
            if (_player.Role is Sheriff)
            {
                // Крепкий орешек: Снижение получаемого урона на 30% на 8 секунд
            }
        }
    }
}
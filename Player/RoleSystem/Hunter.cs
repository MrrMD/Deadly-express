namespace Player.RoleSystem
{
    public class Hunter : Role
    {
        private Player _player;

        private void Start()
        {
            RoleName = "Hunter";
            AbilitySprite = null;
            
            _player = GetComponent<Player>();
            if (_player != null)
            {
                EditPlayerData();
            }
        }

        internal override void EditPlayerData()
        {
            if (_player.Role is Hunter)
            {
                _player.PlayerData.AnimalOpenSpeedRate *= 1.20f;
            }
        }
        
        internal override void AbilityActivate()
        {
            if (_player.Role is Hunter)
            {
                // Потомственный следопыт: видит животных в радиусе 50 метров на 6 секунд 
            }
        }
    }
}
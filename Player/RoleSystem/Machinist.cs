namespace Player.RoleSystem
{
    public class Machinist : Role
    {
        private Player _player;

        private void Start()
        {
            RoleName = "Machinist";
            AbilitySprite = null;
            
            _player = GetComponent<Player>();
            if (_player != null)
            {
                EditPlayerData();
            }
        }

        internal override void EditPlayerData()
        {
            if (_player.Role is Machinist)
            {
                _player.PlayerData.TrainStopRate *= 1.20f;
            }
        }
        
        internal override void AbilityActivate()
        {
            if (_player.Role is Machinist)
            {
                // Глаз-алмаз: Видит препятсвия в радиусе 50 метров на 6 секунд
            }
        }
    }
}
using UnityEngine;

[CreateAssetMenu(fileName = "New Medicament", menuName = "Item/Medicament")]
public class Medicament : ItemData, IUsable
{
    [SerializeField] private float treatment;
    [SerializeField] private bool isPoisoning;
    [SerializeField] private float poisonTime;

    public void Use()
    {
        if (isPoisoning)
        {
            Player.Instance.gameObject.GetComponent<PlayerPoisonSystem>().Poison(poisonTime);
            return;
        }

        Player.Instance.HealthSystem.CmdHeal(treatment);
    }
}

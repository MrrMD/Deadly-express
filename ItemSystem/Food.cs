using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Item/Food")]
public class Food : ItemData, IUsable
{

    // ����� ������� ��������� ����������, ������� ����� �������� �� ����� ������� ICanBePoisoning � ������ isPoisoning � ������ ���

    [Header("Food Settings")]
    [SerializeField] private float saturation;
    [SerializeField] private float treatment;
    [SerializeField] private float rest;
    [SerializeField] private bool isPoisoning;
    [SerializeField] private float poisonTime;
    [SerializeField] private bool isShouldBeFried;
    [SerializeField] private bool isFried;
    public void Use()
    {
        if (isPoisoning)
        {
            //PlayerStats.Instance.CmdPoison(poisonTime);
            return;
        }

        //Player.Instance.FoodSystem.CmdAddFood(isShouldBeFried && !isFried ? saturation / 3 : saturation);
        //Player.Instance.HealthSystem.CmdHeal(isShouldBeFried && !isFried ? treatment / 3 : treatment);
        //Player.Instance.StaminaSystem.CmdAddStamina(isShouldBeFried && !isFried ? rest / 3 : rest);
    }
}

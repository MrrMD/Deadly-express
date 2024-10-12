
using System.Collections.Generic;
using UnityEngine;

public abstract class Role : MonoBehaviour{

    #region Fields

    [SerializeField] private string roleName;
    [SerializeField] private string description;
    [SerializeField] private Sprite abilitySprite;

    #endregion

    #region Properties

    public string RoleName
    {
        get { return roleName; }
        set { roleName = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public Sprite AbilitySprite
    {
        get { return abilitySprite; }
        set { abilitySprite = value; }
    }

    #endregion

    internal abstract void EditPlayerData();
    internal abstract void AbilityActivate();
}


using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour {

    #region Fields

    [SerializeField] private string role_name;
    [SerializeField] private string description;
    [SerializeField] private List<ItemData> startItems;
    [SerializeField] private ItemData specialItem ;
    [SerializeField] private Sprite[] abilities_sprites;

    #endregion

    #region Properties

    public string RoleName
    {
        get { return role_name; }
        set { role_name = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public List<ItemData> StartItems
    {
        get { return startItems; }
        set { startItems = value; }
    }

    public ItemData SpecialItem
    {
        get { return specialItem; }
        set { specialItem = value; }
    }

    public Sprite[] AbilitiesSprites
    {
        get { return abilities_sprites; }
        set { abilities_sprites = value; }
    }

    #endregion
}

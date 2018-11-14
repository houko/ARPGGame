/**
* author: xiaomo
* github: https://github.com/xiaomoinfo
* email: xiaomo@xiamoo.info
* QQ_NO: 83387856
* Desc: 
*/

public class Weapon : Item
{
    private long Attack { get; set; }

    private WeaponType WeaponType { get; set; }

    public Weapon(int id, string name, string desc, ItemType itemType, ItemQuality itemQuality, int buyPrice, int sellPrice, float weight, string icon, long attack,
        WeaponType weaponType) : base(id, name, desc, itemType, itemQuality, buyPrice, sellPrice, weight, icon)
    {
        Attack = attack;
        WeaponType = weaponType;
    }
}


public enum WeaponType
{
}

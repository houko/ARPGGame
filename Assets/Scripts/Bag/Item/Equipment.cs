/**
* author: xiaomo
* github: https://github.com/xiaomoinfo
* email: xiaomo@xiamoo.info
* QQ_NO: 83387856
* Desc: 装备
*/

using System.ComponentModel;

public class Equipment : Item
{
    /// <summary>
    /// 力量
    /// </summary>
    public int Power { get; set; }

    /// <summary>
    /// 智力
    /// </summary>
    public int Intellect { get; set; }

    /// <summary>
    /// 敏捷
    /// </summary>
    public int Agility { get; set; }

    /// <summary>
    /// 体力
    /// </summary>
    public int Stamina { get; set; }

    /// <summary>
    /// 装备类型
    /// </summary>
    private EquipmentType EquipmentType { get; set; }

    public Equipment(int id, string name, string desc, ItemType itemType, ItemQuality itemQuality, int buyPrice, int sellPrice, float weight, string icon, int power, int intellect,
        int agility, int stamina, EquipmentType equipmentType) : base(id, name, desc, itemType, itemQuality, buyPrice, sellPrice, weight, icon)
    {
        Power = power;
        Intellect = intellect;
        Agility = agility;
        Stamina = stamina;
        EquipmentType = equipmentType;
    }
}

public enum EquipmentType
{
    [Description("头部")] HEAD = 1,
    [Description("肩膀")] SHOUlDER = 2,
    [Description("上衣")] CLOTH = 3,
    [Description("手镯")] BRACELET = 4,
    [Description("戒指")] RING = 5,
    [Description("项链")] NECK = 6,
    [Description("腰带")] BELT = 7,
    [Description("下装")] PANTS = 8,
    [Description("鞋子")] SHOE = 9,
    [Description("武器")] WEAPON = 10
}

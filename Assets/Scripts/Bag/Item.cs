/**
* author: xiaomo
* github: https://github.com/xiaomoinfo
* email: xiaomo@xiamoo.info
* QQ_NO: 83387856
* Desc: 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;

[Serializable]
public class Item
{
    /// <summary>
    /// id
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// 名字
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Desc { get; set; }

    /// <summary>
    ///  类型
    /// </summary>
    public ItemType ItemType { get; set; }

    /// <summary>
    /// 品质 
    /// </summary>
    public ItemQuality ItemQuality { get; set; }

    /// <summary>
    /// 购买价格
    /// </summary>
    public int BuyPrice { get; set; }

    /// <summary>
    /// 售出价格
    /// </summary>
    public int SellPrice { get; set; }

    /// <summary>
    /// 重量
    /// </summary>
    public float Weight { get; set; }

    /// <summary>
    /// icon
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// 血量
    /// </summary>
    public long Hp { get; set; }

    /// <summary>
    /// 蓝量
    /// </summary>
    public long Mp { get; set; }

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


    public Item()
    {
        ID = -1;
    }

    public Item(int id, string name, string desc, ItemType itemType, ItemQuality itemQuality, int buyPrice, int sellPrice, float weight, string icon, long hp, long mp, int power,
        int intellect, int agility, int stamina, EquipmentType equipmentType)
    {
        ID = id;
        Name = name;
        Desc = desc;
        ItemType = itemType;
        ItemQuality = itemQuality;
        BuyPrice = buyPrice;
        SellPrice = sellPrice;
        Weight = weight;
        Icon = icon;
        Hp = hp;
        Mp = mp;
        Power = power;
        Intellect = intellect;
        Agility = agility;
        Stamina = stamina;
        EquipmentType = equipmentType;
    }
}

[Serializable]
public class ItemList
{
    public List<Item> items;
}


public enum ItemType
{
    [Description("装备")] EQUIPMENT,
    [Description("道具")] ITEM,
    [Description("药品")] DRUG,
    [Description("武器")] WEAPON,
}


public enum ItemQuality
{
    [Description("白色")] WHITE = 1,
    [Description("绿色")] GREEN = 2,
    [Description("蓝色")] BLUE = 3,
    [Description("粉色")] PINK = 4,
    [Description("橙色")] ORANGE = 5,
    [Description("红色")] RED = 6
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

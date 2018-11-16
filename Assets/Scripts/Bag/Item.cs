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
    public int id;

    /// <summary>
    /// 名字
    /// </summary>
    public string name;

    /// <summary>
    /// 描述
    /// </summary>
    public string desc;

    /// <summary>
    ///  类型
    /// </summary>
    public ItemType itemType;

    /// <summary>
    /// 品质 
    /// </summary>
    public ItemQuality itemQuality;

    /// <summary>
    /// 购买价格
    /// </summary>
    public int buyPrice;

    /// <summary>
    /// 售出价格
    /// </summary>
    public int sellPrice;

    /// <summary>
    /// 重量
    /// </summary>
    public float weight;

    /// <summary>
    /// icon
    /// </summary>
    public string icon;


    /// <summary>
    /// 叠加容量
    /// </summary>
    public int capacity;

    /// <summary>
    /// 血量
    /// </summary>
    public long hp;

    /// <summary>
    /// 蓝量
    /// </summary>
    public long mp;

    /// <summary>
    /// 力量
    /// </summary>
    public int power;

    /// <summary>
    /// 智力
    /// </summary>
    public int intellect;

    /// <summary>
    /// 敏捷
    /// </summary>
    public int agility;

    /// <summary>
    /// 体力
    /// </summary>
    public int stamina;


    public Item()
    {
        id = -1;
    }


    public Item(int id, string name, string desc, ItemType itemType, ItemQuality itemQuality, int buyPrice, int sellPrice, float weight, string icon, long hp, long mp, int power,
        int intellect, int agility, int stamina)
    {
        this.id = id;
        this.name = name;
        this.desc = desc;
        this.itemType = itemType;
        this.itemQuality = itemQuality;
        this.buyPrice = buyPrice;
        this.sellPrice = sellPrice;
        this.weight = weight;
        this.icon = icon;
        this.hp = hp;
        this.mp = mp;
        this.power = power;
        this.intellect = intellect;
        this.agility = agility;
        this.stamina = stamina;
    }
}

[Serializable]
public class ItemList
{
    public List<Item> items = new List<Item>();
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

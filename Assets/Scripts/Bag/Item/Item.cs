/**
* author: xiaomo
* github: https://github.com/xiaomoinfo
* email: xiaomo@xiamoo.info
* QQ_NO: 83387856
* Desc: 
*/

using System.ComponentModel;
using UnityEngine;

public class Item
{
    public int ID { get; set; }

    public string Name { get; set; }

    public string Desc { get; set; }

    public ItemType ItemType { get; set; }

    public ItemQuality ItemQuality { get; set; }

    public int BuyPrice { get; set; }

    public int SellPrice { get; set; }

    public float Weight { get; set; }

    public string Icon { get; set; }

    public Item()
    {
        ID = -1;
    }


    public Item(int id, string name, string desc, ItemType itemType, ItemQuality itemQuality, int buyPrice, int sellPrice, float weight, string icon)
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
    }
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

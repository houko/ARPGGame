/**
* author: xiaomo
* github: https://github.com/xiaomoinfo
* email: xiaomo@xiamoo.info
* QQ_NO: 83387856
* Desc: 药品
*/

public class Drug : Item
{
    public long Hp { get; set; }

    public long Mp { get; set; }

    public Drug(int id, string name, string desc, ItemType itemType, ItemQuality itemQuality, int buyPrice, int sellPrice, float weight, string icon, long hp, long mp) : base(id,
        name, desc, itemType, itemQuality, buyPrice, sellPrice, weight, icon)
    {
        Hp = hp;
        Mp = mp;
    }
}

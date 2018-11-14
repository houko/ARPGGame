/**
* author: xiaomo
* github: https://github.com/xiaomoinfo
* email: xiaomo@xiamoo.info
* QQ_NO: 83387856
* Desc: 背包管理器
*/

using Boo.Lang;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    private static BagManager _instance;

    private List<Item> itemList;

    public void ParseJson()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("items.json");
        string json = JsonUtility.ToJson(textAsset.text);
    }

    public static BagManager Instance()
    {
        return _instance ? _instance : GameObject.Find("BagManager").GetComponent<BagManager>();
    }
}

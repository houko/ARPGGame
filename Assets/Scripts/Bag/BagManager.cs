/**
* author: xiaomo
* github: https://github.com/xiaomoinfo
* email: xiaomo@xiamoo.info
* QQ_NO: 83387856
* Desc: 背包管理器
*/

using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    private static BagManager _instance;

    private void Awake()
    {
        ParseJson();
    }

    public void ParseJson()
    {
        string json = File.ReadAllText(Application.streamingAssetsPath + "/item.json");
        ItemList itemList = JsonUtility.FromJson<ItemList>(json);
        foreach (var item in itemList.items)
        {
            Debug.Log(item.name);
        }
    }

    public static BagManager Instance()
    {
        return _instance ? _instance : GameObject.Find("BagManager").GetComponent<BagManager>();
    }
}

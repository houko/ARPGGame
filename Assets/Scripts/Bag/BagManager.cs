/**
* author: xiaomo
* github: https://github.com/xiaomoinfo
* email: xiaomo@xiamoo.info
* QQ_NO: 83387856
* Desc: 背包管理器
*/

using System.Collections.Generic;
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
        TextAsset textAsset = Resources.Load<TextAsset>("items");
        ItemList itemList = JsonUtility.FromJson<ItemList>(textAsset.text);
        foreach (var item in itemList.items)
        {
            Debug.Log(item);
        }
    }

    public static BagManager Instance()
    {
        return _instance ? _instance : GameObject.Find("BagManager").GetComponent<BagManager>();
    }
}

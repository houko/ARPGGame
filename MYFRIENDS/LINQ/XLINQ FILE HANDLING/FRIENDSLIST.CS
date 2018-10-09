using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MyFriends
{
    public class FriendsList : ObservableCollection<Friend>
    {
        private static FriendsList instance;//保存FriendsList的实例
        private FriendsList()
        {//私有构造函数，用于从XML文件中获取联系人列表
            try
            {
                XMLFileOperations.XmlFilename = //获取保存联系人的XML文件名
                    (string)Application.Current.Properties["SavedDetailsFileName"];
                //获取联系人列表
                List<Friend> theList = XMLFileOperations.GetFriends();
                foreach (Friend friend in theList)
                {//将联系人添加到集合中
                    this.Add(friend);
                }
            }
            catch { }
        }
        public static FriendsList Instance()
        {//单件模式的实例化函数
            if (instance == null)
            {//实例化联系人列表类
                instance = new FriendsList();
            }
            return instance;
        }
    }
}

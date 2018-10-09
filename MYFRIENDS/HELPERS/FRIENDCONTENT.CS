using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFriends
{
    public class FriendContent
    {//用于临时保存联系人信息的单件类
        public string FriendName { get; set; }//名字
        public string FriendEmail { get; set; }//电邮
        public string PhotoUrl { get; set; }//照片路径
        public string VideoUrl { get; set; }//视频路径
        public string MusicUrl { get; set; }//音乐路径
        private static FriendContent instance;//实例变量
        private FriendContent()
        {//私有构造函数，防止用户直接实例化
        }
        public void Reset()
        {//调用Reset方法清空字段
            FriendName = string.Empty;
            FriendEmail = string.Empty;
            PhotoUrl = string.Empty;
            VideoUrl = string.Empty;
            MusicUrl = string.Empty;
        }
        public static FriendContent Instance()
        {//实例化FriendContent的公共静态方法
            if (instance == null)
            {
                instance = new FriendContent();
            }
            return instance;
        }
    }
}

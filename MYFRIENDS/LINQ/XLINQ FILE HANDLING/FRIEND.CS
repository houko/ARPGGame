using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFriends
{
    public class Friend
    {//表示单个联系人的实例
        public Guid ID { get; set; }//ID号
        public string Email { get; set; }//电邮
        public string Name { get; set; }//名字
        public string PhotoUrl { get; set; }//照片
        public string VideoUrl { get; set; }//视频
        public string MusicUrl { get; set; }//音频
    }
}

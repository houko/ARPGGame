using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Xml.Linq;
using System.Xml;
using System.IO;


namespace MyFriends
{
    public class XMLFileOperations
    {
        public static string XmlFilename { get; set; }

        public static List<Friend> GetFriends()
        {
            string fullXmlPath =//获取当前XML文件的完整路径 
                System.IO.Path.Combine
                (Environment.CurrentDirectory, 
                XMLFileOperations.XmlFilename);
            //使用LINQ to XML语法查询XML文件中的联系人
            var xmlFriendResults =
                from friend in StreamElements(fullXmlPath, "Friend")
                select new Friend
                {//将查询结果投影为一个Friend对象
                    ID = new Guid(friend.Element("ID").Value),
                    Name = friend.Element("name").SafeValue(),
                    Email = friend.Element("email").SafeValue(),
                    PhotoUrl = friend.Element("photo").SafeValue(),
                    VideoUrl = friend.Element("video").SafeValue(),
                    MusicUrl = friend.Element("music").SafeValue()
                };
            //返回所查询的列表
            return xmlFriendResults.ToList();
        }
        public static void CreateInitialFile(string fullXmlPath, Friend friend)
        {//创建初始文件
            XElement friendsXmlDocument =
                new XElement("MyFriends",
                    new XElement("Friend",
                        new XElement("ID", friend.ID),
                        new XElement("name", friend.Name),
                        new XElement("email", friend.Email),
                        new XElement("photo", friend.PhotoUrl),
                        new XElement("video", friend.VideoUrl),
                        new XElement("music", friend.MusicUrl))
                );
            //Save方法将XML序列化为指定位置的文件
            friendsXmlDocument.Save(fullXmlPath);
        }
        public static void AppendToFile(string fullXmlPath, Friend friend)
        {//追加XML到文件中，Load方法从文件加载XElement
            XElement friendsXmlDocument = XElement.Load(fullXmlPath);
            //调用XElement对象追加联系人信息
                friendsXmlDocument.Add(new XElement("Friend",
                new XElement("ID", friend.ID),
                new XElement("name", friend.Name),
                new XElement("email", friend.Email),
                new XElement("photo", friend.PhotoUrl),
                new XElement("video", friend.VideoUrl),
                new XElement("music", friend.MusicUrl))
            );
            //Save方法将XML序列化为指定位置的文件
            friendsXmlDocument.Save(fullXmlPath);
        }


        public static IEnumerable<XElement> StreamElements(string uri, string name)
        {//返回指定文件中的XML元素的集合
            using (XmlReader reader = XmlReader.Create(uri))
            {//使用指定路径加载XML文件
                reader.MoveToContent();//跳至内容节点
                while (reader.Read())//读取内容节点
                {//如果节点类型为元素，且元素名称为指定的参数名称
                    if ((reader.NodeType == XmlNodeType.Element) &&
                      (reader.Name == name))
                    {//使用ReadFrom方法创建XElement，表示XLINQ节点
                        XElement element = (XElement)XElement.ReadFrom(reader);
                        yield return element;//向迭代器返回元素
                    }
                }
                reader.Close();
            }
        }

        public static void SaveOnExit()
        {//退出时保存的方法
            string xmlFilename = (string)System.Windows.Application.
                Current.Properties["SavedDetailsFileName"];//获取保存的文件名
            string fullXmlPath = Path.Combine((string)System.Windows.//获取保存文件路径
                Application.Current.Properties["SaveFolder"], xmlFilename);
            XDocument document = new XDocument(new XElement//初始化XDocument实例
                ("MyFriends", getExistingElements()));
            document.Save(fullXmlPath);//保存XML文档
        }
        //获取FriendsList中所有的联系人记录，加载到XML中
        private static List<XElement> getExistingElements()
        {
            List<XElement> elements = new List<XElement>();
            foreach (Friend friend in FriendsList.Instance())
            {//遍历集合，加载到XElement集合中
                elements.Add(new XElement("Friend",
                        new XElement("ID", friend.ID),
                        new XElement("name", friend.Name),
                        new XElement("email", friend.Email),
                        new XElement("photo", friend.PhotoUrl),
                        new XElement("video", friend.VideoUrl),
                        new XElement("music", friend.MusicUrl)));
            }
            return elements;//返回 List<XElement>泛型集合
        }
    }
}

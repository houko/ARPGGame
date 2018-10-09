using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Xml.Linq;
using System.Xml;

namespace MyFriends
{
    public static class CustomStringExtensions
    {   //扩展方法，判断文件集合中是否存在与指定的谓词相匹配的文件名称。
        public static IEnumerable<string> IsImageFile(this IEnumerable<string> files,
                                            Predicate<string> isMatch)
        {
            foreach (string file in files)//遍历集合
            {
                if (isMatch(file))//如果谓词方法返回true
                    yield return file;//使用yield关键字返回迭代值
            }
        }
        //扩展方法，判断文件集合中是否存在jpg，png或者是bmp文件
        public static IEnumerable<string> IsImageFile(this IEnumerable<string> files)
        {
            foreach (string file in files)//遍历文件
            {//判断是否存在图像文件扩展名的文件
                if (file.Contains(".jpg") || file.Contains(".png") || file.Contains(".bmp"))
                    yield return file;//向迭代器返回图像文件
            }
        }
    }
}

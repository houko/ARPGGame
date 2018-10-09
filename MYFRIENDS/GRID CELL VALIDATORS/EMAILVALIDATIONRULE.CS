using System;
using System.Collections.Generic;
using System.Text;
//添加对如下命名空间的引用
using Xceed.Wpf.DataGrid;
using Xceed.Wpf.DataGrid.ValidationRules;
using System.Windows.Controls;
using System.Globalization;
using System.Text.RegularExpressions;
namespace MyFriends
{
    //定义一个验证电子邮件是否正确的类
    public class EmailValidationRule : CellValidationRule
    {
        public override ValidationResult Validate
            (object value, CultureInfo cultureInfo,
            CellValidationContext cellValidationContext)
        {
            //指定验证电子邮件格式的正则表达式
            string pattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            //实例化一个Regex类准备验证
            Regex regEx=new Regex(pattern);
            if (!regEx.IsMatch((string)value))
            {//如果验证失败，则返回一个具有错误消息的ValidationResult对象
                return new ValidationResult
                    (false, "输入了无效的密码");
            }
            //返回代表
            return new ValidationResult(true, null);
        }
    }
}

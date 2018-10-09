using System;
using System.Collections.Generic;
using System.Text;
//添加需要引用的命名空间
using Xceed.Wpf.DataGrid;
using Xceed.Wpf.DataGrid.ValidationRules;
using System.Windows.Controls;
using System.Globalization;
namespace MyFriends
{
    public class NotEmptyValidationRule : CellValidationRule
    {//不能为空的验证规则
        public override ValidationResult Validate
            (object value, CultureInfo cultureInfo,
            CellValidationContext cellValidationContext)
        {//判断value值是否为空
            if (Convert.ToString(value).Equals(string.Empty))
            {//如果为空，则返回一个验证失败的ValidationResult对象实例
                return new ValidationResult(false, "名称不能为空");
            }
            //如果不为空，则返回验证成功的ValidationResult对象实例
            return new ValidationResult(true, null);
        }
    }
}

using System;
using UnityEngine;

namespace FireNBM
{
    public static class EnumExtension
    {
        // Ex: actionsUnit.HasFlag(action) : enumValue = actionsUnit và flag = action
        public static bool HasFlag(this Enum enumValue, Enum flag)
        {
            // Chuyển enumValue và flad sang kiểu int để thực hiện phép toán bitwise
            int intEnumValue = Convert.ToInt32(enumValue);
            int intFlag = Convert.ToInt32(flag);

            // Kiểm tra cờ được thiết lập trong giá trị enum.
            return (intEnumValue & intFlag) == intFlag;
        }
    }
}
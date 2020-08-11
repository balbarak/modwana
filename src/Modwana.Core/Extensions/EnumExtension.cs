using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modwana.Core.Extensions
{
    public static class EnumExtension
    {
        public static string GetDisplayName(this Enum value)
        {
            var type = value.GetType();

            if (!type.IsEnum) throw new ArgumentException(String.Format("Type '{0}' is not Enum", type));
            
            var members = type.GetMember(value.ToString());
            if (members.Length == 0)
                return "";

            var member = members[0];
            var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);

            if (attributes.Length > 0)
            {
                var attribute = (DisplayAttribute)attributes[0];
                return attribute.GetName();
            }
            else
                return value.ToString();
        }
    }
}

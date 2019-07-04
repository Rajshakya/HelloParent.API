using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HelloParent.Utilities.Enums
{
   public static class EnumHelper
    {
        public static Dictionary<string, string> GetEnumLabels(Type typeOfEnum)
        {
            var toReturn = new Dictionary<string, string>();
            foreach (
                var field in typeOfEnum.GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public))
            {
                var value = (int)field.GetValue(null);
                var name = Enum.GetName(typeOfEnum, value);
                var label = name;
                foreach (DisplayAttribute currAttr in field.GetCustomAttributes(typeof(DisplayAttribute), true))
                {
                    label = currAttr.Name;
                    break;
                }

                if (name != null)
                {
                    toReturn[name] = label;
                }
            }

            return toReturn;
        }
        public static bool HasSomething(this string str)
        {
            return !String.IsNullOrEmpty(str);
        }

        public static string DisplayName(Type enumType, string enumValue)
        {
            var member = enumType.GetMember(enumValue).FirstOrDefault();
            if (member != null)
            {
                var attr =
                    member.GetCustomAttributes(typeof(DisplayAttribute), false)
                        .OfType<DisplayAttribute>()
                        .FirstOrDefault();
                if (attr != null)
                {
                    if (attr.ResourceType == null)
                    {
                        return attr.Name;
                    }
                    else
                    {
                        return attr.GetName();
                    }
                }
            }
            return enumValue;
        }
    }

    public static class StringHelper
    {
        public static ObjectId AsObjectId(this string str)
        {
            if (!IsObjectId(str))
            {
                return ObjectId.Empty;
            }
            return ObjectId.Parse(str);
        }
        public static bool IsObjectId(this string str)
        {
            ObjectId temp;
            return !String.IsNullOrEmpty(str) && ObjectId.TryParse(str, out temp);
        }


    }
}

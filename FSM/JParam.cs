using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ================================
//* 功能描述：JParam  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/5/20 14:13:21
// ================================
namespace Assets.JackCheng.FSM
{
    public enum JParamType
    {
        Int,
        Float,
        Double,
        Boolen,
        Func,
    }
    public abstract class JParam
    {
        private static Dictionary<JParamType, JConditionType> dic = new Dictionary<JParamType, JConditionType>();

        static JParam()
        {
            dic[JParamType.Int] =
            dic[JParamType.Float] =
            dic[JParamType.Double] = JConditionType.Less
                                    | JConditionType.LessEquals
                                    | JConditionType.Greater
                                    | JConditionType.GreaterEquals
                                    | JConditionType.Equals
                                    | JConditionType.NotEquals;
            dic[JParamType.Boolen] = JConditionType.Equals | JConditionType.NotEquals;
            dic[JParamType.Func] = JConditionType.Func;
        }

        //----------------------------------

        public JParam(string _name, JParamType _type)
        {
            this.mName = _name;
            this.mParamType = _type;
        }
        public string mName;
        
        public JParamType mParamType;
        
        public object Value;

        public T GetValue<T>() where T : struct
        {
            return (T)Value;
        }
        public T GetClassValue<T>() where T : class
        {
            return Value as T;
        }

        public bool CheckValue()
        {
            return CheckValue(Value);
        }
        public virtual bool CheckValue(object _value)
        {
            bool result = false;
            switch (mParamType)
            {
                case JParamType.Int:
                    result = _value is int;
                    break;
                case JParamType.Float:
                    result = _value is float;
                    break;
                case JParamType.Double:
                    result = _value is double;
                    break;
                case JParamType.Boolen:
                    result = _value is bool;
                    break;
                case JParamType.Func:
                    result = true; // func args, it can spectial anything..., always return true
                    break;
            }
            return result;
        }

        public JParam Clone()
        {
            return MemberwiseClone() as JParam;
        }
    }
}

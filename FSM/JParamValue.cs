using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ================================
//* 功能描述：JParamValue  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/5/20 15:04:04
// ================================
namespace Assets.JackCheng.FSM
{
    public class JParamValue : JParam
    {
        public JParamValue(string name)
            : this(name, JParamType.Int)
        {

        }

        public JParamValue(string name, JParamType type)
            : base(name, type)
        {
            switch (type)
            {
                case JParamType.Int:
                    Value = 0;
                    break;
                case JParamType.Float:
                    Value = 0f;
                    break;
                case JParamType.Double:
                    Value = 0d;
                    break;
                default:
                    throw new Exception(string.Format("param value invalidated:{0}", type));
            }
        }

        public override bool CheckValue(object _value)
        {
            switch (mParamType)
            {
                case JParamType.Int:
                    return _value is int;
                case JParamType.Float:
                    return _value is float;
                case JParamType.Double:
                    return _value is double;
            }
            return false;

        }
    }
}

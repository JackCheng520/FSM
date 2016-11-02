using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ================================
//* 功能描述：JCondition  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/5/20 11:18:12
// ================================
namespace Assets.JackCheng.FSM
{
    [Flags]
    public enum JConditionType
    {
        None = 0,
        Less = 1,
        LessEquals = 2,
        Equals = 4,
        Greater = 8,
        GreaterEquals = 16,
        NotEquals = 32,
        Func = 64,
    }

    public class JCondition
    {
        public JCondition(string _key, object _value, JConditionType _conditionType)
        {
            this.mKey = _key;
            this.mValue = _value;
            this.mConditionType = _conditionType;
        }

        private string mKey;

        private object mValue;

        private JConditionType mConditionType;

        public bool Check(JFSM fsm)
        {
            if (string.IsNullOrEmpty(mKey))
                return false;
            var val = fsm.GetParam<JParam>(mKey);
            if (val == null)
                return false;
            bool result = false;
            switch (val.mParamType)
            {
                case JParamType.Int:
                case JParamType.Double:
                case JParamType.Float:
                    val = fsm.GetParam<JParamValue>(mKey);
                    if (val != null)
                    {
                        result = CheckValueAndTarget(Convert.ToInt32(val.Value), Convert.ToInt32(mValue), mConditionType);
                    }
                    break;
                case JParamType.Boolen:
                    val = fsm.GetParam<JParamBoolen>(mKey);
                    if (val != null)
                    {
                        result = CheckBool(Convert.ToBoolean(val.Value), Convert.ToBoolean(mValue), mConditionType);
                    }
                    break;
                case JParamType.Func:
                    val = fsm.GetParam<JParamFunc>(mKey);
                    if (val != null)
                    {
                        FuncParamHandler _action = (FuncParamHandler)val.Value;
                        object[] _args = mValue == null ? null : (mValue is object[] ? mValue as object[] : new object[] { mValue });
                        result = _action == null ? false : _action(_args);
                    }
                    break;
            }
            return result;
        }

        private bool CheckBool(bool a, bool b, JConditionType type)
        {
            if (type == JConditionType.Equals)
                return a == b;
            else if (type == JConditionType.NotEquals)
                return a != b;
            return false;
        }

        private bool CheckValueAndTarget(double value, double target, JConditionType type)
        {
            var result = false;
            if (ContainType(type, JConditionType.Less))
            {
                result = value < target;
            }
            else if (ContainType(type, JConditionType.LessEquals))
            {
                result = value <= target;
            }
            else if (ContainType(type, JConditionType.Greater))
            {
                result = value > target;
            }
            else if (ContainType(type, JConditionType.GreaterEquals))
            {
                result = value >= target;
            }
            else if (ContainType(type, JConditionType.NotEquals))
            {
                result = value != target;
            }
            else if (ContainType(type, JConditionType.Equals))
            {
                result = value == target;
            }

            return result;
        }

        private bool ContainType(JConditionType a, JConditionType b)
        {
            return (a & b) == b;
        }
    }
}

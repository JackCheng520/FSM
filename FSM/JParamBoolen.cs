using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ================================
//* 功能描述：JParamBoolen  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/5/20 15:12:12
// ================================
namespace Assets.JackCheng.FSM
{
    public class JParamBoolen : JParam
    {
        public JParamBoolen(string name)
            : base(name,JParamType.Boolen)
        {
            
        }

        public override bool CheckValue(object _value)
        {
            return _value is bool;
        }
    }
}

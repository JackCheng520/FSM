using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ================================
//* 功能描述：JParamFunc  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/5/20 15:13:31
// ================================
namespace Assets.JackCheng.FSM
{
    public class JParamFunc : JParam
    {
        public JParamFunc(string name)
            : base(name, JParamType.Func)
        {

        }

        public override bool CheckValue(object _value)
        {
            return true;
        }
    }
}

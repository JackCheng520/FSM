using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ================================
//* 功能描述：JPipeline  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/5/20 11:14:45
// ================================
namespace Assets.JackCheng.FSM
{
    public class JPipeline
    {
        public JPipeline(string _srcName, string _targetName)
        {
            this.SrcName = _srcName;
            this.TargetName = _targetName;
        }
        private string srcName;
        public string SrcName
        {
            set { srcName = value; }
            get { return this.srcName; }
        }

        private string targetName;
        public string TargetName
        {
            set { targetName = value; }
            get { return this.targetName; }
        }

        private List<JCondition> conditions = new List<JCondition>();

        #region Add / Remove / Get Condition
        public void AddCondition(JCondition _condition)
        {
            if (conditions.Contains(_condition))
                return;
            conditions.Add(_condition);
        }

        public void AddConditons(List<JCondition> _conditons)
        {
            for (int i = 0; i < _conditons.Count; i++)
            {
                AddCondition(_conditons[i]);
            }
        }

        public void RemoveCondition(JCondition _condition)
        {
            if (conditions.Contains(_condition))
                conditions.Remove(_condition);
        }

        #endregion

        public bool Check(JFSM fsm)
        {
            var result = true;
            for (int i = 0, len = conditions.Count; i < len; i++)
            {
                if (!conditions[i].Check(fsm))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}

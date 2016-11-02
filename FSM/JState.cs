using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ================================
//* 功能描述：JState  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/5/20 11:11:34
// ================================
namespace Assets.JackCheng.FSM
{
    public abstract class JState
    {
        public JState(string _stateName) { StateName = _stateName; }

        private string stateName;
        public string StateName
        {
            set { this.stateName = value; }
            get { return this.stateName; }
        }

        private bool beInit = false;

        private bool needUpdate = true;

        private List<JTransition> transitions = new List<JTransition>();

        #region Add / Remove / Get Transition
        public void AddTransition(JTransition _transition)
        {
            if (transitions.Contains(_transition))
                return;
            transitions.Add(_transition);
        }

        public void RemoveTransition(JTransition _transition)
        {
            if (transitions.Contains(_transition))
                transitions.Remove(_transition);
        }

        public List<JTransition> GetTransitions(string _srcName, string _targetName)
        {
            List<JTransition> mlist = new List<JTransition>();
            for (int i = 0, len = transitions.Count; i < len; i++)
            {
                if (transitions[i].SrcName.Equals(_srcName) && transitions[i].TargetName.Equals(_targetName))
                {
                    mlist.Add(transitions[i]);
                }
            }
            return mlist;
        }

        #endregion

        public void Update(JFSM fsm)
        {
            //if (!beInit)
            //{//初始化
            //    beInit = true;
            //    onEnter(fsm);
            //}

            if (needUpdate)
            {//更新
                needUpdate = false;
                for (int i = 0, len = transitions.Count; i < len; i++)
                {
                    transitions[i].Update(fsm);
                }
            }
        }

        public void DirtyState()
        {
            needUpdate = true;
        }

        //---------------------------------------------------
        
        public abstract void onEnter(JFSM fsm);

        public abstract void onExcute(JFSM fsm);

        public abstract void onExit(JFSM fsm);
    }
}

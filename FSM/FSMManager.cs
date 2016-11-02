using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ================================
//* 功能描述：FSMManager  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/5/26 13:50:08
// ================================
namespace Assets.JackCheng.FSM
{
    public delegate bool FuncParamHandler(params object[] args);
    public class FSMManager : JSington<FSMManager>
    {
        public void TransitionTo(JFSM fsm ,string srcState,string targetState,List<JCondition> conditions)
        {
            JTransition t = null;
            JPipeline p = null;
            GetTransitionAndPipeline(fsm, srcState, targetState,out t, out p);
            p.AddConditons(conditions);
        }

        public void GetTransitionAndPipeline(JFSM fsm, string srcState, string targetState, out JTransition t, out JPipeline p)
        {
            t = null;
            p = null;
            var _state = fsm.GetState(srcState);
            if (_state == null)
                return;
            var _transitions = _state.GetTransitions(srcState, targetState);
            if (_transitions.Count > 0)
                t = _transitions[0];
            if (t == null)
            {
                t = new JTransition(srcState, targetState);
                _state.AddTransition(t);
            }
            var _pipelines = t.GetPipelines(srcState, targetState);
            if (_pipelines.Count > 0)
                p = _pipelines[0];
            if (p == null)
            {
                p = new JPipeline(srcState, targetState);
                t.AddPipeline(p);
            }
        }
    }
}

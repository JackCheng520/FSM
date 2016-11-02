using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ================================
//* 功能描述：IdleState  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/5/26 15:51:00
// ================================
namespace Assets.JackCheng.FSM.FSMTest
{
    public class IdleState : JState
    {
        public IdleState(string name)
            : base(name)
        {

        }

        private float time = 0;

        public override void onEnter(JFSM fsm)
        {
            UnityEngine.Debug.Log("IdleState -- OnEnter");
            time = 0;
        }

        public override void onExcute(JFSM fsm)
        {
            time += Time.deltaTime;

            if (time > 5)
            {
                time = 0;
                fsm.SetParamValue(FSMMonster.BeIdleTrue, FSMMonster.MonsterRun);
            }
        }

        public override void onExit(JFSM fsm)
        {

        }
    }
}

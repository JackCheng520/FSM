using Assets.JackCheng.TREE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ================================
//* 功能描述：MoveToState  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/5/26 14:35:26
// ================================
namespace Assets.JackCheng.FSM.FSMTest
{
    public class MoveToState : JState
    {
        public MoveToState(string name)
            : base(name)
        {

        }

        public override void onEnter(JFSM fsm)
        {
            UnityEngine.Debug.Log("MoveToState -- OnEnter");
        }

        public override void onExcute(JFSM fsm)
        {
            Vector3 targetPos = JBTMathfUtil.Vector3ZeroY((fsm.me as FSMMonster).jblackboard.GetData<Vector3>(FSMMonster.nextMovePos));
            Vector3 currentPos = JBTMathfUtil.Vector3ZeroY(fsm.me.transform.position);
            float distToTarget = JBTMathfUtil.GetDistance2D(targetPos, currentPos);
            if (distToTarget < 1f)
            {

                fsm.SetParamValue(FSMMonster.BeIdleTrue, FSMMonster.MonsterIdle);
                (fsm.me as FSMMonster).jblackboard.Add(FSMMonster.canChangePos, true);
                return ;
            }
            else
            {
                Vector3 toTarget = JBTMathfUtil.GetDirection2D(targetPos, currentPos);
                float movingStep = 3f * Time.deltaTime;
                if (movingStep > distToTarget)
                {
                    movingStep = distToTarget;
                }
                fsm.me.transform.position = fsm.me.transform.position + toTarget * movingStep;
                return ;
            }
        }

        public override void onExit(JFSM fsm)
        {

        }
    }
}

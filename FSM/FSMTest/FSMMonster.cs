using Assets.JackCheng.TREE;
using Assets.JackCheng.TreeTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


// ================================
//* 功能描述：FSMMonster  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/5/26 15:39:28
// ================================
namespace Assets.JackCheng.FSM.FSMTest
{
    class FSMMonster : JPlayer
    {
        public const string Idle = "Idle";
        public const string Run = "Run";
        public const string BeIdleTrue = "BeIdleTrue";

        public const int MonsterIdle = 1;

        public const int MonsterRun = 0;

        public JCondition CON_RUN_TO_IDLE = new JCondition(BeIdleTrue, MonsterIdle, JConditionType.Equals);
        public JCondition CON_IDLE_TO_RUN = new JCondition(BeIdleTrue, MonsterRun, JConditionType.Equals);

        public const string canChangePos = "canChangePos";

        public const string nextMovePos = "nextMovePos";

        private JFSM fsm;

        public JBlackboard jblackboard;

        private GameObject target;

        private AIBehaviorRequest currentRequest;
        private AIBehaviorRequest nextRequest;


        public FSMMonster Init()
        {
            fsm = new JFSM("FSM", this);

            fsm.AddState(new MoveToState(Run));
            fsm.AddState(new IdleState(Idle));

            fsm.SetCurrentState(Idle);

            fsm.AddParam(new JParamValue(BeIdleTrue, JParamType.Int) { Value = MonsterIdle });

            FSMManager.Ins.TransitionTo(fsm, Run, Idle, new List<JCondition>() { CON_RUN_TO_IDLE });
            FSMManager.Ins.TransitionTo(fsm, Idle, Run, new List<JCondition>() { CON_IDLE_TO_RUN });

            jblackboard = new JBlackboard();
            jblackboard.Add(canChangePos, true);
            target = JGameResourceMgr.Ins.LoadResource("target");

            return this;
        }


        private float durationTime = 0.1f;
        private float time = 0;
        void Update()
        {
            //time += Time.deltaTime;
            //if (time >= durationTime)
            //{
            //    time = 0;

            UpdateAI(0, 0);
            UpdateRequest(0, 0);
            UpdateBehavior(0, 0);
            //}
        }

        private void UpdateAI(float _gameTime, float _deltaTime)
        {
            bool isTrue = jblackboard.GetData<bool>(canChangePos);
            if (isTrue)
            {
                jblackboard.Add(canChangePos, false);
                Vector3 targetPos = new Vector3(UnityEngine.Random.Range(-15f, 15f), 0, UnityEngine.Random.Range(-15f, 15f));
                jblackboard.Add(nextMovePos, targetPos);
                nextRequest = new AIBehaviorRequest
                    (
                    _gameTime,
                    targetPos
                    );
            }

        }

        public int UpdateRequest(float _gameTime, float _deltaTime)
        {
            if (currentRequest != nextRequest)
            {
                currentRequest = nextRequest;

                Vector3 targetPos = currentRequest.nextMovingTarget
                    + JBTMathfUtil.GetDirection2D(currentRequest.nextMovingTarget, transform.position) * 0.2f;

                Vector3 startPos = new Vector3(targetPos.x, -1.4f, targetPos.z);
                target.transform.position = startPos;
                LeanTween.move(target, targetPos, 1f);
            }

            return 0;
        }

        public int UpdateBehavior(float _gameTime, float _deltaTime)
        {
            if (currentRequest == null)
                return 0;

            fsm.Update();

            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ================================
//* 功能描述：JTransition  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/5/20 11:15:15
// ================================
namespace Assets.JackCheng.FSM
{
    public class JTransition
    {
        public JTransition(string _srcName, string _targetName)
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

        private List<JPipeline> pipelines = new List<JPipeline>();

        #region Add / Remvoe / Get Pipeline
        public void AddPipeline(JPipeline _pipeline)
        {
            if (pipelines.Contains(_pipeline))
                return;
            pipelines.Add(_pipeline);
        }

        public void RemovePipeline(JPipeline _pipeline)
        {
            if (pipelines.Contains(_pipeline))
                pipelines.Remove(_pipeline);
        }

        public List<JPipeline> GetPipelines(string _srcName, string _targetName)
        {
            List<JPipeline> mlist = new List<JPipeline>();
            for (int i = 0; i < pipelines.Count; i++)
            {
                if (pipelines[i].SrcName.Equals(_srcName) && pipelines[i].TargetName.Equals(_targetName)) {
                    mlist.Add(pipelines[i]);
                }
            }

            return mlist;
        }
        #endregion 

        public void Update(JFSM fsm)
        {
            for (int i = 0; i < pipelines.Count; i++)
            {
                if (pipelines[i].Check(fsm))
                {//设置状态机状态
                    fsm.SetCurrentState(pipelines[i].TargetName);
                }
            }
        }

        public void Clear()
        {
            pipelines.Clear();
        }
    }
}

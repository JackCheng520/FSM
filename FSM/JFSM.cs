using Assets.JackCheng.FSM.FSMTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ================================
//* 功能描述：JFSM  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/5/20 11:15:46
// ================================
namespace Assets.JackCheng.FSM
{
    public class JFSM
    {
        public JFSM(string _name,JPlayer _me)
        {
            this.name = _name;
            this.me = _me;
        }
        private readonly Dictionary<string, JParam> fsmParamDic = new Dictionary<string, JParam>();

        private Dictionary<string, JState> statesDic = new Dictionary<string, JState>();

        private string name;

        public JPlayer me;

        private JState lastState;

        private JState curState;

        #region Add / Remove / Get Param 
        public void AddParam(JParam _param)
        {
            if (_param == null || fsmParamDic.ContainsKey(_param.mName))
                return;
            if (!_param.CheckValue())
                return;
            fsmParamDic.Add(_param.mName, _param);
        }

        public JParam AddParam(string _name, JParamType _type)
        {
            JParam _param = fsmParamDic.ContainsKey(_name) ? fsmParamDic[_name] : null;
            if (_param == null)
            {
                _param = new JParamValue(_name, _type);
                switch (_type)
                {
                    case JParamType.Int:
                        _param.Value = 0;
                        break;
                    case JParamType.Float:
                        _param.Value = 0f;
                        break;
                    case JParamType.Double:
                        _param.Value = 0d;
                        break;
                }
                AddParam(_param);
            }
            return _param;
        }

        public void RemoveParam(JParam _param)
        {
            if (_param == null || !fsmParamDic.ContainsKey(_param.mName))
                return;
            fsmParamDic.Remove(_param.mName);
        }

        public JParam GetParam(string _name)
        {
            if (!fsmParamDic.ContainsKey(_name))
                return null;
            return fsmParamDic[_name];
        }

        public T GetParam<T>(string _name) where T : JParam
        {
            return GetParam(_name) as T;
        }

        public T GetParamValue<T>(string _name) where T : struct
        {
            return GetParam(_name).GetValue<T>();
        }

        public T GetParamClassValue<T>(string _name) where T : class
        {
            return GetParam(_name).GetClassValue<T>();
        }

        public void SetParamValue(string _name, object _value, bool autoAsValue = true)
        {
            var srcParam = GetParam(_name);

            if (srcParam == null)
                return;
            if (autoAsValue && _value.GetType() != srcParam.Value.GetType())
            {
                switch (srcParam.mParamType)
                {
                    case JParamType.Int:
                        _value = Convert.ToInt32(_value);
                        break;
                    case JParamType.Float:
                        _value = Convert.ToSingle(_value);
                        break;
                    case JParamType.Double:
                        _value = Convert.ToDouble(_value);
                        break;
                    case JParamType.Func:
                        _value = (Delegate)_value;
                        break;
                }
            }

            UnityEngine.Debug.logger.Log("Set param -- " + _value);
            if (srcParam.CheckValue())
            {
                srcParam.Value = _value;
                
                //更新状态
                if (curState != null)
                    curState.DirtyState();
            }
        }

        #endregion

        #region Add / Remove / GetParam
        public void AddState(JState _state)
        {
            if (statesDic.ContainsKey(_state.StateName))
                return;
            statesDic.Add(_state.StateName, _state);
        }

        public void RemoveState(JState _state)
        {
            if (statesDic.ContainsKey(_state.StateName))
                statesDic.Remove(_state.StateName);
        }

        public JState GetState(string _name)
        {
            if (statesDic.ContainsKey(_name))
                return statesDic[_name];
            return null;
        }

        public void SetCurrentState(string _name)
        {
            curState = GetState(_name);
        }
        #endregion

        public void Update()
        {
            if (curState != lastState)
            {
                if (lastState != null)
                {
                    lastState.onExit(this);
                }
                
                if (curState != null)
                {
                    curState.onEnter(this);
                }
                lastState = curState;
                
            }
            if (lastState != null)
            {
                lastState.Update(this);
                lastState.onExcute(this);
            }

        }

    }
}

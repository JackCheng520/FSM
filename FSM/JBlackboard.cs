using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ================================
//* 功能描述：JBlackboard  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/5/26 14:39:12
// ================================
namespace Assets.JackCheng.FSM
{
    public enum eJBlackboardUpdateType
    {
        Add,
        Remove,
        Modify,
    }
    public delegate void BlackboardUpdateCallBack(JBlackboard b, eJBlackboardUpdateType t);
    public class JBlackboard
    {
        private Dictionary<string, object> dic = new Dictionary<string, object>();
        public event BlackboardUpdateCallBack callback;
        public void Add(string _key, object _value)
        {
            if (dic.ContainsKey(_key))
            {
                dic[_key] = _value;
                if (callback != null)
                    callback(this, eJBlackboardUpdateType.Modify);
            }
            else
            {
                dic.Add(_key, _value);
                if (callback != null)
                    callback(this, eJBlackboardUpdateType.Add);
            }
        }

        public void Remove(string _key)
        {
            if (dic.ContainsKey(_key))
            {
                dic.Remove(_key);
                if (callback != null)
                    callback(this, eJBlackboardUpdateType.Remove);
            }
        }

        public T GetData<T>(string _key)
        {
            if (dic.ContainsKey(_key))
                return (T)dic[_key];

            return default(T);
        }
    }
}

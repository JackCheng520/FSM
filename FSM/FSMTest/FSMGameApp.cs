using Assets.JackCheng.TreeTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ================================
//* 功能描述：FSMGameApp  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/5/26 17:10:18
// ================================
namespace Assets.JackCheng.FSM.FSMTest
{
    class FSMGameApp : MonoBehaviour
    {
        void OnGUI()
        {
            //speed up/slow down
            Time.timeScale = GUILayout.HorizontalSlider(Time.timeScale, 0, 2);
            //add unity
            if (GUILayout.Button("Add Monster"))
            {
                GameObject go = JGameResourceMgr.Ins.LoadResource("entity");
                if (go != null)
                {
                    go.AddComponent<FSMMonster>().Init();
                }
            }
        }
    }
}

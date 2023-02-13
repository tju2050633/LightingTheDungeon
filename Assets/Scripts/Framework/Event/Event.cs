using UnityEngine;
using System;

/*
    事件类的公共父类
*/

namespace LightingTheDungeon
{
    public class Event<T> where T : Event<T>
    {
        // 事件委托(触发时执行的动作)
        private static Action actions;

        // 注册事件
        public static void Register(Action action)
        {
            actions += action;
        }

        // 取消注册事件
        public static void Unregister(Action action)
        {
            actions -= action;
        }

        // 触发事件
        public static void Invoke()
        {
            actions?.Invoke();
        }
    }
}

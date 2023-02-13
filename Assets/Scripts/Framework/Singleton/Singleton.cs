using System;
using System.Reflection;

/* 
    单例类的公共父类
*/

namespace LightingTheDungeon
{
    public class Singleton<T> where T : new()
    {
        private static T mInstance;

        // 通过 .Instance 获取单例
        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    if (mInstance == null)
                    {
                        mInstance = new T();
                    }
                    return mInstance;
                }
                return mInstance;
            }
        }

        // 防止外部实例化
        protected Singleton() { }
    }
}
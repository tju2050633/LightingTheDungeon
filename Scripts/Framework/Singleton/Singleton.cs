using System;
using System.Reflection;

/* 
    单例类的公共父类
*/

namespace GloryOfDead
{
    public class Singleton<T> where T : Singleton<T>
    {
        private static T mInstance;

        // 通过 .Instance 获取单例
        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    // 通过反射获取构造函数列表
                    var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

                    // 获取无参、非public构造函数
                    var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);

                    // 获取不到该构造函数，抛出异常
                    if (ctor == null)
                    {
                        throw new System.Exception("Non-public ctor() not found!");
                    }

                    // 通过构造函数创建实例
                    mInstance = ctor.Invoke(null) as T;
                }
                return mInstance;
            }
        }

        // 防止外部实例化
        protected Singleton() { }
    }
}
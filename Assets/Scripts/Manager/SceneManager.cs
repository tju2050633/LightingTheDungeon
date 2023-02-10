using UnityEngine;

namespace GloryOfDead
{
    public class SceneManager : Singleton<SceneManager>
    {
        // 单例模式
        private SceneManager() { }

        // 切换场景
        public void SwitchScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}


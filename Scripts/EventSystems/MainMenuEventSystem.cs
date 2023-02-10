using UnityEngine;

/*
    主菜单事件系统，挂载到MainMenu场景的EventSystem上
*/

namespace GloryOfDead
{
    public class MainMenuEventSystem : MonoBehaviour
    {
        void Start()
        {
            // 注册【开始游戏】事件
            StartEvent.Register(OnStart);
        }

        private void OnStart()
        {
            // 加载游戏场景
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");

            Debug.Log("开始游戏");
        }

        private void OnDestroy() {
            // 注销【开始游戏】事件
            StartEvent.Unregister(OnStart);
        }
    }

}
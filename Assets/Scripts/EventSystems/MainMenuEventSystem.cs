using UnityEngine;

/*
    主菜单事件系统，挂载到MainMenu场景的EventSystem上
*/

namespace LightingTheDungeon
{
    public class MainMenuEventSystem : MonoBehaviour
    {
        void Start()
        {
            // 注册【开始游戏】事件
            StartEvent.Register(OnStart);

            // 播放BGM
            AudioManager.Instance.PlayBGM("MainScene");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }

        private void OnStart()
        {
            // 停止BGM
            AudioManager.Instance.StopBGM();

            // 加载游戏场景
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
        }

        private void OnDestroy()
        {
            // 注销【开始游戏】事件
            StartEvent.Unregister(OnStart);
        }
    }

}
using UnityEngine;

/*
    游戏场景事件系统，挂载到Game场景的EventSystem上
*/

namespace GloryOfDead
{
    public class GameEventSystem : MonoBehaviour
    {
        // PauseUI面板
        public GameObject pauseUI;

        void Start()
        {
            // 注册【暂停】事件
            PauseEvent.Register(OnPause);

            // 注册【返回】事件
            BackEvent.Register(OnBack);

            // 注册【继续】事件
            ContinueEvent.Register(OnContinue);

            // 注册【退出】事件
            QuitEvent.Register(OnQuit);
        }

        private void OnPause()
        {
            // 显示暂停菜单
            pauseUI.SetActive(true);

            Debug.Log("暂停游戏");
        }

        private void OnBack()
        {
            // 返回主界面
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

            Debug.Log("返回主界面");
        }

        private void OnContinue()
        {
            // 隐藏暂停菜单
            pauseUI.SetActive(false);

            Debug.Log("继续游戏");
        }

        private void OnQuit()
        {
            // 退出游戏(切换主界面)
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

            Debug.Log("退出游戏");
        }

        private void OnDestroy()
        {
            // 注销【暂停】事件
            PauseEvent.Unregister(OnPause);

            // 注销【返回】事件
            BackEvent.Unregister(OnBack);

            // 注销【继续】事件
            ContinueEvent.Unregister(OnContinue);

            // 注销【退出】事件
            QuitEvent.Unregister(OnQuit);
        }
    }

}
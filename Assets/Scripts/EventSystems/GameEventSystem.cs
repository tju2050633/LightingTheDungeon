using UnityEngine;

/*
    游戏场景事件系统，挂载到Game场景的EventSystem上
*/

namespace LightingTheDungeon
{
    public class GameEventSystem : MonoBehaviour
    {
        // PauseUI面板
        public GameObject pauseUI;

        // 玩家
        public Player player;

        // 胜利/失败 动画
        public GameObject victoryAnim;
        public GameObject gameOverAnim;

        void Start()
        {
            // 激活输入管理器
            InputManager.Instance.Activate();

            // 注册【暂停】事件
            PauseEvent.Register(OnPause);

            // 注册【返回】事件
            BackEvent.Register(OnBack);

            // 注册【继续】事件
            ContinueEvent.Register(OnContinue);

            // 注册【退出】事件
            QuitEvent.Register(OnQuit);

            // 注册【移动】事件
            MoveEvent.Register(OnMove);

            // 注册【停止】事件
            StopEvent.Register(OnStop);

            // 注册【失败】事件
            GameOverEvent.Register(OnGameOver);

            // 注册【胜利】事件
            VictoryEvent.Register(OnVictory);

            // 播放BGM
            AudioManager.Instance.PlayBGM("GameScene");
        }

        private void OnPause()
        {
            // 暂停BGM
            AudioManager.Instance.PauseBGM();

            // 显示暂停菜单
            pauseUI.SetActive(true);
        }

        private void OnBack()
        {
            // 停止BGM
            AudioManager.Instance.StopBGM();

            // 返回主界面
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

        private void OnContinue()
        {
            // 继续BGM
            AudioManager.Instance.PlayBGM("GameScene");

            // 隐藏暂停菜单
            pauseUI.SetActive(false);
        }

        private void OnQuit()
        {
            // 停止BGM
            AudioManager.Instance.StopBGM();

            // 退出游戏(切换主界面)
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

        private void OnMove()
        {
            // 播放行走动画
            player.setWalkAnim(true);

            // 设置玩家速度
            Vector2 v = new Vector2(0, player.rb.velocity.y);
            if (Input.GetKeyDown(KeyCode.W) && player.onGround)
            {
                v.y = player.speed_y;
                player.onGround = false;
            }

            if (Input.GetKey(KeyCode.A))
            {
                v.x = -player.speed_x;

                // 翻转角色
                player.transform.localScale = new Vector3(-1, 1, 1);
            }
            if (Input.GetKey(KeyCode.D))
            {
                v.x = player.speed_x;

                // 翻转角色
                player.transform.localScale = new Vector3(1, 1, 1);
            }

            // 移动玩家
            player.setVelocity(v);
        }

        private void OnStop()
        {
            // 播放idle动画
            player.setWalkAnim(false);

            // 停止玩家
            if (Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.D)
            || Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                player.setVelocity(new Vector2(0, player.rb.velocity.y));
            }
        }

        private void OnGameOver()
        {
            // 播放失败音乐
            AudioManager.Instance.PlayBGM("GameOver", false);

            // 隐藏UI
            GameObject.Find("UI").gameObject.SetActive(false);

            // 播放失败动画
            gameOverAnim.SetActive(true);
        }

        private void OnVictory()
        {
            // 播放胜利音乐
            AudioManager.Instance.PlayBGM("Victory", false);

            // 隐藏UI
            GameObject.Find("UI").gameObject.SetActive(false);

            // 播放胜利动画或跳转场景
            if (victoryAnim == null)
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
            else
                victoryAnim.SetActive(true);
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

            // 注销【移动】事件
            MoveEvent.Unregister(OnMove);

            // 注销【停止】事件
            StopEvent.Unregister(OnStop);

            // 注销【失败】事件
            GameOverEvent.Unregister(OnGameOver);

            // 注销【胜利】事件
            VictoryEvent.Unregister(OnVictory);
        }
    }

}
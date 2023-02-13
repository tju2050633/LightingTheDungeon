using UnityEngine;

namespace LightingTheDungeon
{
    public class InputManager : Singleton<InputManager>
    {
        public bool anyKeyToContinue = false;
        public void Activate()
        {
            // 添加Update的监听
            MonoManager.Instance.AddUpdateListener(InputUpdate);
        }

        private void InputUpdate()
        {
            // 移动命令检测
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                // 触发移动事件
                MoveEvent.Invoke();
            }

            // 移动命令停止检测
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                // 触发停止事件
                StopEvent.Invoke();
            }

            // 任意键继续
            if (anyKeyToContinue && Input.anyKeyDown)
            {
                // 切换到主界面
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

                anyKeyToContinue = false;
            }
        }

    }
}




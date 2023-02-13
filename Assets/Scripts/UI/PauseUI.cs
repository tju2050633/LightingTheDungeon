using UnityEngine;
using UnityEngine.UI;

/*
    暂停菜单界面
*/

namespace LightingTheDungeon
{
    public class PauseUI : MonoBehaviour
    {
        void Start()
        {
            // 委托：【继续游戏】点击事件
            transform.Find("BtnContinue").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySound("Click");
                new ContinueCommand().Execute();
            });

            // 委托：【退出游戏】点击事件
            transform.Find("BtnQuit").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySound("Click");
                new QuitCommand().Execute();
            });
        }
    }
}

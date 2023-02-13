using UnityEngine;
using UnityEngine.UI;

/*
    主菜单
*/

namespace LightingTheDungeon
{
    public class MainMenuUI : MonoBehaviour
    {
        void Start()
        {
            // 委托：【开始游戏】点击事件
            transform.Find("BtnStart").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySound("Click");
                new StartCommand().Execute();
            });
        }
    }
}

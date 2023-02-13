using UnityEngine;
using UnityEngine.UI;

/*
    游戏场景UI层
*/

namespace LightingTheDungeon
{
    public class GameUI : MonoBehaviour
    {
        void Start()
        {
            // 委托：【暂停】点击事件
            transform.Find("BtnPause").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySound("Click");
                new PauseCommand().Execute();
            });

            // 委托：【返回】点击事件
            transform.Find("BtnBack").GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySound("Click");
                new BackCommand().Execute();
            });
        }
    }
}

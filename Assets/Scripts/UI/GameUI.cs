using UnityEngine;
using UnityEngine.UI;

/*
    游戏场景UI层
*/

namespace GloryOfDead
{
    public class GameUI : MonoBehaviour
    {
        void Start()
        {
            // 委托：【暂停】点击事件
            transform.Find("BtnPause").GetComponent<Button>().onClick.AddListener(() =>
            {
                new PauseCommand().Execute();
            });

            // 委托：【返回】点击事件
            transform.Find("BtnBack").GetComponent<Button>().onClick.AddListener(() =>
            {
                new BackCommand().Execute();
            });
        }
    }
}

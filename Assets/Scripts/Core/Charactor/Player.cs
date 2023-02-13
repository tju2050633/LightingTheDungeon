using UnityEngine;

// 动画控制器

/*
    玩家类
*/

namespace LightingTheDungeon
{
    public class Player : MonoBehaviour
    {
        public Rigidbody2D rb;
        public float speed_x;
        public float speed_y;

        public bool onGround;

        // idle动画，行走动画
        public RuntimeAnimatorController idle;
        public RuntimeAnimatorController walk;


        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            speed_x = 3;
            speed_y = 6;
        }

        public void setVelocity(Vector2 v)
        {
            rb.velocity = v;
        }

        public void setWalkAnim(bool walk)
        {
            if(walk)
                GetComponent<Animator>().runtimeAnimatorController = this.walk;
            else
                GetComponent<Animator>().runtimeAnimatorController = this.idle;
                
        }

        // 与墙体碰撞回调函数
        public void OnCollisionEnter2D(Collision2D collision)
        {
            // 如果是底部碰撞，设置onGround为true
            // if (collision.contacts[0].normal.y == 1)
                onGround = true;

            // 如果碰撞到最低端，游戏结束
            if(collision.gameObject.tag == "Bottom")
            {
                AudioManager.Instance.PlaySound("HumanDie");
                // 触发游戏结束事件
                GameOverEvent.Invoke();
            }

            // 如果碰撞到敌人，被杀害
            if (collision.gameObject.tag == "Werewolf" || collision.gameObject.tag == "Vampire")
            {
                AudioManager.Instance.PlaySound("HumanDie");
                // 触发游戏结束事件
                GameOverEvent.Invoke();
            }

            // 如果碰到终点，游戏胜利
            if (collision.gameObject.tag == "Destination")
            {
                // 触发游戏胜利事件
                VictoryEvent.Invoke();
            }
        }
    }
}



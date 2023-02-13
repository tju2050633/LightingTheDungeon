using UnityEngine;

namespace LightingTheDungeon
{
    public class AnimOnce : MonoBehaviour
    {
        private Animator animator;

        public GameObject background;


        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            // 即将播放完时，停止在最后一帧
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                background.SetActive(true);
                gameObject.SetActive(false);
                InputManager.Instance.anyKeyToContinue = true;
            }
        }
    }
}
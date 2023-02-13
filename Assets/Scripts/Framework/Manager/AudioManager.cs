using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LightingTheDungeon
{

    public class AudioManager : Singleton<AudioManager>
    {
        #region 路径常量
        private const string BGM_PATH = "Audio/BGM/"; //BGM音频文件路径
        private const string SOUND_PATH = "Audio/Sound/"; //音效文件路径
        #endregion

        #region 当前音量记录
        [HideInInspector]
        public float bgm_volume = 1f, sound_volume = 1f;
        #endregion

        #region 核心组件与容器
        //BGM组件
        private AudioSource audio;

        //音效组件
        private GameObject sound;
        private List<AudioSource> soundList = new List<AudioSource>();

        //控制音效播放组件的移除
        public AudioManager()
        {
            if (GameObject.Find("Audio") == null) audio = new GameObject("Audio").AddComponent<AudioSource>();
            //判断Sound是否存在
            if (GameObject.Find("Sound") == null) sound = new GameObject("Sound");

            //加入帧更新测试，如果音效播放完成则删除组件
            MonoManager.Instance.AddUpdateListener(() =>
            {
                for (int i = soundList.Count - 1; i >= 0; --i)
                {
                    if (!soundList[i].isPlaying)
                    {
                        GameObject.Destroy(soundList[i]);
                        soundList.Remove(soundList[i]);
                    }
                }
            });

            GameObject.DontDestroyOnLoad(audio);
            GameObject.DontDestroyOnLoad(sound);
        }
        #endregion

        #region BGM相关
        //开始播放BGM
        /*
            Param1:bgm在Resources目录下"Audio/BGM/"的文件名
            Param2:bgm是否循环播放
        */
        public void PlayBGM(string bgmName, bool isLoop = true)
        {
            ResourcesManager.Instance.LoadAsync<AudioClip>(BGM_PATH + bgmName, (clip) =>
           {
               audio.clip = clip;
               audio.loop = isLoop;
               audio.volume = bgm_volume;
               audio.Play();
           });
        }

        //停止音乐播放
        public void StopBGM()
        {
            if (audio != null) audio.Stop();
        }

        //暂停播放音乐
        public void PauseBGM()
        {
            if (audio != null) audio.Pause();
        }

        //改变背景音乐音量
        public void SwitchVolume(float volume)
        {
            if (audio == null) return;
            if (volume > 1 || volume < 0) return; //限制音量为0——1之间
            audio.volume = volume;
            bgm_volume = volume; //单独记录一份
        }
        #endregion

        #region 音效相关
        //播放音效
        public void PlaySound(string soundName, UnityAction<AudioSource> callback = null, bool isLoop = false)
        {
            //异步加载资源
            ResourcesManager.Instance.LoadAsync<AudioClip>(SOUND_PATH + soundName, (clip) =>
             {
                 AudioSource sound_Audio = sound.AddComponent<AudioSource>();
                 sound_Audio.clip = clip;
                 sound_Audio.loop = isLoop;
                 sound_Audio.volume = sound_volume;
                 sound_Audio.Play();
                 //加入列表
                 soundList.Add(sound_Audio);
                 if (callback != null) callback(sound_Audio);
             });
        }

        //停止所有音效
        public void StopAllSound()
        {
            if (soundList.Count == 0) return;
            foreach (AudioSource au in sound.GetComponents<AudioSource>())
            {
                GameObject.Destroy(au);
                soundList.Remove(au);
            }
        }
        #endregion
    }
}
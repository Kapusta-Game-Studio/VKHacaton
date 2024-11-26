using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup effects_mix;
        [SerializeField] private AudioMixerGroup music_mix;
        public AudioClip CurrentMusic { get; private set; }
        public string CurrentMusicName => CurrentMusic != null ? CurrentMusic.name : string.Empty;

        private Dictionary<string, AudioClip> effects;
        private Dictionary<string, AudioClip> musics;

        internal static AudioManager Instance { get; private set; }

        private void Awake()
        {
            effects = InitializeAudio("Audio/Effects/");
            musics = InitializeAudio("Audio/Music/");
        }

        private void OnEnable()
        {
            if (Instance != this)
                DestroyImmediate(Instance);
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private Dictionary<string, AudioClip> InitializeAudio(string path)
        {
            Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();

            AudioClip[] audios = Resources.LoadAll<AudioClip>(path);

            foreach (AudioClip item in audios)
                clips.Add(item.name, item);

            return clips;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            List<Button> buttons = new List<Button>(Resources.FindObjectsOfTypeAll<Button>());
            foreach (Button item in buttons)
                item.onClick.AddListener(() => AudioManager.Instance.PlaySound("Click"));
        }
        internal AudioSource PlaySound(string name, Vector3 pos = new Vector3(), bool voluminous = false)
        {
            if (!effects.ContainsKey(name))
            {
                Debug.LogError("No such sound: Audio/Effects/" + name);
                return null;
            }

            GameObject obj = new GameObject("Effect");
            obj.transform.position = pos;
            obj.transform.parent = transform;

            AudioSource
                audio = obj
                    .AddComponent<
                        AudioSource>(); 
            audio.clip = effects[name];
            audio.outputAudioMixerGroup = effects_mix;
            if (voluminous) 
                audio.spatialBlend = 0.7f;
            audio.Play();

            Destroy(obj, audio.clip.length);
            return audio;
        }

        internal void PlayMusic(string name, Vector3 pos = new Vector3())
        {
            GameObject o = this.gameObject;
            AudioSource audioSource = o != null && o.GetComponent<AudioSource>() ?
                gameObject.GetComponent<AudioSource>() : gameObject.AddComponent<AudioSource>();// TODO: Добавление компонента на объект - это очень медленная операция

            if (audioSource.clip != musics[name])
            {
                audioSource.clip = musics[name];
                CurrentMusic = audioSource.clip;
                audioSource.outputAudioMixerGroup = music_mix;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }
}

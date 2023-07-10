using UnityEngine;

namespace MaximovInk.NumberSort
{
    public class SoundManager : MonoBehaviourSingleton<SoundManager>
    {
        public enum SoundPlayType
        {
            Background,
            Fail,
            Complete,
            Stop
        }

        [SerializeField]
        private AudioClip BackgroundClip;
        [SerializeField]
        private AudioClip FailClip;
        [SerializeField]
        private AudioClip CompleteClip;

        [SerializeField]
        private AudioSource AudioSource;

        private bool backgroundIsPlaying = false;

        private void Awake()
        {
            
            DontDestroyOnLoad(this);
            SoundManager.Instance.PlaySound(SoundManager.SoundPlayType.Background);
            AudioSource.clip = BackgroundClip;
        }

        public void ToggleAudio()
        {
            AudioSource.volume = AudioSource.volume > 0f ? 0 : 1f;
        }

        public bool AudioIsEnabled()
        {
            return AudioSource.volume > 0f;
        }

        public void PlaySound(SoundPlayType type)
        {
            if (backgroundIsPlaying && type == SoundPlayType.Background)
               return;

            if (AudioSource == null) return;
            if (FailClip == null) return;
            if (CompleteClip == null) return;

            //NOT GOOD BUT WORKS
            switch (type)
            {
                case SoundPlayType.Background:
                    PlayerPrefs.SetInt("Tutorial", 0);
                    AudioSource.Play();
                    backgroundIsPlaying = true;
                    break;
                case SoundPlayType.Fail:
                   
                    AudioSource.PlayOneShot(FailClip);
                    break;
                case SoundPlayType.Complete:
                    AudioSource.PlayOneShot(CompleteClip);
                    break;
                case SoundPlayType.Stop:
                    AudioSource.Stop();
                    break;
                default:
                    break;
            }


        }
    }
}
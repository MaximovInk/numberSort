using UnityEngine;

namespace MaximovInk.NumbersSort
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

            AudioSource.clip = BackgroundClip;
        }

        public void PlaySound(SoundPlayType type)
        {
            if (backgroundIsPlaying && type == SoundPlayType.Background)
               return;

            //NOT GOOD BUT WORKS
            switch (type)
            {
                case SoundPlayType.Background:
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
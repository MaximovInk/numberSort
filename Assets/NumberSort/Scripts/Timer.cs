
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MaximovInk.NumbersSort
{
    public class Timer : MonoBehaviour
    {
        public Slider slider;
        public TextMeshProUGUI infoText;

        public float initSeconds = 10f;
        private float seconds = 5f;

        private void Awake()
        {
            seconds = initSeconds;
        }

        private void Update()
        {
            seconds -= Time.deltaTime;

            if (seconds <= 0)
            {
                LevelManager.Instance.FailLevel();
                seconds = 100;
                return;
            }

            slider.value = seconds / initSeconds;
            infoText.text = $"Осталось {(int)(seconds)} секунд";

        }
    }
}
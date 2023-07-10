using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MaximovInk.NumberSort
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

        public void Hide() {
            slider.gameObject.SetActive(false);
            infoText.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (LevelGenerator.isTutorial)
                return;

            seconds -= Time.deltaTime;

            if (seconds <= 0)
            {
                LevelManager.Instance.FailLevel();
                seconds = 100;
                return;
            }

            slider.value = seconds / initSeconds;
            infoText.text = $"Left {(int)(seconds)} sec";

        }
    }
}
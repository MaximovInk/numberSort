using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        slider.value = seconds / initSeconds;
        infoText.text = $"Осталось {(int)(seconds)} секунд"; 

    }
}

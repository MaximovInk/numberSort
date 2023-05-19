using MaximovInk.NumbersSort;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public Image icon;
    public Sprite spriteOn, spriteOff;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            SoundManager.Instance.ToggleAudio();
            UpdateGraphic();
        });
        UpdateGraphic();
    }

    private void UpdateGraphic()
    {
        icon.sprite = SoundManager.Instance.AudioIsEnabled() ? spriteOn : spriteOff;
    }
}

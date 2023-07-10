using UnityEngine;
using UnityEngine.UI;

namespace MaximovInk.NumberSort
{
    [System.Serializable]
    public class DiceSkin
    {
        public Sprite[] Sprites;
    }

    public class SkinManager : MonoBehaviourSingleton<SkinManager>
    {
        public Image Background;

        private int diceID;
        private int backgroundID;

        public Sprite[] Backgrounds;
        public DiceSkin[] Dices;

        private bool isLoaded;

        void Start()
        {
            Init();
        }

        void Init()
        {
            backgroundID = PlayerPrefs.GetInt("BackgroundID");
            diceID = PlayerPrefs.GetInt("DiceID");

            Background.sprite = Backgrounds[backgroundID];

            isLoaded = true;
        }

        public Sprite GetSprite(int value)
        {
            if (!isLoaded) Init();

            var skin = Dices[diceID];

             var index = Mathf.Clamp(value - 1, 0, skin.Sprites.Length);

            return skin.Sprites[index];
        }
    }

    
}
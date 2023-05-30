using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MaximovInk.NumbersSort
{
    public class NumberSortManager : MonoBehaviourSingleton<NumberSortManager>
    {
        [SerializeField]
        private Sprite[] spritesInit;

        private Dictionary<int, Sprite> sprites = new Dictionary<int, Sprite>();

        public Sprite UnknownNumber;

        public NumberInstance NumberInstancePrefab { get => m_NumberInstancePrefab; }

        public Canvas canvas;

        [SerializeField]
        private NumberInstance m_NumberInstancePrefab;

        [SerializeField]
        private TextMeshProUGUI m_CompleteInfo;

        [SerializeField]
        private TextMeshProUGUI m_StepsText;



        public int LevelMaxSteps = 20;
        private int LevelStepsCounter = 0;

        private void InitDictionary()
        {
            for (int i = 0; i < spritesInit.Length; i++)
            {
                sprites.Add(i, spritesInit[i]);
            }
        }

        public Sprite GetSprite(int value)
        {
            if (sprites.Count == 0) InitDictionary();

            var index = Mathf.Clamp(value - 1, 0, spritesInit.Length);

            return sprites[index];
        }

        public void SetMaxSteps(int steps)
        {
            LevelMaxSteps = steps;
        }

        public void MakeStep()
        {
            if(LevelGenerator.Instance.IsGenerated)
                LevelStepsCounter++;
        }

        public void CheckComplete()
        {
            m_StepsText.text = $"Steps left: {Mathf.Max(0, LevelMaxSteps - LevelStepsCounter)}";

            var containers = GetComponentsInChildren<NumbersContainer>();

            bool complete = true; 
            foreach ( var container in containers )
            {
                if (container.Count == 10 || container.Count == 0)
                    continue;

                complete = false;
            }

            if(complete)
            {
                LevelManager.Instance.CompleteLevel();
            }
            else
            {
                if (LevelStepsCounter >= LevelMaxSteps)
                    LevelManager.Instance.FailLevel();
            }

            m_CompleteInfo.text = complete ? "Complete!" : "Not completed";
            m_CompleteInfo.color = complete ? Color.green : Color.white;
        }
    }
}

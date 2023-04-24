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

        public NumberInstance NumberInstancePrefab { get => m_NumberInstancePrefab; }

        public Canvas canvas;

        [SerializeField]
        private NumberInstance m_NumberInstancePrefab;

        [SerializeField]
        private TextMeshProUGUI m_CompleteInfo;

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

        public void CheckComplete()
        {
            var containers = GetComponentsInChildren<NumbersContainer>();

            bool complete = true; 
            foreach ( var container in containers )
            {
                if (container.count == 10 || container.count == 0)
                    continue;

                complete = false;
            }

            m_CompleteInfo.text = complete ? "Complete!" : "Not completed";
            m_CompleteInfo.color = complete ? Color.green : Color.white;
        }
    }
}

using UnityEngine;

namespace MaximovInk.NumbersSort
{
    public class LevelManager : MonoBehaviourSingleton<LevelManager>
    {
        [SerializeField]
        private RectTransform parent;
        [SerializeField]
        private NumbersContainer numbersContainerPrefab;

        public NumbersContainer AddContainer()
        {
            var container = Instantiate(numbersContainerPrefab, parent);

            return container;
        }

    }
}

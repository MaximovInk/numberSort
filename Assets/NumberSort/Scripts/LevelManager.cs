using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaximovInk.NumbersSort
{
    public class LevelManager : MonoBehaviourSingleton<LevelManager>
    {
        [SerializeField]
        private RectTransform parent;
        [SerializeField]
        private NumbersContainer numbersContainerPrefab;

        private void Awake()
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundPlayType.Background);
        }

        public void FailLevel()
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundPlayType.Fail);


            //FAIL MENU

            ExecuteAfterDelay(() =>
            { 
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }, 0.5f);
        }

        public void CompleteLevel()
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundPlayType.Complete);


            //Complete MENU

            ExecuteAfterDelay(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }, 0.5f);
        }


        public NumbersContainer AddContainer()
        {
            var container = Instantiate(numbersContainerPrefab, parent);

            return container;
        }

        public static void ExecuteAfterFrame(Action action)
        {
            Instance.StartCoroutine(Instance.ExecuteAfterFrameCoorutine(action));
        }

        public static void ExecuteAfterDelay(Action action, float delay)
        {
            Instance.StartCoroutine(Instance.ExecuteAfterDelayCoorutine(action, delay));
        }

        private IEnumerator ExecuteAfterFrameCoorutine(Action action)
        {
            yield return null;

            action.Invoke();
        }

        private IEnumerator ExecuteAfterDelayCoorutine(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);

            action.Invoke();
        }


    }
}

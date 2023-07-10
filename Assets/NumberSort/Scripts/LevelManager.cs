using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaximovInk.NumberSort
{
    public class LevelManager : MonoBehaviourSingleton<LevelManager>
    {
        [SerializeField]
        private RectTransform parent;
        [SerializeField]
        private NumbersContainer numbersContainerPrefab;

        [SerializeField]
        private GameObject FailMenu;
        [SerializeField]
        private GameObject CompleteMenu;

        private void Awake()
        {
            
        }

        public void FailLevel()
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundPlayType.Fail);

            //FAIL MENU

            FailMenu.SetActive(true);
            Time.timeScale = 0;

            /*
             ExecuteAfterDelay(() =>
            { 
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }, 0.5f);
             */
        }

        public void CompleteLevel()
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundPlayType.Complete);

            //Complete MENU

            CompleteMenu.SetActive(true);
            Time.timeScale = 0;

            /*
              ExecuteAfterDelay(() =>
             {
                 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
             }, 0.5f);
             */
        }

        private int containerCounter = 0;

        public List<NumbersContainer> containers = new List<NumbersContainer>();

        public NumbersContainer AddContainer()
        {
            var container = Instantiate(numbersContainerPrefab, parent);

            container.Index = containerCounter;

            containerCounter++;

            containers.Add(container);

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

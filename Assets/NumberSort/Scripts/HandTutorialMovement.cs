using UnityEngine;

namespace MaximovInk.NumberSort
{
    [System.Serializable]
    public struct Step {
        public int Container;
        public int Number;
    }
   
    public class HandTutorialMovement : MonoBehaviour
    {
        public Step[] src;
        public Step[] dst;

        public int Step { get; private set; }

        public int GetCurrentSrcContainerIdx()
        {
            return src[Step].Container;
        }

        public int GetCurrentDstContainerIdx()
        {
            return dst[Step].Container;
        }

        public GameObject CompleteTutorialObject;

        public void NextStep() {
            Step++;
            if(Step == src.Length)
            {
                Step = 0;
                CompleteTutorialObject.SetActive(true);
                gameObject.SetActive(false);
                LevelGenerator.Instance.FinishTutorial();
            }
        }

        private float moveLerp = 0f;

        public float moveSpeed = 1f; 
        public AnimationCurve moveCurve;
        public Timer timer;

        private void OnEnable()
        {
            timer.Hide();
        }

        private void Update()
        {
            moveLerp += Time.deltaTime * moveSpeed;
            if(moveLerp > 1f)
            {
                moveLerp = 0f;
            }

            var finalLerp = moveCurve.Evaluate(moveLerp);

            var srcContainer = LevelManager.Instance.containers[src[Step].Container];
            var dstContainer = LevelManager.Instance.containers[dst[Step].Container];

            Vector2 from = srcContainer.transform.position;
            Vector2 to = dstContainer.transform.position;

            transform.position = Vector3.Lerp(from, to, finalLerp);
        }
    }
}
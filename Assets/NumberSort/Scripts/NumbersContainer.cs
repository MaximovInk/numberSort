using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;
using System;

namespace MaximovInk.NumbersSort {
    public class NumbersContainer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private TextMeshProUGUI m_CountText;

        [SerializeField]
        private Transform m_NumbersParent;

        public int count { get; private set; } = 0;

        private void Start()
        {
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            var nums = GetComponentsInChildren<NumberInstance>();
            int count = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                count += nums[i].Value;
            }

            m_CountText.text = count.ToString();

            if(count == 10)
                m_CountText.color = Color.green;
            else
                m_CountText.color = Color.red;

            this.count = count;

            NumberSortManager.Instance.CheckComplete();
        }

        private const float ADD_DURATION = 0.15F;
        private const float MOVING_DURATION = 0.15F;

        public bool Add(int value, Vector2 initPos)
        {
            if (value <= 0) return false;
            if (m_NumbersParent.childCount >= 4) return false;

            var num1 = Instantiate(NumberSortManager.Instance.NumberInstancePrefab, m_NumbersParent);

            num1.Value = value;

            num1.transform.SetAsFirstSibling();
            num1.SetVisible(false);

            UpdateCounter();

            var canvas = NumberSortManager.Instance.canvas;
            var numAnim = Instantiate(NumberSortManager.Instance.NumberInstancePrefab, canvas.transform);
            numAnim.transform.localPosition = initPos;
            numAnim.Value = value;
            numAnim.name = "Anim";
            numAnim.SetVisible(true);

            ExecuteAfterFrame(() =>
            {
                var tween = numAnim.transform.DOMove(num1.transform.position, ADD_DURATION);

                tween.onKill += () =>
                {
                    Destroy(numAnim.gameObject);
                    num1.SetVisible(true);
                };
            });


            return true;
        }

        private void ExecuteAfterFrame(Action action)
        {
            StartCoroutine(ExecuteAfterFrameCoorutine(action));
        }

        IEnumerator ExecuteAfterFrameCoorutine(Action action)
        {
            yield return  null;

            action.Invoke();
        }

        public bool Add(int value)
        {
            if (value <= 0) return false;
            if (m_NumbersParent.childCount >= 4) return false;

            var num1 = Instantiate(NumberSortManager.Instance.NumberInstancePrefab, m_NumbersParent);
            num1.Value = value;

            num1.transform.SetAsFirstSibling();

            UpdateCounter();

            return true;
        }

        private bool isDrag = false;

        private Transform movingImage;

        public void OnEndDrag(PointerEventData eventData)
        {
            var end = eventData.pointerCurrentRaycast.gameObject?.GetComponentInParent<NumbersContainer>();

            var val = movingImage.GetComponent<NumberInstance>().Value;

            if (end != null && end != this)
            {
                bool can = end.Add(val, movingImage.localPosition);
                if (!can)
                    Add(val, movingImage.localPosition);
            }
            else
                Add(val, movingImage.localPosition);

            movingImage.DOKill();

            Destroy(movingImage.gameObject);

            UpdateCounter();
        }

        private static Vector2 mouseCanvasPos = Vector2.zero;

        public void OnDrag(PointerEventData eventData)
        {
            if(isDrag)
            {
                var canvas = NumberSortManager.Instance.canvas;
                Vector2 mousePos = Input.mousePosition;
                Vector2 screenSize = new Vector2(Screen.width, Screen.height);
                Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;
                float screenAspect = screenSize.x / screenSize.y;
                float canvasAspect = canvasSize.x / canvasSize.y;
                float correction = canvasAspect / screenAspect;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvas.transform as RectTransform,
                    mousePos, Camera.main,
                    out Vector2 canvasPos
                    );
                if (canvasAspect > screenAspect)
                    canvasPos.y *= correction;
                else
                    canvasPos.x /= correction;

                var currentRect = (this.transform as RectTransform);

                var halfHeight = currentRect.rect.height / 2f;
                var halfWidth = currentRect.rect.width / 2f;

                mouseCanvasPos = canvasPos;

                var newPos = canvasPos;
                newPos.y = currentRect.transform.localPosition.y + halfHeight;


                var end = eventData.pointerCurrentRaycast.gameObject?.GetComponentInParent<NumbersContainer>();

                if (end != null)
                {
                    var delta = 1f-Mathf.Abs(canvasPos.x - end.transform.localPosition.x)/ halfWidth;

                    newPos.y -= halfHeight * delta / 5f;
                    newPos.x = end.transform.localPosition.x;

                    newPos.x = Mathf.Lerp(newPos.x, mouseCanvasPos.x, delta/5f);
                }

                if(movingImage != null)
                    movingImage.transform.DOLocalMove(newPos, MOVING_DURATION);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (m_NumbersParent.childCount > 0)
            {
                isDrag = true;
                movingImage = m_NumbersParent.GetChild(0);
                movingImage.SetParent(NumberSortManager.Instance.canvas.transform);
            }
        }
    }
}
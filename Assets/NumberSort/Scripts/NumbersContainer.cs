
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MaximovInk.NumbersSort {
    public class NumbersContainer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private TextMeshProUGUI m_CountText;

        [SerializeField]
        private Transform m_NumbersParent;

        public bool init;

        private static bool init_ok = false;

        public int count { get; private set; } = 0;

        private void Start()
        {
            if (init)
            {
                if (init_ok)
                {
                    Add(2);
                    Add(5);
                    Add(4);
                    Add(1);
                }
                else
                {
                    Add(4);
                    Add(3);
                    Add(1);
                    init_ok = true;
                }
            }

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
                bool can = end.Add(val);
                if (!can)
                    Add(val);
            }
            else
                Add(val);

            Destroy(movingImage.gameObject);

            UpdateCounter();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(isDrag)
            {

                /*
                 
                  var canvas = NumberSortManager.Instance.canvas;
                 Vector2 mousePos = Input.mousePosition;

                 // Получаем размеры экрана
                 Vector2 screenSize = new Vector2(Screen.width, Screen.height);

                 // Получаем размеры Canvas в пикселях
                 Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

                 // Вычисляем соотношение размеров экрана и Canvas
                 float canvasScale = canvasSize.y / screenSize.y;

                 // Вычисляем позицию мыши в координатах Canvas
                 Vector2 canvasPos = (mousePos / canvasScale) - (canvasSize / 2f);

                 movingImage.transform.localPosition = canvasPos;
                 */

                var canvas = NumberSortManager.Instance.canvas;
                Vector2 mousePos = Input.mousePosition;
                // Получаем размеры экрана
                Vector2 screenSize = new Vector2(Screen.width, Screen.height);
                // Получаем размеры Canvas в пикселях
                Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;
                // Получаем соотношение сторон экрана
                float screenAspect = screenSize.x / screenSize.y;
                // Получаем соотношение сторон Canvas
                float canvasAspect = canvasSize.x / canvasSize.y;
                // Вычисляем поправку для координат мыши
                float correction = canvasAspect / screenAspect;
                // Вычисляем позицию мыши в координатах Canvas
                Vector2 canvasPos = Vector2.zero;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePos, Camera.main, out canvasPos);
                // Корректируем позицию мыши в зависимости от соотношения сторон
                if (canvasAspect > screenAspect)
                    canvasPos.y *= correction;
                else
                    canvasPos.x /= correction;
                // Устанавливаем позицию movingImage в полученных координатах
                movingImage.transform.localPosition = canvasPos;
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
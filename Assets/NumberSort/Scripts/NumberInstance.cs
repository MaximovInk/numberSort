using UnityEngine;
using UnityEngine.UI;

namespace MaximovInk.NumbersSort
{
    public class NumberInstance : MonoBehaviour
    {
        public int Value;

        private Image m_Image;

        private void Awake()
        {
            m_Image = GetComponent<Image>();
        }

        private void Start()
        {
            m_Image.sprite = 
                NumberSortManager.Instance.GetSprite(Value);
        }
    }
}

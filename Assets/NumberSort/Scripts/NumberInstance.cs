using UnityEngine;
using UnityEngine.UI;

namespace MaximovInk.NumberSort
{
    public class NumberInstance : MonoBehaviour
    {
        public int Value;

        private Image m_Image;

        public bool isUnknown;

        private void Awake()
        {
            m_Image = GetComponent<Image>();
        }

        private void Start()
        {
            UpdateSprite();
        }

        public void SetVisible(bool visible)
        {
            UpdateSprite();

            m_Image.color = visible ? Color.white : Color.clear;
        }

        private void UpdateSprite()
        {
            if (isUnknown)
                m_Image.sprite =
                   NumberSortManager.Instance.UnknownNumber;
            else
                m_Image.sprite =
               NumberSortManager.Instance.GetSprite(Value);
        }
    }
}

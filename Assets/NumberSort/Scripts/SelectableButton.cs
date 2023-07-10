using UnityEngine;
using UnityEngine.UI;

namespace MaximovInk.NumberSort { 

    public class SelectableButton : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        public void Init(int index, SelectableMenu menu)
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(() => {
                menu.OnSelectItem(index);
            });
        }

        public void SetColor(Color color)
        {
            image.color = color;
        }
    }
}
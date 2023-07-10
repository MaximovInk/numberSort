using System;
using UnityEngine;

namespace MaximovInk.NumberSort
{
    public class SelectableMenu : MonoBehaviour
    {
        private SelectableButton[] buttons;

        public Color ColorNormal;
        public Color ColorSelected;

        public Color MultiplyColor;

        public string SaveID;

        public Action OnSelectChange;

        private int prevSelect = 0;

        protected virtual void Awake()
        {
            buttons = GetComponentsInChildren<SelectableButton>();
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Init(i, this);
            }
            prevSelect = PlayerPrefs.GetInt(SaveID, 0);
            SetSelected(prevSelect);
        }

        public void OnSelectItem(int index)
        {
            if (prevSelect == index)
                return;

            PlayerPrefs.SetInt(SaveID, index);
            SetSelected(index);
            OnSelectChange?.Invoke();
            prevSelect = index;
        }

        protected void SetSelected(int index)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetColor(ColorNormal * MultiplyColor);
            }

            buttons[index].SetColor(ColorSelected* MultiplyColor);
        }
    }
}

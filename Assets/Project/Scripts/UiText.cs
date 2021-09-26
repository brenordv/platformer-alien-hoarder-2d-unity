using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class UiText : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        public void UpdateUi(string text)
        {
            _text.text = text;
        }
    }
}
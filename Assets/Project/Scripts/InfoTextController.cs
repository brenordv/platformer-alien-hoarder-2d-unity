using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class InfoTextController : MonoBehaviour
    {
        public Text infoText;
        public float defaultInfoTime = 3f;

        private void Awake()
        {
            infoText.gameObject.SetActive(false);
        }

        public void ShowInfo(string text, float? infoDuration = null)
        {
            var duration = infoDuration ?? defaultInfoTime;
            StartCoroutine(UpdateText(text, duration));
        }

        private IEnumerator UpdateText(string text, float duration)
        {
            infoText.text = text;
            infoText.gameObject.SetActive(true);
            yield return new WaitForSeconds(duration);
            infoText.gameObject.SetActive(false);
        }
    }
}
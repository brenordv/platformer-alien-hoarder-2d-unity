using UnityEngine;

namespace Project.Scripts
{
    public class Gem : MonoBehaviour
    {
        public int scoreValue = 10;

        public delegate void GemHandler(int points);

        public event GemHandler OnCollect;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            OnCollect?.Invoke(scoreValue);
            Destroy(gameObject);
        }
    }
}
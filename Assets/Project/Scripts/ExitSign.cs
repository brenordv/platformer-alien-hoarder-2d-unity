using UnityEngine;

namespace Project.Scripts
{
    public class ExitSign : MonoBehaviour
    {
        public delegate void ExitSignHandler();

        public event ExitSignHandler OnExit;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            OnExit?.Invoke();
        }
    }
}
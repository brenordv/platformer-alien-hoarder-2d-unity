using UnityEngine;

namespace Project.Scripts
{
    public class Trap : MonoBehaviour
    {
        public delegate void TrapHandler();

        public event TrapHandler OnTrigger;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            OnTrigger?.Invoke();
        }
    }
}
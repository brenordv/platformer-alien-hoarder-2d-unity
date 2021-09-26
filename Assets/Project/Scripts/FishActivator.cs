using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    public class FishActivator : MonoBehaviour
    {
        public List<Enemy> fishes;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            foreach (var fish in fishes)
            {
                fish.canMove = true;
            }
        }
    }
}
using System;

namespace Project.Scripts.Core
{
    [Serializable]
    public class CollectibleControl
    {
        public int total;
        public int collected;

        public bool CollectedAll()
        {
            return total == collected && total > 0;
        }
    }
}
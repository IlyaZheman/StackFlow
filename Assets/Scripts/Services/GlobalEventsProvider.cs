using UnityEngine;

namespace Services
{
    [DefaultExecutionOrder(-1000)]
    public class GlobalEventsProvider : MonoBehaviour
    {
        public static GlobalEventsProvider Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
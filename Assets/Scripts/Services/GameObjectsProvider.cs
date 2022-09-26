using Data.Projectile;
using UnityEngine;

namespace Services
{
    public class GameObjectsProvider : MonoBehaviour
    {
        public static GameObjectsProvider Instance { get; private set; }

        [SerializeField] private GameObject enemy;
        [SerializeField] private GameObject bullet;
        
        public GameObject Enemy => enemy;
        public GameObject Bullet => bullet;
        
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
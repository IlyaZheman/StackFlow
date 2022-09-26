using Data.Health;
using Data.Unit.MainPlayer;
using Morpeh;
using UnityEngine;

namespace Logic.Health
{
    public class EnemyImpact : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float impactCooldown;

        private float _timer;
        
        private void Update()
        {
            _timer += Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            TakeDamage(other);
        }

        private void OnTriggerStay(Collider other)
        {
            TakeDamage(other);
        }

        private void TakeDamage(Component other)
        {
            if (_timer < impactCooldown)
            {
                return;
            }

            if (!other.gameObject.TryGetComponent<EntityProvider>(out var provider))
            {
                return;
            }

            if (provider.Entity == null ||
                !provider.Entity.Has<MainPlayerComponent>() || 
                !provider.Entity.Has<HealthComponent>())
            {
                return;
            }

            ref var health = ref provider.Entity.GetComponent<HealthComponent>().Health;
            health -= damage;
            _timer = 0;
        }
    }
}
using Data.Health;
using Data.Unit.Enemy;
using Morpeh;
using UnityEngine;

namespace Logic.Projectiles
{
    public class Projectile : EntityProvider
    {
        [SerializeField] private Rigidbody projectileRigidbody;
        [SerializeField] private Transform projectileTransform;
        [SerializeField] private int damage;
        [SerializeField] private float speed;
        [SerializeField] private float lifetime;

        private float _timer;

        private void Awake()
        {
            projectileRigidbody.AddForce(projectileTransform.rotation * Vector3.forward * speed, ForceMode.Impulse);
        }

        private void FixedUpdate()
        {
            if (_timer < lifetime)
            {
                _timer += Time.fixedDeltaTime;
                return;
            }
            
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent<EntityProvider>(out var provider))
            {
                return;
            }

            if (provider.Entity == null ||
                !provider.Entity.Has<EnemyComponent>() || 
                !provider.Entity.Has<HealthComponent>())
            {
                return;
            }
            
            ref var health = ref provider.Entity.GetComponent<HealthComponent>().Health;
            health -= damage;
            
            World.Default.RemoveEntity(Entity);
            Destroy(gameObject);
        }
    }
}
using Data.Projectiles;
using Data.Unit.MainPlayer;
using Morpeh;
using UnityEngine;

namespace Logic.Projectiles
{
    public class ProjectileReplenishmentPoint : EntityProvider
    {
        [SerializeField] private Projectile ammoPrefab;
        [SerializeField] private int count;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<EntityProvider>(out var provider))
            {
                return;
            }

            if (provider.Entity == null ||
                !provider.Entity.Has<MainPlayerComponent>() || 
                !provider.Entity.Has<ProjectilesBagComponent>())
            {
                return;
            }
            
            ref var projectilesBag = ref provider.Entity.GetComponent<ProjectilesBagComponent>();
            for (var i = 0; i < count; i++)
            {
                projectilesBag.Projectiles.Push(ammoPrefab);
            }
            
            World.Default.RemoveEntity(Entity);
            Destroy(gameObject);
        }
    }
}
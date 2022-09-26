using Base;
using Data.Projectile;
using Data.Projectiles;
using Data.Spawn;
using Data.Unit;
using Data.Unit.MainPlayer;
using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Logic.Projectiles
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class CreateProjectileSystem : UpdateSystemBase
    {
        private Filter _gunFilter;
        private Filter _playerFilter;
        private Filter _bulletsContainerFilter;

        private const float Countdown = 3f;
        private float _timer;

        public override void OnAwake()
        {
            _gunFilter = World.Filter
                .With<GunComponent>()
                .With<TransformComponent>();
            _playerFilter = World.Filter
                .With<MainPlayerComponent>()
                .With<ProjectilesBagComponent>();
            _bulletsContainerFilter = World.Filter
                .With<BulletsSpawnComponent>()
                .With<TransformComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            if (_playerFilter.Length == 0 || _gunFilter.Length == 0)
            {
                return;
            }
            
            if (_timer < Countdown)
            {
                _timer += deltaTime;
                return;
            }

            _timer = 0;

            var containerEntity = _bulletsContainerFilter.First();
            ref var containerTransform = ref containerEntity.GetComponent<TransformComponent>().transform;
            
            var playerEntity = _playerFilter.First();
            ref var projectileBag = ref playerEntity.GetComponent<ProjectilesBagComponent>().Projectiles;
            
            var gunEntity = _gunFilter.First();
            ref var gunTransform = ref gunEntity.GetComponent<TransformComponent>().transform;

            if (projectileBag.Count == 0)
            {
                return;
            }
            
            var projectile = projectileBag.Pop();
            Object.Instantiate(projectile, gunTransform.position, gunTransform.rotation, containerTransform);
        }
    }
}
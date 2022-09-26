using Base;
using Data.Unit;
using Data.Unit.Enemy;
using Morpeh;
using Services;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

namespace Logic.Spawn
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class EnemySpawnSystem : UpdateSystemBase
    {
        private Filter _enemySpawnSettings;
        private Filter _enemyContainerFilter;

        private float _countdown;
        private int _enemyCount;
        private float _timer;

        public override void OnAwake()
        {
            _enemySpawnSettings = World.Filter
                .With<EnemySpawnSettings>();
            
            _enemyContainerFilter = World.Filter
                .With<EnemyContainerComponent>()
                .With<TransformComponent>();

            _countdown = _enemySpawnSettings.First().GetComponent<EnemySpawnSettings>().EnemySpawnCountdown;
        }

        public override void OnUpdate(float deltaTime)
        {
            _timer += deltaTime;

            if (_timer < _countdown)
            {
                return;
            }

            var maxEnemyCount = _enemySpawnSettings.First().GetComponent<EnemySpawnSettings>();

            if (_enemyCount >= maxEnemyCount.MaxEnemy)
            {
                return;
            }
            
            _timer = 0;

            ref var containerTransform = ref _enemyContainerFilter.First().GetComponent<TransformComponent>().transform;
            
            var enemyPrefab = GameObjectsProvider.Instance.Enemy;
            var randomPoint = GetRandomPointOnMesh(maxEnemyCount.MaxDistance);
            Object.Instantiate(enemyPrefab, randomPoint, Quaternion.identity, containerTransform);

            _enemyCount++;
        }

        private Vector3 GetRandomPointOnMesh(float maxDistance)
        {
            var randomDirection = Random.insideUnitSphere * maxDistance;
            NavMesh.SamplePosition(randomDirection, out var hit, maxDistance, 1);

            if (float.IsInfinity(hit.position.x) || 
                float.IsInfinity(hit.position.y) ||
                float.IsInfinity(hit.position.z))
            {
                return GetRandomPointOnMesh(maxDistance);
            }

            return hit.position;
        }
    }
}
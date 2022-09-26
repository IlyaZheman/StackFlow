using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using Morpeh;
using Base;
using Data.Projectiles;
using Data.Spawn;
using Data.Unit;
using Logic.Projectiles;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Logic.Spawn
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class SpawnReplenishmentPointSystem : UpdateSystemBase
    {
        private Filter _filter;
        private Filter _replenishmentPointsFilter;

        public override void OnAwake()
        {
            _filter = World.Filter
                .With<BulletsSpawnComponent>()
                .With<TransformComponent>();

            _replenishmentPointsFilter = World.Filter.With<ProjectileReplenishmentPointComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            var entity = _filter.First();
            ref var bullets = ref entity.GetComponent<BulletsSpawnComponent>();

            if (_replenishmentPointsFilter.Length >= bullets.MaxPoints)
            {
                return;
            }

            ref var transform = ref entity.GetComponent<TransformComponent>().transform;

            var point = ChooseProjectileByWeight(bullets.Weights, bullets.ReplenishmentPoints);
            var randomPoint = GetRandomPointOnMesh(bullets.MaxDistance);

            Object.Instantiate(point, randomPoint, Quaternion.identity, transform);
        }

        private ProjectileReplenishmentPoint ChooseProjectileByWeight(int[] weights,
            IReadOnlyList<ProjectileReplenishmentPoint> replenishmentPoints)
        {
            var total = weights.Sum();
            var weight = 0;
            var randomWeight = Random.Range(0, total);
            
            for (var i = 0; i < weights.Length; i++)
            {
                weight += weights[i];
                if (randomWeight <= weight)
                {
                    return replenishmentPoints[i];
                }
            }

            throw new ArgumentNullException();
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
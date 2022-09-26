using System.Collections.Generic;
using Base;
using Data.Unit;
using Data.Unit.Enemy;
using Data.Unit.MainPlayer;
using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Logic.Movement
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class RotatePlayerToNearestEnemySystem : UpdateSystemBase
    {
        private Filter _playerFilter;
        private Filter _enemiesFilter;

        public override void OnAwake()
        {
            _playerFilter = World.Filter
                .With<MainPlayerComponent>()
                .With<TransformComponent>()
                .With<UnitComponent>();
            _enemiesFilter = World.Filter
                .With<EnemyComponent>()
                .With<TransformComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            if (_playerFilter.Length == 0)
            {
                return;
            }
            
            var playerEntity = _playerFilter.First();
            ref var playerTransform = ref playerEntity.GetComponent<TransformComponent>().transform;
            ref var playerUnitComponent = ref playerEntity.GetComponent<UnitComponent>();

            var enemiesPositions = new List<Vector3>();

            for (var i = 0; i < _enemiesFilter.Length; i++)
            {
                var entity = _enemiesFilter.GetEntity(i);
                ref var entityTransform = ref entity.GetComponent<TransformComponent>().transform;
                enemiesPositions.Add(entityTransform.position);
            }

            var vector = FindClosestTarget(playerTransform.position, enemiesPositions);

            if (vector == new Vector3())
            {
                RotateForward(playerTransform, playerUnitComponent, deltaTime);
            }
            else
            {
                playerTransform.LookAt(vector);
            }
        }

        private void RotateForward(Transform playerTransform, UnitComponent playerUnitComponent, float deltaTime)
        {
            if (playerUnitComponent.MoveTargetDirection == Vector3.zero)
            {
                return;
            }

            var currentRotation = playerTransform.rotation;
            var targetRotation = Quaternion.LookRotation(playerUnitComponent.MoveTargetDirection);

            var error = Quaternion.Angle(currentRotation, targetRotation);
            playerTransform.rotation = Quaternion.RotateTowards(currentRotation, 
                targetRotation, error * playerUnitComponent.RotationLerpSpeed * deltaTime);
        }

        private Vector3 FindClosestTarget(Vector3 mainPlayerPosition, List<Vector3> enemiesPositions)
        {
            var closest = 1000f;
            var closestObject = new Vector3();

            for (var i = enemiesPositions.Count - 1; i >= 0; i--)
            {
                var dist = Vector3.Distance(enemiesPositions[i], mainPlayerPosition);
                if (dist < 10f && dist < closest)
                {
                    closest = dist;
                    closestObject = enemiesPositions[i];
                }
            }

            return closestObject;
        }
    }
}
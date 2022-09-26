using Base;
using Data.Unit;
using Data.Unit.MainPlayer;
using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Logic.Movement
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class UpdateUnitsRotationSystem : UpdateSystemBase
    {
        private Filter _filter;

        public override void OnAwake()
        {
            _filter = World.Filter
                .With<UnitComponent>()
                .With<TransformComponent>()
                .Without<MainPlayerComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var unit = ref entity.GetComponent<UnitComponent>();
                ref var transform = ref entity.GetComponent<TransformComponent>().transform;
                if (unit.MoveTargetDirection == Vector3.zero)
                {
                    continue;
                }

                var currentRotation = transform.rotation;
                var targetRotation = Quaternion.LookRotation(unit.MoveTargetDirection);

                var error = Quaternion.Angle(currentRotation, targetRotation);
                transform.rotation = Quaternion.RotateTowards(
                    currentRotation, targetRotation, error * unit.RotationLerpSpeed * deltaTime);
            }
        }
    }
}
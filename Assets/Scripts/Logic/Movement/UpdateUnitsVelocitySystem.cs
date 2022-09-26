using Base;
using Data.Movement;
using Data.Unit;
using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Logic.Movement
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class UpdateUnitsVelocitySystem : UpdateSystemBase
    {
        private const float VelocityLerpSpeed = 25f;

        private Filter _filter;

        public override void OnAwake()
        {
            _filter = World.Filter
                .With<UnitComponent>()
                .With<NavMeshAgentComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var unit = ref entity.GetComponent<UnitComponent>();
                ref var agent = ref entity.GetComponent<NavMeshAgentComponent>();

                var currentVelocity = agent.NavMeshAgent.velocity;
                var targetVelocity = unit.MoveTargetDirection.normalized * 
                                     unit.MaxSpeed * Mathf.Clamp01(unit.InputMagnitude);
                
                var newVelocity = Vector3.Lerp(currentVelocity, targetVelocity, VelocityLerpSpeed * deltaTime);
                agent.NavMeshAgent.velocity = newVelocity;
                unit.Velocity = newVelocity.magnitude;
            }
        }
    }
}
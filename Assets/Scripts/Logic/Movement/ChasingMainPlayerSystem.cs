using Base;
using Data.Movement;
using Data.Unit;
using Data.Unit.Enemy;
using Data.Unit.MainPlayer;
using Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Logic.Movement
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class ChasingMainPlayerSystem : UpdateSystemBase
    {
        private Filter _mainPlayerFilter;
        private Filter _enemiesFilter;

        public override void OnAwake()
        {
            _mainPlayerFilter = World.Filter
                .With<MainPlayerComponent>()
                .With<TransformComponent>();
            _enemiesFilter = World.Filter
                .With<EnemyComponent>()
                .With<TransformComponent>()
                .With<NavMeshAgentComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            if (_mainPlayerFilter.Length == 0)
            {
                return;
            }
            
            var mainPlayerEntity = _mainPlayerFilter.First();
            ref var playerTransform = ref mainPlayerEntity.GetComponent<TransformComponent>().transform;

            for (var i = 0; i < _enemiesFilter.Length; i++)
            {
                var enemyEntity = _enemiesFilter.GetEntity(i);
                ref var agent = ref enemyEntity.GetComponent<NavMeshMovementComponent>().NavMeshMovement;

                agent.SetDestination(playerTransform.position);
            }
        }
    }
}
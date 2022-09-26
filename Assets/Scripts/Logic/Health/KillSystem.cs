using Base;
using Data.Health;
using Data.Unit;
using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Logic.Health
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class KillSystem : LateUpdateSystemBase
    {
        private Filter _filter;

        public override void OnAwake()
        {
            _filter = World.Filter
                .With<HealthComponent>()
                .With<TransformComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            for (var i = 0; i < _filter.Length; i++)
            {
                var entity = _filter.GetEntity(i);
                ref var health = ref entity.GetComponent<HealthComponent>().Health;

                if (health <= 0)
                {
                    ref var transform = ref entity.GetComponent<TransformComponent>().transform;
                    Object.Destroy(transform.gameObject);
                    World.Default.RemoveEntity(entity);
                }
            }
        }
    }
}
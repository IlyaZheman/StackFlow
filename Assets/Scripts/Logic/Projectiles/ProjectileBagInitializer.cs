using System.Collections.Generic;
using Base;
using Data.Projectiles;
using Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Logic.Projectiles
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class ProjectileBagInitializer : InitializerBase
    {
        private Filter _filter;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<ProjectilesBagComponent>();
            ref var bag = ref _filter.First().GetComponent<ProjectilesBagComponent>().Projectiles;
            bag = new Stack<Projectile>();
        }
    }
}
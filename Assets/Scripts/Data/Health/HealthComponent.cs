using System;
using Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Data.Health
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public struct HealthComponent : IComponent
    {
        public int Health;
    }
}
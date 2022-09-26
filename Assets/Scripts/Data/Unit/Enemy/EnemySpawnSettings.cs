using System;
using Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Data.Unit.Enemy
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public struct EnemySpawnSettings : IComponent
    {
        public float EnemySpawnCountdown;
        public int MaxEnemy;
        public float MaxDistance;
    }
}
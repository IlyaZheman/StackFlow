using System;
using Logic.Projectiles;
using Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Data.Spawn
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public struct BulletsSpawnComponent : IComponent
    {
        public ProjectileReplenishmentPoint[] ReplenishmentPoints;
        public int[] Weights;
        public float Countdown;
        public int MaxPoints;
        public float MaxDistance;
    }
}
using System;
using Logic.Movement;
using Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Data.Unit
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public struct NavMeshMovementComponent : IComponent
    {
        public NavMeshMovement NavMeshMovement;
    }
}
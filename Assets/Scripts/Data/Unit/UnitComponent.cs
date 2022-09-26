using System;
using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Data.Unit
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public struct UnitComponent : IComponent
    {
        public float MaxSpeed;

        [HideInInspector] public Vector3 MoveTargetDirection;
        [HideInInspector] public float InputMagnitude;
        [HideInInspector] public float Velocity;
        public float RotationLerpSpeed => 15f;
    }
}
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
    public struct TransformComponent : IComponent
    {
        public Transform transform;
    }
}
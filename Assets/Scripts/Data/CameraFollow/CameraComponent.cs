using System;
using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Data.CameraFollow
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public struct CameraComponent : IComponent
    {
        public Camera Camera;
        public Vector3 Offset;
        public Vector3 EulerAngles;
        public float FollowSpeed;
    }
}
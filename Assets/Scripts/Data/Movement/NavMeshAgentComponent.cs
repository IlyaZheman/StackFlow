using System;
using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.AI;

namespace Data.Movement
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public struct NavMeshAgentComponent : IComponent
    {
        public NavMeshAgent NavMeshAgent;
    }
}
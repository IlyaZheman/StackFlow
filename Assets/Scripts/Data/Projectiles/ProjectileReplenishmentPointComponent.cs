﻿using System;
using Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Data.Projectiles
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public struct ProjectileReplenishmentPointComponent : IComponent
    {
        
    }
}
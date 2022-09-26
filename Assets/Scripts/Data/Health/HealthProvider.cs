using Unity.IL2CPP.CompilerServices;
using Morpeh;

namespace Data.Health
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class HealthProvider : MonoProvider<HealthComponent>
    {
        
    }
}
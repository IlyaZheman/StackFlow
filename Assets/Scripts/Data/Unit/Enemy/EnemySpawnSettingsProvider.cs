using Unity.IL2CPP.CompilerServices;
using Morpeh;

namespace Data.Unit.Enemy
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class EnemySpawnSettingsProvider : MonoProvider<EnemySpawnSettings>
    {
        
    }
}
using Unity.IL2CPP.CompilerServices;
using Morpeh;

namespace Data.CameraFollow
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class CameraProvider : MonoProvider<CameraComponent>
    {
        
    }
}
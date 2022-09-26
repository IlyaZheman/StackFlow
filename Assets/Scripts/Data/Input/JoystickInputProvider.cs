using Unity.IL2CPP.CompilerServices;
using Morpeh;

namespace Data.Input
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class JoystickInputProvider : MonoProvider<JoystickInputComponent>
    {
        
    }
}
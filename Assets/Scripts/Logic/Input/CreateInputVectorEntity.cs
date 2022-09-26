using Base;
using Data.Input;
using Morpeh;

namespace Logic.Input
{
    public class CreateInputVectorEntity : InitializerBase
    {
        public override void OnAwake()
        {
            var inputVectorEntity = World.CreateEntity();
            inputVectorEntity.AddComponent<InputVectorComponent>();
        }
    }
}
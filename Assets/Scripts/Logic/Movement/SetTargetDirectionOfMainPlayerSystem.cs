using Base;
using Data.Input;
using Data.Unit;
using Data.Unit.MainPlayer;
using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Logic.Movement
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class SetTargetDirectionOfInputTarget : UpdateSystemBase
    {
        private Filter _filter;
        private Filter _inputFilter;

        public override void OnAwake()
        {
            _filter = World.Filter
                .With<UnitComponent>()
                .With<MainPlayerComponent>();
            _inputFilter = World.Filter
                .With<InputVectorComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            ref var input = ref _inputFilter.First().GetComponent<InputVectorComponent>();
            foreach (var entity in _filter)
            {
                ref var unit = ref entity.GetComponent<UnitComponent>();
                unit.MoveTargetDirection = new Vector3(input.Direction.x, 0, input.Direction.y);
                unit.InputMagnitude = input.Direction.magnitude;
            }
        }
    }
}
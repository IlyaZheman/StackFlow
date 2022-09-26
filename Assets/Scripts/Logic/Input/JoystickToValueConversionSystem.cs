using Base;
using Data.CameraFollow;
using Data.Input;
using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Logic.Input
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class JoystickToValueConversionSystem : UpdateSystemBase
    {
        private Filter _joystickFilter;
        private Filter _inputValueFilter;
        private Filter _cameraFilter;

        public override void OnAwake()
        {
            _joystickFilter = World.Filter.With<JoystickInputComponent>();
            _inputValueFilter = World.Filter.With<InputVectorComponent>();
            _cameraFilter = World.Filter.With<CameraComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            var joystickEntity = _joystickFilter.First();
            ref var joystick = ref joystickEntity.GetComponent<JoystickInputComponent>().Joystick;

            var cameraEntity = _cameraFilter.First();
            ref var cameraComponent = ref cameraEntity.GetComponent<CameraComponent>();

            var joystickValueEntity = _inputValueFilter.First();
            ref var joystickValue = ref joystickValueEntity.GetComponent<InputVectorComponent>().Direction;

            var cameraAngle = cameraComponent.EulerAngles.y;
            var inputAngle = Vector2.SignedAngle(Vector2.right, joystick.Direction);
            
            var offset = inputAngle - cameraAngle;
            var radian = new Vector2(Mathf.Cos(offset * Mathf.Deg2Rad), Mathf.Sin(offset * Mathf.Deg2Rad));
            var direction = radian * Vector2.Distance(Vector2.zero, joystick.Direction);

            joystickValue = direction;
        }
    }
}
using Base;
using Data.CameraFollow;
using Data.Unit;
using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Logic.CameraFollow
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class CameraFollowTargetSystem : LateUpdateSystemBase
    {
        private Filter _cameraFilter;
        private Filter _cameraTargetFilter;

        public override void OnAwake()
        {
            _cameraFilter = World.Filter
                .With<CameraComponent>();
            _cameraTargetFilter = World.Filter
                .With<CameraTargetComponent>()
                .With<TransformComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            if (_cameraFilter.Length == 0 || _cameraTargetFilter.Length == 0)
            {
                return;
            }

            ref var cameraComponent = ref _cameraFilter.First().GetComponent<CameraComponent>();

            foreach (var entity in _cameraTargetFilter)
            {
                var targetPosition = entity.GetComponent<TransformComponent>().transform.position;
                var cameraTransform = cameraComponent.Camera.transform;
                cameraTransform.position = Vector3.Lerp(cameraTransform.position, 
                    targetPosition + cameraComponent.Offset, cameraComponent.FollowSpeed * deltaTime);

                cameraComponent.Camera.transform.rotation = Quaternion.Lerp(
                    cameraTransform.rotation, Quaternion.Euler(cameraComponent.EulerAngles), 
                    cameraComponent.FollowSpeed * Time.deltaTime);
            }
        }
    }
}
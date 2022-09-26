using System.Collections.Generic;
using Logic.CameraFollow;
using Logic.Health;
using Logic.Input;
using Logic.Movement;
using Logic.Projectiles;
using Logic.Spawn;
using Morpeh;
using UnityEngine;

namespace Services
{
    public class EcsInstaller : MonoBehaviour
    {
        private World _world;

        private void OnEnable()
        {
            _world = World.Default;

            var systemGroup = _world.CreateSystemsGroup();
            systemGroup.AddInitializer(new CreateInputVectorEntity());
            systemGroup.AddInitializer(new ProjectileBagInitializer());

            var systems = new List<ISystem>
            {
                // Update Systems
                new JoystickToValueConversionSystem(),
                new SetTargetDirectionOfInputTarget(),
                new UpdateUnitsVelocitySystem(),
                new UpdateUnitsRotationSystem(),
                new ChasingMainPlayerSystem(),
                new RotatePlayerToNearestEnemySystem(),
                new CreateProjectileSystem(),
                
                new SpawnReplenishmentPointSystem(),
                new EnemySpawnSystem(),
                
                // Late Update Systems
                new CameraFollowTargetSystem(),
                new KillSystem(),
            };

            foreach (var system in systems)
            {
                systemGroup.AddSystem(system);
            }

            _world.AddSystemsGroup(0, systemGroup);
        }
    }
}
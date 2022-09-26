using System.Collections.Generic;
using System.Linq;
using Data.Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Logic.Movement
{
    public class NavMeshMovement : MonoBehaviour
    {
        [SerializeField] private UnitProvider unitProvider;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float threshold = 0.15f;

        private List<Vector3> _corners;
        private bool _moving;

        public bool SetDestination(Vector3 destination)
        {
            var path = new NavMeshPath();
            if (navMeshAgent.enabled == false) {
                return false;
            }
            navMeshAgent.CalculatePath(destination, path);
            if (path.status != NavMeshPathStatus.PathComplete) {
                return false;
            }
            
            _corners = path.corners.ToList();
            _corners.RemoveAt(0);
            _moving = true;
            return true;
        }

        public void StopAgent()
        {
            _moving = false;
            _corners.Clear();
            ref var unit = ref unitProvider.GetData();
            unit.MoveTargetDirection = Vector3.zero;
            unit.InputMagnitude = 0;
        }

        private void Update()
        {
            if (!_moving) {
                return;
            }
            
            ref var unit = ref unitProvider.GetData();
            
            if (_corners.Count == 0) 
            {
                _moving = false;
                unit.MoveTargetDirection = Vector3.zero;
                unit.InputMagnitude = 0;
                return;
            }

            var distanceToNextWaypoint =
                _corners[0] - new Vector3(transform.position.x, _corners[0].y, transform.position.z);

            while (distanceToNextWaypoint.magnitude <= threshold)
            {
                _corners.RemoveAt(0);
                if (_corners.Count == 0) {
                    return;
                }
                distanceToNextWaypoint =
                    _corners[0] - new Vector3(transform.position.x, _corners[0].y, transform.position.z);
            }

            distanceToNextWaypoint.y = 0.0f;
            unit.MoveTargetDirection = distanceToNextWaypoint;
            unit.InputMagnitude = 1;
        }

        private void OnDrawGizmosSelected()
        {
            if (!_moving) return;
            foreach (var corner in _corners) {
                Gizmos.DrawSphere(corner, 0.2f);
            }
        }
    }
}
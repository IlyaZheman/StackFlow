using UnityEngine;

namespace Services
{
    public class TargetFrameRate : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}
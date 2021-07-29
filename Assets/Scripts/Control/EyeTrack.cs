using UnityEngine;
using UnityEngine.XR.MagicLeap;

namespace PeakActivity
{
    public class EyeTrack : MonoBehaviour
    {
        private Transform _eyeTransform;

        private Camera _mainCamera;

        public void Setup(GameObject eyePrefab)
        {
            _eyeTransform = Instantiate(eyePrefab).transform;
            _mainCamera = GlobalRefs.TheMainCamera;
            MLEyes.Start();
        }

        protected void OnDisable()
        {
            MLEyes.Stop();
        }

        protected void Update()
        {
            if (!MLEyes.IsStarted) return;
            var eyePos = MLEyes.FixationPoint;
            var dir = eyePos - _mainCamera.transform.position;
            if (Physics.Raycast(_mainCamera.transform.position, dir, out var hit, 100f))
            {
                _eyeTransform.position = hit.point;
            }
        }
    }
}
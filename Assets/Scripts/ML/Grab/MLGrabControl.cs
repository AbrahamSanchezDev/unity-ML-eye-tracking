using UnityEngine;

namespace PeakActivity
{
    public class MLGrabControl : MonoBehaviour
    {
        public static MLGrabObject CurObject;

        public static void EndGrab()
        {
            CurObject.OnEndGrab();
            CurObject = null;
        }

        public static void StartGrab(MLGrabObject obj)
        {
            CurObject = obj;
            CurObject.OnGrab();
        }
    }
}
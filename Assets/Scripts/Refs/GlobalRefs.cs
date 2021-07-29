using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Object = UnityEngine.Object;

namespace PeakActivity
{
    public static class GlobalRefs
    {
        public static Camera TheMainCamera;

        //Find the camera
        public static void FindCamera()
        {
            if (TheMainCamera) return;
            var cam = Camera.main;
            if (cam == null)
            {
                cam = Object.FindObjectOfType<Camera>();
            }
            if (cam == null)
            {
                Debug.Log("No camera found");
                return;
            }
            TheMainCamera = cam;
        }

        public static ARCameraManager ArCamera;

        public static string CurDateAsText()
        {
            var dir = DateTime.Now.ToString();
            dir = dir.Replace(" ", "_");
            dir = dir.Replace("/", "-");
            dir = dir.Replace(":", "-");
            return dir;
        }
    }
}
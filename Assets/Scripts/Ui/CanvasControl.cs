using System.Collections.Generic;
using UnityEngine;

namespace PeakActivity
{
    public class CanvasControl : MonoBehaviour
    {
        private static List<GameObject> _canvasObjs = new List<GameObject>();

        private static bool _show = true;

        public static void RegisterCanvasObj(GameObject obj)
        {
            _canvasObjs.Add(obj);
        }

        public static void ToggleDisplay()
        {
            _show = !_show;
            for (int i = 0; i < _canvasObjs.Count; i++)
            {
                _canvasObjs[i].SetActive(_show);
            }
        }
        protected void Awake()
        {
            _show = true;
        }
    }
}
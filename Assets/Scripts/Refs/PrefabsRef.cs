using UnityEngine;

namespace PeakActivity
{
    [CreateAssetMenu(fileName = "PrefabsRef", menuName = "Peak/PrefabsRef")]
    public class PrefabsRef : ScriptableObject
    {
        private static PrefabsRef _prefabs;

        public static PrefabsRef Prefabs
        {
            get
            {
                if (_prefabs != null)
                {
                    return _prefabs;
                }
                _prefabs = Resources.Load<PrefabsRef>("Data/PrefabsRef");
                return _prefabs;
            }
        }

        [Header("Prefabs")] public Canvas NormalCanvas;
        public Canvas MlCanvas;
        public Canvas MlNoneMovingCanvas;
        public Transform GetCanvas()
        {
            var obj = Instantiate(NormalCanvas);
            CanvasControl.RegisterCanvasObj(obj.gameObject);
            return obj.transform;
        }

        public LineRenderer LinePrefab;
        public GameObject LineEndObj;
    }
}
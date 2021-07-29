using UnityEngine;

namespace PeakActivity
{
    public class MainSetup : MonoBehaviour
    {
        public GameObject EyePointPrefab;

        //Called once
        protected void Awake()
        {
            Setup();
        }

        protected void Start()
        {
            var eyeTrack = AddComponent<EyeTrack>();
            eyeTrack.Setup(EyePointPrefab);
        }
        //Setup Refs and needed components
        private void Setup()
        {
            //var prefabs = PrefabsRef.Prefabs;
            GlobalRefs.FindCamera();


            //AddComponent<CanvasControl>();
            ////Grab Objs Control
            //AddComponent<MLGrabControl>();
            ////World Space Ui
            //var spaceUi = AddComponent<WorldSpaceUiControl>();
            //spaceUi.SetTarget(GlobalRefs.TheMainCamera.transform);
            ////Aim for ML
            //var line = AddComponent<MLAimLine>();
            //line.Init(prefabs.LinePrefab, prefabs.LineEndObj);
            ////Inputs for ML
            //var inputs = AddComponent<MLInputsMain>();
            //inputs.SetLine(line);
        }

        //Add Component to this setup Obj
        private T AddComponent<T>() where T : MonoBehaviour
        {
            return gameObject.AddComponent<T>();
        }
    }
}
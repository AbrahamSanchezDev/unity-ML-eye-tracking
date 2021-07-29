using System.Collections.Generic;
using UnityEngine;

namespace PeakActivity
{
    public class WorldSpaceUiControl : MonoBehaviour
    {
        private static WorldSpaceUiControl _instance;
        private List<WorldSpaceUiObj> _uiObjs = new List<WorldSpaceUiObj>();

        private Transform _mainTarget;

        //Init values
        private void Awake()
        {
            _instance = this;
            _mainTarget = Camera.main.transform;
        }

        //Register for the bump button
        protected void OnEnable()
        {
            MLAimLine.OnBumper.AddListener(MoveUi);
        }

        //Unregister for the bump button
        protected void OnDisable()
        {
            MLAimLine.OnBumper.RemoveListener(MoveUi);
        }

        //Global Call to add or remove ui objects
        public static void AddObj(WorldSpaceUiObj ui, bool add)
        {
            if (_instance)
            {
                if (add)
                {
                    _instance.RegisterObj(ui);
                }
                else
                {
                    _instance.UnRegisterObj(ui);
                }
            }
            else
            {
                Debug.Log("No Instance of WorldSpaceUiControl");
            }
        }

        //Add the ui object if it has the ui
        private void RegisterObj(WorldSpaceUiObj ui)
        {
            if (!_uiObjs.Contains(ui))
            {
                _uiObjs.Add(ui);
            }
        }

        //Remove the ui object if it has the ui
        private void UnRegisterObj(WorldSpaceUiObj ui)
        {
            if (_uiObjs.Contains(ui))
            {
                _uiObjs.Remove(ui);
            }
        }

        //Set the target to follow
        public void SetTarget(Transform targetTransform)
        {
            _mainTarget = targetTransform;
        }

        //Move the ui objects to the front of the camera
        private void MoveUi()
        {
            if (_mainTarget == null) return;
            for (int i = 0; i < _uiObjs.Count; i++)
            {
                _uiObjs[i].SetTarget(_mainTarget);
            }
        }
    }
}
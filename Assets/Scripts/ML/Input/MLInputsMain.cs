using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PeakActivity
{
    /// <summary>
    /// Inputs control for Magic Leap
    /// </summary>
    public class MLInputsMain : MonoBehaviour
    {
        private float _maxDistance = 50;
        private RaycastHit _hit;
        private Ray _ray;
        protected GameObject _hittedObj;
        protected Vector3 _hittedPoint;

        private MLAimLine _line;

        public static bool LeftClickOn = false;
        public static bool RightClickOn;

        public void SetLine(MLAimLine line)
        {
            _line = line;
            LeftClickOn = false;
            RightClickOn = false;
        }

        protected void OnEnable()
        {
            MLAimLine.OnTriggerPressed.AddListener(OnTriggerPressed);
        }

        protected void OnDisable()
        {
            MLAimLine.OnTriggerPressed.RemoveListener(OnTriggerPressed);
        }

        private void OnTriggerPressed()
        {
            if (_line == null) return;
            _ray.origin = _line.ControlerTransform.position;
            _ray.direction = _line.ControlerTransform.forward;
            if (Physics.Raycast(_ray, out _hit, _maxDistance))
            {
                _hittedObj = _hit.transform.gameObject;
                _hittedPoint = _hit.point;
                OnHit();
            }
        }

        private void OnHit()
        {
            if (MLGrabControl.CurObject)
            {
                MLGrabControl.EndGrab();
                return;
            }
            var grab = _hittedObj.GetComponent<MLGrabObject>();
            if (grab)
            {
                MLGrabControl.StartGrab(grab);
                return;
            }
            var interactObj = _hittedObj.GetComponent<InteractorObject>();
            if (interactObj)
            {
                interactObj.OnInteract();
                return;
            }

            var button = _hittedObj.GetComponent<Button>();
            if (button)
            {
                button.onClick.Invoke();
                return;
            }

            var toggle = _hittedObj.GetComponent<Toggle>();
            if (toggle)
            {
                toggle.onValueChanged.Invoke(!toggle.isOn);
                return;
            }
            var dropDown = _hittedObj.GetComponent<Dropdown>();
            if (dropDown)
            {
                var leftClick = new PointerEventData(EventSystem.current) {button = PointerEventData.InputButton.Left};

                dropDown.OnPointerClick(leftClick);
                return;
            }
            var events = _hittedObj.GetComponent<IPointerClickHandler>();
            if (events != null)
            {
                var leftClick = new PointerEventData(EventSystem.current) {button = PointerEventData.InputButton.Left};

                events.OnPointerClick(leftClick);
                return;
            }
        }
    }
}
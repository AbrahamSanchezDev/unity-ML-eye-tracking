using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.MagicLeap;

namespace PeakActivity
{
    [System.Serializable]
    public class OnScaleEvent : UnityEvent<float>
    {
    }

    public class MLAimLine : MonoBehaviour
    {
        public static UnityEvent OnTriggerPressed = new UnityEvent();
        public static UnityEvent OnBumper = new UnityEvent();
        public static OnScaleEvent OnScale = new OnScaleEvent();

        private LineRenderer _line;
        private GameObject _pointerGo;
#if UNITY_LUMIN
        private MLInput.Controller _controller;
#endif
        private RaycastHit _hit;
        public Transform ControlerTransform;
        public static Transform PivotTransform;
        public static Transform EndPivot;

        private bool _trigger;
        private float _maxRayDistance = 5;
        private float _movingSpeed = 3;

        private float _curDistance;


        protected void Start()
        {
            var go = new GameObject("Controller");
            go.AddComponent<DontDestroyOnLoad>();
            ControlerTransform = go.transform;
            PivotTransform = new GameObject("Position Obj").transform;
            PivotTransform.SetParent(ControlerTransform);
            SetupController();
        }

        private void SetupController()
        {
#if UNITY_LUMIN
            if (MLInput.IsStarted)
                MLInput.Stop();
            MLInput.Start();
            MLInput.OnControllerButtonDown += OnButtonDown;
            MLInput.OnControllerButtonUp += OnButtonUp;

            _controller = MLInput.GetController(MLInput.Hand.Left);
            if (_controller == null)
                _controller = MLInput.GetController(MLInput.Hand.Right);
#endif
        }

        protected void OnDestroy()
        {
            CleanupController();
        }

        private void CleanupController()
        {
#if UNITY_LUMIN
            MLInput.OnControllerButtonDown -= OnButtonDown;
            MLInput.OnControllerButtonUp -= OnButtonUp;
            MLInput.Stop();
#endif
        }

        public void Init(LineRenderer LinePrefab, GameObject LineEndObj)
        {
            if (_line != null) return;
            _line = Instantiate(LinePrefab);
            _pointerGo = Instantiate(LineEndObj);
            _line.gameObject.AddComponent<DontDestroyOnLoad>();
            _pointerGo.AddComponent<DontDestroyOnLoad>();
            EndPivot = _pointerGo.transform;
        }

        protected void Update()
        {
            if (_line == null)
            {
                Debug.Log("No Line");
                return;
            }
            UpdateTempPosition();
            SetRayPosition();

            CheckTrigger();
            MoveObj();
        }

        private void MoveObj()
        {
            UpdateToucePath();
        }

        private void UpdateTempPosition()
        {
#if UNITY_LUMIN
            ControlerTransform.position = _controller.Position;
            ControlerTransform.rotation = _controller.Orientation;
#endif
        }

        private void SetRayPosition()
        {
#if UNITY_LUMIN
            if (_controller == null) return;
            _line.transform.position = _controller.Position;
            _line.transform.rotation = _controller.Orientation;
#endif
            if (Physics.Raycast(_line.transform.position, _line.transform.forward, out _hit, _maxRayDistance))
            {
                _curDistance = Vector3.Distance(_line.transform.position, _hit.point);

                _pointerGo.SetActive(true);
                _pointerGo.transform.position = _hit.point;
                _line.useWorldSpace = true;
                _line.SetPosition(0, _line.transform.position);
                _line.SetPosition(1, _hit.point);
            }
            else
            {
                _pointerGo.SetActive(false);

                _line.useWorldSpace = false;
                _line.SetPosition(0, Vector3.zero);
                _line.SetPosition(1, Vector3.forward * _maxRayDistance);
            }
        }

        private void OnButtonDown(byte controllerId, MLInput.Controller.Button button)
        {
            if (button == MLInput.Controller.Button.Bumper)
            {
                OnBumper.Invoke();
            }
        }

        private void OnButtonUp(byte controllerId, MLInput.Controller.Button button)
        {
        }

        //Set the position of the Pivot Object
        public static void SetPivotPosition(Transform pos)
        {
            if (PivotTransform)
                PivotTransform.transform.position = pos.position;
        }

        private void CheckTrigger()
        {
#if UNITY_LUMIN
            if (_controller == null)
            {
                return;
            }
            //Is Active
            if (_controller.TriggerValue > 0.2f)
            {
                if (_trigger)
                    return;
                _trigger = true;
                OnClick();
            }
            else
            {
                _trigger = false;
            }
#endif
        }

        private void OnClick()
        {
            OnTriggerPressed.Invoke();
        }

        private void UpdateToucePath()
        {
#if UNITY_LUMIN
            if (!_controller.Touch1Active) return;
            var x = _controller.Touch1PosAndForce.x;
            var y = _controller.Touch1PosAndForce.y;
            var force = _controller.Touch1PosAndForce.z;

            if (!(force > 0)) return;
            if (x > 0.5f || x < -0.5f)
            {
                //Scale up
                OnScale.Invoke(x);
            }
            if (y > 0.4f || y < -0.4f)
            {
                //Move forwrd or back
                if (y < 0)
                {
                    PivotTransform.position = Vector3.MoveTowards(PivotTransform.position,
                        ControlerTransform.position, (-y * _movingSpeed) * Time.deltaTime);
                }
                else
                {
                    var forwardPos = ControlerTransform.position + (ControlerTransform.forward * 5);
                    PivotTransform.position = Vector3.MoveTowards(PivotTransform.position,
                        forwardPos, (y * _movingSpeed) * Time.deltaTime);
                }
            }
#endif
        }
    }
}
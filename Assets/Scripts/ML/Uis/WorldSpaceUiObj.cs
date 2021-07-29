using UnityEngine;

namespace PeakActivity
{
    public class WorldSpaceUiObj : MonoBehaviour
    {
        public float Distance = 1.5f;
        public Vector3 Offset;

        public Transform Target;
        private Transform _myTransform;

        public bool LookAtTarget;

        //Init values
        protected void Awake()
        {
            _myTransform = transform;
        }

        //Register to the control
        protected void OnEnable()
        {
            WorldSpaceUiControl.AddObj(this, true);
        }

        //Unregister to the control
        protected void OnDisable()
        {
            WorldSpaceUiControl.AddObj(this, false);
        }

        //Set the target to follow
        public void SetTarget(Transform other)
        {
            Target = other;
            MoveToWantedPos();
        }

        //Move the wanted position
        private void MoveToWantedPos()
        {
            if (Target == null) return;
            var wantedPos = Target.position + (Target.forward * Distance);
            Target.position = wantedPos;

            if (Offset != Vector3.zero)
                _myTransform.position = Target.TransformPoint(Offset);
            _myTransform.rotation = Quaternion.LookRotation(_myTransform.position - Target.position, Target.up);
            var eulers = _myTransform.eulerAngles;
            eulers.z = 0;
            _myTransform.eulerAngles = eulers;

            if (LookAtTarget)
            {
                _myTransform.transform.LookAt(Target);
            }
        }
    }
}
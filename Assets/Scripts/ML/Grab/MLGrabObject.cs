using System.Collections;
using UnityEngine;

namespace PeakActivity
{
    public abstract class MLGrabObject : MonoBehaviour
    {
        public abstract void OnGrab();

        public abstract void OnEndGrab();


        //Set the left click bool to the given value
        protected void SetLeftClickTo(bool on = true)
        {
            MLInputsMain.LeftClickOn = on;
        }

        protected void MoveTopDownObj(bool start = true)
        {
            StopCoroutine("MoveTopDownObjCo");
            if (start)
                StartCoroutine("MoveTopDownObjCo");
        }

        protected Vector3 _wantedPos;
        protected Vector3 initialPosition;
        protected Transform _myTransform;

#if UNITY_LUMIN
        protected IEnumerator MoveTopDownObjCo()
        {
            yield return null;
            while (true)
            {
                _wantedPos = MLAimLine.EndPivot.position;
                _wantedPos.y = initialPosition.y;

                OnMoveTop();
                yield return null;
            }
        }
#endif
        protected virtual void OnMoveTop()
        {
        }
    }
}
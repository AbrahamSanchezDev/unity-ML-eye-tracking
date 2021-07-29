using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeakActivity
{
    public class GrabMovingPart : MLGrabObject
    {
        public override void OnGrab()
        {
            //Debug.Log("OnGrab");
            transform.SetParent(MLAimLine.PivotTransform);
        }

        public override void OnEndGrab()
        {
            //Debug.Log("OnEndGrab");
            transform.SetParent(null);
        }
    }
}
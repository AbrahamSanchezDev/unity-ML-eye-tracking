using UnityEngine;

namespace PeakActivity
{
    /// <summary>
    /// Base class to interact with objects
    /// </summary>
    public abstract class InteractorObject : MonoBehaviour
    {
        //Called when an iteraction object is found in a gameObject
        public abstract void OnInteract();
    }
}
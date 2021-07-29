using UnityEngine;

namespace PeakActivity
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
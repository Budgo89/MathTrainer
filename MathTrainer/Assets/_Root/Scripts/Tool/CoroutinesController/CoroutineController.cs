using System.Collections;
using UnityEngine;

namespace Tool
{
    public sealed class CoroutineController : MonoBehaviour
    {
        private static CoroutineController _instance;
        
        private static CoroutineController instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("[COROUTINE MANAGER]");
                    _instance = go.AddComponent<CoroutineController>();
                    DontDestroyOnLoad(go);
                }

                return _instance;
            }
        }

        public static Coroutine StartRoutine(IEnumerator enumerator)
        {
            return instance.StartCoroutine(enumerator);
        }

        public static void StopRoutine(Coroutine coroutine)
        {
            instance.StopCoroutine(coroutine);
        }
    }
}
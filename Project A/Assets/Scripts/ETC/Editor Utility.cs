using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKProject.EditorUtils
{
    public class EditorUtility : MonoBehaviour
    {
#if UNITY_EDITOR
        public static void DebugLogScript(string str)
        {
            Debug.Log($"{str}");
        }
#endif
    }

}
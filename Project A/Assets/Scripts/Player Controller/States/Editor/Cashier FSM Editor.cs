using UnityEngine;
using UnityEditor;
using CKProject.FSM;

namespace CKProject.EditorUtils
{
#if UNITY_EDITOR
    [CustomEditor(typeof(CashierFSM))]
    public class CashierFSMEditor : Editor
    {
        private CashierFSM fsmEditor;
        private void OnEnable()
        {
            fsmEditor = (CashierFSM)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();


            if (GUILayout.Button("상태 추가", GUILayout.Width(120), GUILayout.Height(30)))
            {
                fsmEditor.AddState();
            }

            //if (GUILayout.Button("제거", GUILayout.Width(120), GUILayout.Height(30)))
            //{
            //    fsmEditor.SubState();
            //}

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

        }
    }
#endif
}
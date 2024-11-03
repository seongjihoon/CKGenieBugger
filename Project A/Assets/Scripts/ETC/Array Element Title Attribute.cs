using UnityEditor;
using UnityEngine;

namespace CKProject.AttributeEditor
{
//#if UNITY_EDITOR
    public class ArrayElementTitleAttribute : PropertyAttribute
    {
        public string Varname;
        public ArrayElementTitleAttribute()
        {
            //Varname = ElementTitleVar;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ArrayElementTitleAttribute))]
    public class ArrayElementTitleDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        protected virtual ArrayElementTitleAttribute Attribute => attribute as ArrayElementTitleAttribute;
        SerializedProperty TitleNameProp;

        public static string[] unitMoney = new string[] { "\0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X" , "Y", "Z",
        "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ" , "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY" , "AZ",
        "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ" , "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY" , "BZ",
        "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ" , "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY" , "CZ",};

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //string FullPathName = property.propertyPath + "." + Attribute.Varname;
            //TitleNameProp = property.serializedObject.FindProperty(FullPathName);
            //string newLabel = GetTitle();
            //if (string.IsNullOrEmpty(newLabel))
            //newLabel = label.text;
            string newLabel = string.Empty;
            int num = ElementNumIntParse(label);


            if (num < unitMoney.Length)
                newLabel = unitMoney[num];
            else
                newLabel = label.text;
            EditorGUI.PropertyField(position, property, new GUIContent(newLabel, label.tooltip), true);
        }

        private int ElementNumIntParse(GUIContent label)
        {
            int count = 0;
            string result = string.Empty;
            while (count < label.text.Length)
            {
                if (label.text[count] >= '0' && label.text[count] <= '9')
                {
                    result += label.text[count];
                }
                count++;
            }
            return int.Parse(result);
        }

        string GetTitle()
        {
            switch (TitleNameProp.propertyType)
            {
                case SerializedPropertyType.Generic:
                    break;
                case SerializedPropertyType.Integer:
                    return TitleNameProp.intValue.ToString();
                case SerializedPropertyType.Boolean:
                    return TitleNameProp.boolValue.ToString();
                case SerializedPropertyType.Float:
                    return TitleNameProp.floatValue.ToString();
                case SerializedPropertyType.String:
                    return TitleNameProp.stringValue;
                case SerializedPropertyType.Color:
                    return TitleNameProp.colorValue.ToString();
                case SerializedPropertyType.ObjectReference:
                    return TitleNameProp.objectReferenceValue.ToString();
                case SerializedPropertyType.LayerMask:
                    break;
                case SerializedPropertyType.Enum:
                    return TitleNameProp.enumNames[TitleNameProp.enumValueIndex];
                case SerializedPropertyType.Vector2:
                    return TitleNameProp.vector2Value.ToString();
                case SerializedPropertyType.Vector3:
                    return TitleNameProp.vector3Value.ToString();
                case SerializedPropertyType.Vector4:
                    return TitleNameProp.vector4Value.ToString();
                case SerializedPropertyType.Rect:
                    break;
                case SerializedPropertyType.ArraySize:
                    break;
                case SerializedPropertyType.Character:
                    break;
                case SerializedPropertyType.AnimationCurve:
                    break;
                case SerializedPropertyType.Bounds:
                    break;
                case SerializedPropertyType.Gradient:
                    break;
                case SerializedPropertyType.Quaternion:
                    break;
                case SerializedPropertyType.ExposedReference:
                    break;
                case SerializedPropertyType.FixedBufferSize:
                    break;
                case SerializedPropertyType.Vector2Int:
                    break;
                case SerializedPropertyType.Vector3Int:
                    break;
                case SerializedPropertyType.RectInt:
                    break;
                case SerializedPropertyType.BoundsInt:
                    break;
                default:
                    break;
            }
            return "";
        }
    }
#endif
}
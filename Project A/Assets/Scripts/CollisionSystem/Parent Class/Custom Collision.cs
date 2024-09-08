using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKProject.CustomSystem
{
    public abstract class CustomCollision : MonoBehaviour
    {
        public Vector3 Offset;

        public abstract bool OnCollision(Transform t);
    }
}

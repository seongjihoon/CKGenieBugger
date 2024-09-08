using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollision
{
    Vector3 Offset { get; set; }

    public bool OnCollision(Transform t);
}

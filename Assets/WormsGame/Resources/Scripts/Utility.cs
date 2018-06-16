using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utility

{
    public static class Utility
    {
        public static void DebugVector2(Vector2 vector)
        {
            Debug.Log("X: " + vector.x + " |Y: " + vector.y);
        }
        public static void DebugVector3(Vector3 vector)
        {
            Debug.Log("X: " + vector.x + " |Y: " + vector.y + " |Z: " + vector.z);
        }
    }
}
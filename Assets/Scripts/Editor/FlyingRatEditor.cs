using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FlyingRat))]
public class FlyingRatEditor : Editor
{
    private void OnSceneGUI()
    {
        FlyingRat rat = (FlyingRat)target;

        Color c = Color.green;
        if (rat.alert == true)
        {
            c = Color.red;
        }

        Handles.color = new Color(c.r, c.g, c.b, 0.3f);
        Handles.DrawSolidArc(
            rat.transform.position, 
            -rat.transform.forward, 
            Quaternion.AngleAxis(rat.fovAngle / 2f, rat.transform.forward) * -rat.transform.right, 
            rat.fovAngle, 
            rat.fov);

        Handles.color = c;
        rat.fov = Handles.ScaleValueHandle(
            rat.fov,
            rat.transform.position - rat.transform.right * rat.fov,
            rat.transform.rotation,
            -2,
            Handles.SphereHandleCap,
            1);
    }
}

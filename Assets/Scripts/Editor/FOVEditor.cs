using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FovEnnemy))]
public class FovEnemyEditor : Editor
{
    private void OnSceneGUI()
    {
        FovEnnemy FovE = (FovEnnemy)target;

        Color c = Color.green;
        if (FovE.alert == true)
        {
            c = Color.red;
        }

        Handles.color = new Color(c.r, c.g, c.b, 0.3f);
        Handles.DrawSolidArc(
            FovE.transform.position, 
            -FovE.transform.forward, 
            Quaternion.AngleAxis(FovE.fovAngle / 2f, FovE.transform.forward) * -FovE.transform.right, 
            FovE.fovAngle, 
            FovE.fov);

        Handles.color = c;
        FovE.fov = Handles.ScaleValueHandle(
            FovE.fov, 
            FovE.transform.position - FovE.transform.right * FovE.fov, 
            FovE.transform.rotation, 
            -2, 
            Handles.SphereHandleCap, 
            1);
    }
}

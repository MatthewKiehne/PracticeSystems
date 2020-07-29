using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Practice : MonoBehaviour {

    public Camera camera;

    private float duration = 0f;
    private Color lineColor = Color.red;

    private void Start() {

    }

    void Update() {
        HighlightTriangle();
    }


    void HighlightTriangle() {
        RaycastHit hit;
        if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit)) {
            return;
        }

        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (meshCollider == null || meshCollider.sharedMesh == null) {
            return;
        }

        Mesh mesh = meshCollider.sharedMesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        Vector3 p0 = vertices[triangles[hit.triangleIndex * 3 + 0]];
        Vector3 p1 = vertices[triangles[hit.triangleIndex * 3 + 1]];
        Vector3 p2 = vertices[triangles[hit.triangleIndex * 3 + 2]];

        Transform hitTransform = hit.collider.transform;
        p0 = hitTransform.TransformPoint(p0);
        p1 = hitTransform.TransformPoint(p1);
        p2 = hitTransform.TransformPoint(p2);

        Debug.DrawLine(p0, p1, lineColor, duration);
        Debug.DrawLine(p1, p2, lineColor, duration);
        Debug.DrawLine(p2, p0, lineColor, duration);
    }

}
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class OctahedronSphereTester : MonoBehaviour {

    public int subdivisions = 1;

    public float radius = 2f;

    private void Awake() {
        Mesh m = OctahedronSphereCreator.Create(subdivisions, radius);
        GetComponent<MeshFilter>().mesh = m;
        this.GetComponent<MeshCollider>().sharedMesh = m;
    }
}
using Unity.VisualScripting;
using UnityEngine;

public static class Extensions
{
    public static Vector3 Flatten(this Vector3 v)
    {
        return new Vector3(v.x, 0, v.z);
    }

    public static float WrapToAngle(this float f)
    {
        f = f % 360f; 
        if (f < 0f) {
            f += 360f; 
        }
        return f;
    }

    /// <summary>
    /// Provides the material at the RaycastHit point
    /// </summary>
    public static Material GetMaterial(this RaycastHit hit)
    {
        Renderer renderer = hit.collider.GetComponent<Renderer>();
        if (renderer?.sharedMaterials.Length == 1) {
            return renderer.sharedMaterials[0];
        }
        MeshCollider collider = hit.collider as MeshCollider;
        int submesh = 0;
        if (collider != null) {
            Mesh mesh = collider.sharedMesh;

            int limit = hit.triangleIndex * 3;
            for (submesh = 0; submesh < mesh.subMeshCount; submesh++) {
                int numIndices = mesh.GetTriangles(submesh).Length;
                if (numIndices > limit) {
                    break;
                }
                limit -= numIndices;
            }
            return renderer.sharedMaterials[submesh];
        }
        return null;
    }
}
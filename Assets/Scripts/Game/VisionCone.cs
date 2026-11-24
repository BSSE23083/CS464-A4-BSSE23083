using UnityEngine;

// Draws the guard's view on the floor so the player knows where not to walk.
// Rays stop at walls so the cone doesn't go through them.
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VisionCone : MonoBehaviour
{
    [SerializeField] Guard guard;
    [SerializeField] int rays = 24;
    [SerializeField] Color calm = new Color(1f, 0.6f, 0.3f, 0.18f);
    [SerializeField] Color alert = new Color(1f, 0.25f, 0.15f, 0.4f);

    Mesh mesh;
    Vector3[] verts;
    int[] tris;
    MeshRenderer rend;
    MaterialPropertyBlock block;

    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        rend = GetComponent<MeshRenderer>();
        block = new MaterialPropertyBlock();

        verts = new Vector3[rays + 2];
        tris = new int[rays * 3];
        for (int i = 0; i < rays; i++)
        {
            tris[i * 3] = 0;
            tris[i * 3 + 1] = i + 1;
            tris[i * 3 + 2] = i + 2;
        }
        SetAlert(false);
    }

    public void SetAlert(bool on)
    {
        block.SetColor("_BaseColor", on ? alert : calm);
        rend.SetPropertyBlock(block);
    }

    void LateUpdate()
    {
        float range = guard.ViewRange;
        float half = guard.ViewAngle * 0.5f;
        Vector3 origin = guard.transform.position + Vector3.up * 0.5f;

        verts[0] = Vector3.zero;
        for (int i = 0; i <= rays; i++)
        {
            float angle = Mathf.Lerp(-half, half, (float)i / rays);
            Vector3 dir = Quaternion.Euler(0, angle, 0) * Vector3.forward; // local
            Vector3 worldDir = transform.rotation * dir;

            float dist = range;
            if (Physics.Raycast(origin, worldDir, out RaycastHit hit, range, ~0, QueryTriggerInteraction.Ignore)
                && hit.transform.GetComponent<Health>() == null)
                dist = hit.distance;

            verts[i + 1] = dir * dist;
        }

        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateBounds();
    }
}

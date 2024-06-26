using UnityEngine;

public class EvacuationZone : MonoBehaviour
{
    [SerializeField] private Vector2 evacuationSize = new Vector2(4f, 4f);
    [SerializeField] private Color gizmoColor = new Color(0, 1, 0, 0.3f);

    public bool IsInEvacuationZone(Vector3 position)
    {
        Vector3 localPos = transform.InverseTransformPoint(position);
        return Mathf.Abs(localPos.x) <= evacuationSize.x / 2 &&
               Mathf.Abs(localPos.z) <= evacuationSize.y / 2;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawCube(Vector3.zero, new Vector3(evacuationSize.x, 0.1f, evacuationSize.y));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(evacuationSize.x, 0.1f, evacuationSize.y));
    }
}

using UnityEngine;

public class SeguirTransform : MonoBehaviour
{
    [SerializeField] private Transform alvo;
    [SerializeField] private Vector3 offset;

    void LateUpdate()
    {
        if (alvo == null) return;

        transform.position = alvo.position + offset;
        transform.rotation = alvo.rotation;
    }
}

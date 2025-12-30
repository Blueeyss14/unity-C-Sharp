using UnityEngine;

public class TriggerNoiseColor : MonoBehaviour
{
    public Renderer targetRenderer;
    Material mat;

    void Start()
    {
        mat = targetRenderer.material;
        mat.SetFloat("_TriggerValue", 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            mat.SetFloat("_TriggerValue", 1f);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            mat.SetFloat("_TriggerValue", 0f);
    }
}

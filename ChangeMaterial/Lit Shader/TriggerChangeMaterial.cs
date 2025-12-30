using UnityEngine;

public class TriggerChangeMaterial : MonoBehaviour
{
    public Renderer cubeRenderer;
    public Material materialOnTrigger;

    private Material originalMaterial;

    private void Start()
    {
        originalMaterial = cubeRenderer.sharedMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cubeRenderer.material = materialOnTrigger;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cubeRenderer.material = originalMaterial;
        }
    }
}

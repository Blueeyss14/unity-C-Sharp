using UnityEngine;

public class ChangeColorFromShader : MonoBehaviour
{
    public Renderer cubeRenderer;
    private Material cubeMaterial;
    private Color originalColor;

    private void Start()
    {
        cubeMaterial = cubeRenderer.material;
        originalColor = cubeMaterial.GetColor("_CubeColor");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Color target = cubeMaterial.GetColor("_TargetColor");
            cubeMaterial.SetColor("_CubeColor", target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cubeMaterial.SetColor("_CubeColor", originalColor);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class CrosshairAim : MonoBehaviour
{
    public Image crosshair;
    public Camera playerCamera;
    public float distance = 5f;
    public LayerMask itemLayer;

    private Color white = Color.white;
    private Color red = Color.red;
    public Material mat;

    PickupTrigger pickup;

    void Start()
    {
        mat.SetFloat("_TriggerValue", 0f);
        pickup = GameObject.FindGameObjectWithTag("Player").GetComponent<PickupTrigger>();
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (PlayerTrigger.playerInside)
        {
            if (Physics.Raycast(ray, distance, itemLayer))
            {
                crosshair.color = red;
                mat.SetFloat("_TriggerValue", 1f);
                pickup.PickupItem(ray, distance, itemLayer);
            }
            else
            {
                crosshair.color = white;
                mat.SetFloat("_TriggerValue", 0f);
            }
        }
        else
        {
            crosshair.color = white;
            mat.SetFloat("_TriggerValue", 0f);
        }
    }
}
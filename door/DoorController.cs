using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform door;
    public float openRotation; //90
    public float closeRotation; //0
    public float speed; //2

    bool isOpen = false;
    bool isPlayerInside = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
        }

        float targetY = isOpen ? openRotation : closeRotation;

        Quaternion targetRot = Quaternion.Euler(
            door.localEulerAngles.x,
            targetY,
            door.localEulerAngles.z
        );

        door.localRotation = Quaternion.RotateTowards(
            door.localRotation,
            targetRot,
            speed * Time.deltaTime * 100f
        );
    }
}

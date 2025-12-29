using UnityEngine;
public class RotateController : MonoBehaviour
{
    [SerializeField] public Transform door;
    [SerializeField] private float targetAngle; //(collider1=90) (collider2 = -90)
    [SerializeField] private float speed; //2

    private bool isOpen = false;
    private bool isPlayerInside = false;
    private float closedAngle = 0f;
    private static float currentTargetAngle = 0f;
    private void Start()
    {
        if (currentTargetAngle == 0f)
            currentTargetAngle = closedAngle;
    }
    private void Update()
    {
        if (!isPlayerInside) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            currentTargetAngle = isOpen ? targetAngle : closedAngle;
        }
        Quaternion targetRot = Quaternion.Euler(0, currentTargetAngle, 0);
        door.rotation = Quaternion.RotateTowards(
            door.rotation,
            targetRot,
            speed * Time.deltaTime * 100f
        );
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        isPlayerInside = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        isPlayerInside = false;
    }
}
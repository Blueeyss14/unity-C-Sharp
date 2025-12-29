using UnityEngine;

public class DoorManage : MonoBehaviour
{
    public Transform cube;
    public float targetAngle;
    public float speed;
    private bool rotate;

    private void Update()
    {
        if (!rotate) return;

        Quaternion targetRot = Quaternion.Euler(0, targetAngle, 0);
        cube.rotation = Quaternion.RotateTowards(cube.rotation, targetRot, speed * Time.deltaTime);

        if (Quaternion.Angle(cube.rotation, targetRot) < 0.1f)
            rotate = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        rotate = true;
    }
}

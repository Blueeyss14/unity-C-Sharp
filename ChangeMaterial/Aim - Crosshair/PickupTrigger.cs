using UnityEngine;

public class PickupTrigger : MonoBehaviour
{
    public Transform hand;

    public void PickupItem(Ray ray, float distance, LayerMask itemLayer)
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, itemLayer))
        {
            Pickup(hit.transform);
        }
    }

    void Pickup(Transform item)
    {
        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }

        item.SetParent(hand);
        item.localPosition = Vector3.zero;
        item.localRotation = Quaternion.identity;
    }
}

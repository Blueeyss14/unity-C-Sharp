using UnityEngine;
using System.Collections;

public class PickupSystem : MonoBehaviour
{
    public Transform handPickupPoint;
    public float pickupRange = 2f;
    public LayerMask pickupLayer;

    private GameObject heldObject = null;
    private bool isHolding = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHolding)
                TryPickup();
            else
                Drop();
        }
    }

    void TryPickup()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, pickupRange, pickupLayer);
        if (hits.Length > 0)
        {
            GameObject target = hits[0].gameObject;
            heldObject = target;

            animator.SetTrigger("pickupTrigger");

            StartCoroutine(PickupAfterDelay(0.5f));
        }
    }

    IEnumerator PickupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        heldObject.transform.SetParent(handPickupPoint);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        isHolding = true;
    }

    void Drop()
    {
        if (heldObject != null)
        {
            heldObject.transform.SetParent(null);
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;

            heldObject = null;
            isHolding = false;

            animator.SetTrigger("pickupTrigger");

        }
    }
}

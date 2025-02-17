using System.Collections;
using UnityEngine;

public class KiaraNova : MonoBehaviour
{
    CharacterController playerController;
    Animator anime;

    [Header("Grounded")]
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpValue;
    [SerializeField] private LayerMask groundLayer;

    private float waitTime = 10f;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
        anime = GetComponent<Animator>();

        StartCoroutine(WaitAnimationFor());
    }

    void Update()
    {
        bool isGround = CheckIfGrounded();

        if (isGround && jumpVelocity < 0)
        {
            jumpVelocity = 0;
        }

        jumpVelocity += gravity * Time.deltaTime;
        Vector3 jump = new Vector3(0, jumpVelocity, 0);
        playerController.Move(jump * Time.deltaTime);
    }

    bool CheckIfGrounded()
    {
        Vector3 origin = transform.position;
        float distance = 0.2f;
        return Physics.Raycast(origin, Vector3.down, distance, groundLayer);
    }


    private IEnumerator WaitAnimationFor() 
    { yield return new WaitForSeconds(waitTime);
        anime.SetTrigger("StartAnimation");
    }
}

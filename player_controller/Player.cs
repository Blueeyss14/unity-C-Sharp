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


    void Start()
    {
        playerController = GetComponent<CharacterController>();
        anime = GetComponent<Animator>();

        StartCoroutine(PlaySegmentedAnimation());
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

    private IEnumerator PlaySegmentedAnimation()
    {
        anime.Play("TREE_SCENE", 0, 0f);
        AnimatorStateInfo stateInfo = anime.GetCurrentAnimatorStateInfo(0);
        float firstScene = (1f / 176f) * stateInfo.length;
        yield return new WaitForSeconds(firstScene);
        anime.speed = 0; // Stop in 1

        yield return new WaitForSeconds(2);

        anime.Play("TREE_SCENE", 0, 2f / 176f);
        anime.speed = 1; // back to normal speed
        AnimatorStateInfo stateInfo2 = anime.GetCurrentAnimatorStateInfo(0);
        float secondScene = (17f / 176f) * stateInfo2.length;
        yield return new WaitForSeconds(secondScene);
        anime.speed = 0; // Stop in 17

        yield return new WaitForSeconds(5);

        anime.Play("TREE_SCENE", 0, 18f / 176f);
        anime.speed = 1;
    }
}

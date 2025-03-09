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
        // frame 0 to 1
        anime.Play("TREE_SCENE", 0, 0f);
        yield return null; // wajib coy
        AnimatorStateInfo stateInfo = anime.GetCurrentAnimatorStateInfo(0);
        float firstScene = (1f / 176f) * stateInfo.length;
        yield return new WaitForSeconds(firstScene);
        anime.speed = 0; // Stop di frame 1

        yield return new WaitForSeconds(2);

        // frame 2 to 16
        anime.Play("TREE_SCENE", 0, 2f / 176f);
        anime.speed = 1; // back to normal
        yield return null; // to null (wajib ini mah)
        AnimatorStateInfo stateInfo2 = anime.GetCurrentAnimatorStateInfo(0);
        float secondScene = (16f / 176f) * stateInfo2.length;
        yield return new WaitForSeconds(secondScene);
        anime.speed = 0; // Stop in 17

        yield return new WaitForSeconds(2);

        
        anime.Play("TREE_SCENE", 0, 17f / 176f);
        anime.speed = 1; 
        yield return null;
        AnimatorStateInfo stateInfo3 = anime.GetCurrentAnimatorStateInfo(0);
        float thirdScene = (50f / 176f) * stateInfo3.length;
        yield return new WaitForSeconds(thirdScene);
        anime.speed = 0; // Stop in 40

        yield return new WaitForSeconds(1);

        anime.Play("TREE_SCENE", 0, 70f / 176f);
        anime.speed = 1; // back to normal
    }


}

using System.Collections;
using System.Collections.Generic;
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
        anime = GetComponent<Animator>(); // Inisialisasi Animator
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

    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(20.1f); // Menunggu 2 detik sebelum animasi mulai

        anime.Play("TREE_SCENE", 0, 0f); // Mulai dari frame 0
        anime.speed = 1; // Pastikan animasi berjalan normal

        yield return new WaitForSeconds(1f); // Tunggu sebentar agar animasi mulai

        anime.Play("TREE_SCENE", 0, 30f / anime.GetCurrentAnimatorStateInfo(0).length); // Lompat ke frame 30

        yield return new WaitForSeconds(5f); // Menunggu 5 detik

        anime.Play("TREE_SCENE", 0, 31f / anime.GetCurrentAnimatorStateInfo(0).length);
    }

    void playAnimation()
    {
        StartCoroutine(PlayAnimation());
    }
}

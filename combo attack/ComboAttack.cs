using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    private int comboIndex = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            comboIndex++;
            if (comboIndex == 1)
            {
                animator.SetTrigger("Attack");
                animator.SetInteger("ComboIndex", comboIndex);
            } else if (comboIndex == 2)
            {
                animator.SetTrigger("Attack");
                animator.SetInteger("ComboIndex", comboIndex);
            }
            else if (comboIndex == 3)
            {
                animator.SetTrigger("Attack");
                animator.SetInteger("ComboIndex", comboIndex);
                comboIndex = 0;
            }

            //if (comboIndex > 3) {
            //    animator.Play("Attack1");
            //    animator.Play("Attack2");
            //    animator.Play("Attack3");
            //}

        }


    }
}

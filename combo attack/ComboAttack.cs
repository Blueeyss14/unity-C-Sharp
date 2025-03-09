using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    private int comboIndex;
    private bool isClicked;

    private void Start()
    {
        animator = GetComponent<Animator>();
        comboIndex = 0;
        isClicked = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AttackCombo();
        }

    }

    private void AttackCombo() {

        if (isClicked) {
            comboIndex++;
        }

        if (comboIndex == 1) {
            animator.SetInteger("ComboIndex", 1);
        }

        /*
        comboIndex++;
        if (comboIndex == 1)
        {
            animator.SetTrigger("Attack");
            animator.SetInteger("ComboIndex", comboIndex);
        }
        else if (comboIndex == 2)
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
        */
        //if (comboIndex > 3) {
        //    animator.Play("Attack1");
        //    animator.Play("Attack2");
        //    animator.Play("Attack3");
        //}
    }
/*
    public void HandleCombo() { 
        isClicked = false;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && comboIndex == 1)
        {
            animator.SetInteger("ComboIndex", 0);
            isClicked = true;
            comboIndex = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && comboIndex >= 2)
        {
            animator.SetInteger("ComboIndex", 2);
            isClicked = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && comboIndex == 2)
        {
            animator.SetInteger("ComboIndex", 0);
            isClicked = true;
            comboIndex = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && comboIndex >= 3)
        {
            animator.SetInteger("ComboIndex", 3);
            isClicked = true;
        }
        else {
            animator.SetInteger("ComboIndex", 0);
            isClicked = true;
            comboIndex = 0;
        }
    }
    */

    public void HandleCombo()
    {
        isClicked = false;

        if (comboIndex == 2)
        {
            animator.SetInteger("ComboIndex", 2);
            isClicked = true;
        }
        else if (comboIndex > 2)
        {
            animator.SetInteger("ComboIndex", 3);
            isClicked = true;
            comboIndex = 0;
        }
        else
        {
        animator.SetInteger("ComboIndex", 0);
        comboIndex = 0;
        isClicked = true;
        }
    }
}

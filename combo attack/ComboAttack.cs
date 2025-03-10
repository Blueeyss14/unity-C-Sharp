using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    private int comboIndex;
    private bool isClicked;

    void Start()
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

    void AttackCombo()
    {

        if (isClicked)
        {
            comboIndex++;
        }

        if (comboIndex == 1)
        {
            animator.SetInteger("ComboIndex", 1);
        }

    }

    public void HandleCombo()
    {
        isClicked = false;

        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Attack1") && comboIndex >= 2)
        {
            animator.SetInteger("ComboIndex", 2);
            isClicked = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(1).IsName("Attack2") && comboIndex == 2)
        {
            animator.SetInteger("ComboIndex", 0);
            isClicked = true;
            comboIndex = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(1).IsName("Attack2") && comboIndex >= 3)
        {
            animator.SetInteger("ComboIndex", 3);
            isClicked = true;
        } else if (animator.GetCurrentAnimatorStateInfo(1).IsName("Attack2") && comboIndex == 3) {
            animator.SetInteger("ComboIndex", 0);
            isClicked = true;
            comboIndex = 0;
        }
        else
        {
            animator.SetInteger("ComboIndex", 0);
            isClicked = true;
            comboIndex = 0;
        }
    }
}

using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    private int comboIndex;

    void Start()
    {
        animator = GetComponent<Animator>();
  
        comboIndex = 0;
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (comboIndex == 0) animator.SetInteger("ComboIndex", 0);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            comboIndex++;

            if (comboIndex > 20)
            {
                comboIndex = 0;
                ResetCombo();
            }
        }


        if (comboIndex == 1) {
            animator.SetInteger("ComboIndex", 1);
        }


    }


    public void Attack2()
    {
        if (comboIndex >= 4)
        {
            animator.SetInteger("ComboIndex", 2);
        }
    }
    public void Attack3()
    {
        if (comboIndex >= 10)
        {
            animator.SetInteger("ComboIndex", 3);
        }
    }

    public void ResetCombo() {
        comboIndex = 0;
        animator.SetInteger("ComboIndex", 0);
    }
}

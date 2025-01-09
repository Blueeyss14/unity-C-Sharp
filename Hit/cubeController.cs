using UnityEngine;

public class CubeController : MonoBehaviour
{
    public GameObject cube1;
    public GameObject cube2;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        cube1.transform.Translate(new Vector3(horizontal, 0, vertical) * Time.deltaTime * 5f);


        if (Vector3.Distance(cube1.transform.position, cube2.transform.position) < 1f)
        {
            DestroyCube2();
        }
    }

    void DestroyCube2()
    {
        Destroy(cube2);
    }
}

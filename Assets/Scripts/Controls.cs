using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public GameObject FinScreen_1;
    public GameObject FinScreen_2;
    public GameObject FinScreen_3;

    public static int curLevel = 0;

    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Finish")
        {
            if (curLevel == 0)
            {
                Instantiate(FinScreen_1);
            }
            else if (curLevel == 1)
            {
                Instantiate(FinScreen_2);
            }
            else if (curLevel == 2)
            {
                Instantiate(FinScreen_3);
            }

        }
    }
}

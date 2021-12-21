using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Destroy : MonoBehaviour
{

    public TMPro.TextMeshPro text;
    private int number;
    private Material material;



    private void OnCollisionEnter(Collision collision)
    {
        Vector3 tmp = (transform.position - collision.GetContact(0).point).normalized;


        if (collision.transform.tag == "Player" && (Mathf.Abs(tmp.x) <= Mathf.Abs(tmp.z) && tmp.z > 0) 
            && Mathf.Abs(collision.transform.position.x - transform.position.x) < 2f)
        {
            number--;
            text.text = $"{number}";
            if (number == 0)
            {
                Destroy(this.gameObject);

            }
            Destroy(collision.gameObject);
            material.SetColor("_BaseColor", Color.HSVToRGB(number / 20f, 1, 1));

        }

    }
    private void Start()
    {

        material = GetComponent<MeshRenderer>().material;

        number = UnityEngine.Random.Range(1, 21);
        text.text = $"{number}";
        material.SetColor("_BaseColor", Color.HSVToRGB(number / 20f,1,1));

    }

}

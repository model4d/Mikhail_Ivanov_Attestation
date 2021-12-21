using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donor : MonoBehaviour
{
    public TMPro.TextMeshPro text;
    public int number;

    public GameObject snake;
    private Snake snakeLink;


    // Start is called before the first frame update
    void Start()
    {
        snakeLink = snake.GetComponent<Snake>();
        number = UnityEngine.Random.Range(3, 11);
        text.text = $"{number}";
    }

    private void OnTriggerEnter(Collider other)
    {

        for (int i = 0; i < number; i++)
        {
            GameObject tmp = Instantiate(snakeLink.Player);
            snakeLink.snake.Add(tmp);
            tmp.GetComponent<Collider>().enabled = false;
        }
        Destroy(this.gameObject);

    }

}

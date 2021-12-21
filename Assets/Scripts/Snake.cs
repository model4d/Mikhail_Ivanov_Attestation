using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Snake : MonoBehaviour
{
    public Camera camera;
    public GameObject Player;
    public GameObject Player0;
    public List<GameObject> snake;
    private float snakeSmooth = 0.4f;

    private Vector3 _previousMousePosition;
    public int Speed;
    public int MouseSensitivity;

    private float camdist;

    public ParticleSystem popParticle;

    public AudioSource gemSound;
    public AudioSource popSound;

    private int gemNumber = 6;
    private int initLen = 5;

    public TMPro.TextMeshPro bubbleCount;

    public TextMeshProUGUI scoreText;
    private static int score;

    public GameObject GameOver;

    public GameObject finScreen_1;


    void Start()
    {
        snake = new List<GameObject>();


        snake.Add(Player0);
        for (int i = 1; i < initLen; i++)
        {
            snake.Add(Instantiate(Player));
        }
        
        bubbleCount.text = $"{initLen}";
       

        for (int i = 1; i < snake.Count; i++)
        {
            snake[i].transform.position = snake[i - 1].transform.position - new Vector3(0, 0, 1.1f);
            snake[i].GetComponent<Collider>().enabled = false;
        }

        camdist = snake[0].transform.position.z - camera.transform.position.z;

        scoreText.text = "Score: " + score;


    }

    void Update()
    {
        if ((snake[0]) == null)
        {
            snake.RemoveAt(0);
            popParticle.Play();
            popSound.Play();
            UpdateScore(1);


            if (snake.Count > 0)
            {
                snake[0].GetComponent<Collider>().enabled = true;

            }
        }
        if (snake.Count == 0)
        {
            bubbleCount.text = "";
            Destroy(this.gameObject);
            Instantiate(GameOver);
            return;
        }


        RaycastHit leftHit;
        Ray leftRay = new Ray(snake[0].transform.position, new Vector3(-1, 0, 0));

        RaycastHit rightHit;
        Ray rightRay = new Ray(snake[0].transform.position, new Vector3(1, 0, 0));



        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - _previousMousePosition;
            float m = delta.x * MouseSensitivity / 3750;
            bool flag = true;

            if (Physics.Raycast(rightRay, out rightHit))
            {
                if (rightHit.distance - m <= 0.5f)
                {
                    snake[0].transform.Translate(rightHit.distance - 0.5f, 0, 0);
                    flag = false;
                }
            }

            if (Physics.Raycast(leftRay, out leftHit))
            {
                if (leftHit.distance + m <= 0.5f)
                {
                    snake[0].transform.Translate(-leftHit.distance + 0.5f, 0, 0);
                    flag = false;
                }
            }
            if (flag)
            {
                snake[0].transform.Translate(m, 0, 0);
            }



        }

        RaycastHit frontHit;
        Ray frontRay = new Ray(snake[0].transform.position, new Vector3(0, 0, 1));

        if (Physics.Raycast(frontRay, out frontHit))
        {
            if (frontHit.distance <= 0.5f)
            {
                snake[0].transform.Translate(Time.deltaTime, 0, 0);
            }
            else
            {
                snake[0].transform.Translate(0, 0, Speed * Time.deltaTime);
            }

        }
        else
        {
            snake[0].transform.Translate(0, 0, Speed * Time.deltaTime);
        }

        _previousMousePosition = Input.mousePosition;
        
        for (int i = 1; i < snake.Count; i++)
        {
            snake[i].transform.position += new Vector3((snake[i-1].transform.position.x - snake[i].transform.position.x) * snakeSmooth,0,0);
            snake[i].transform.position = new Vector3(snake[i].transform.position.x, 0.5f, snake[i-1].transform.position.z - 1.1f);
        }

        if (snake[0].transform.position.z - camera.transform.position.z > camdist)
        {
            camera.transform.position += new Vector3(0,0, snake[0].transform.position.z - camera.transform.position.z - camdist);
        }
        popParticle.transform.position = snake[0].transform.position;

        if (gemNumber != snake.Count)
        {
            if (gemNumber < snake.Count)
            {
                gemSound.Play();
            }

            gemNumber = snake.Count;
            bubbleCount.text = $"{gemNumber}";
        }

        float tmp1 = bubbleCount.transform.position.z - snake[0].transform.position.z;
        if (tmp1 < 0.5f)
        {
            bubbleCount.transform.position -= new Vector3(0, 0, tmp1 - 0.5f);
        }
        bubbleCount.transform.position = new Vector3(snake[0].transform.position.x, 2f, bubbleCount.transform.position.z);

    }
    void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        MainManager.Instance.allScore = scoreText;

    }


}

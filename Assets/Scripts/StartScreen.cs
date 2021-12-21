using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class StartScreen : MonoBehaviour
{
    public Button button;
    public GameObject snake;

    void Start()
    {
        button.onClick.AddListener(Starter);
    }
    void Starter()
    {
        Controls.curLevel = 0;
        this.gameObject.SetActive(false);
        snake.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static int health = 3;
    public Image[] hearts;
    public Sprite fullheart;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i= 0; i < health; i++)
        {
            hearts[i].sprite = fullheart;
        }
    }
}

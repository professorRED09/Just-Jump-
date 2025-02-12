using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityScale : Subject
{
    Rigidbody2D rb;

    [SerializeField] private Slider gravityBar;
    [SerializeField] float minGrav;
    [SerializeField] float maxGrav;

    [SerializeField] private float countdownTimeFill;
    private float countdownTime;

    [SerializeField]
    private int[] numForRan = new int[] { 1, 2 };
    private bool isCountingDown = false;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCountdown(); // Start the countdown when the game starts
        UpdateGravityBar(rb.gravityScale, maxGrav);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountingDown)
        {
            countdownTime -= Time.deltaTime; // Decrement countdown time
            if (countdownTime <= 0)
            {
                isCountingDown = false; // Stop the countdown

                int luckyNum = numForRan[Random.Range(0, numForRan.Length)]; //random number to calculate for auto random rate
                int huay = luckyNum % 2;

                //randomCode will work when huay is divisible by 2
                if (huay == 0)
                {
                    rb.gravityScale = Random.Range(minGrav, maxGrav); //random gravity force
                    NotifyObserver(PlayerAction.GravityChange); //notify player with sound when the gravity's changed
                    Debug.Log("Gravity has been changed");
                    
                }
                UpdateGravityBar(rb.gravityScale, maxGrav);
                StartCountdown();
            }
        }
    }

    // Start the countdown with a random duration
    void StartCountdown()
    {
        isCountingDown = true;
        countdownTime = countdownTimeFill;
    }

    public void UpdateGravityBar(float currentValue, float maxValue)
    {
        gravityBar.value = currentValue / maxValue;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BreakablePlatform : MonoBehaviour
{
    public Slider lifeBar;
    public GameObject bar;
    [SerializeField] private float life;
    [SerializeField] private float maxLifeSpan;
    [SerializeField] GameObject platform;
    [SerializeField] bool isTouched;

    private Collider2D platformCollider;
    private Renderer platformRenderer;

    private void Awake()
    {
        //lifeSpan = defaultLifeSpan;

    }

    private void Start()
    {
        platformCollider = GetComponent<Collider2D>();
        platformRenderer = GetComponent<Renderer>();

        life = maxLifeSpan;

    }

    private void Update()
    {
        // if player has touched it, start counting down its lifetime
        if (isTouched)
        {
            life -= Time.deltaTime;
            UpdateBar(life, maxLifeSpan);

            // when its life reach 0, then hide the platform for a time
            if(life <= 0)
            {
                StartCoroutine(HidePlatform(2));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isTouched = true;
            bar.SetActive(true);
        }

    }

    // hide the platform for a time and show it again with all value reset
    private IEnumerator HidePlatform(float waitTime)
    {
        // hide its life bar
        bar.SetActive(false);

        // disable the collider and its sprite while hiding
        platformCollider.enabled = false;
        platformRenderer.enabled = false;

        yield return new WaitForSeconds(waitTime);

        // enable the collider and its sprite again
        platformCollider.enabled = true;
        platformRenderer.enabled = true;

        // reset all value
        isTouched = false;        
        life = maxLifeSpan;

        // show its life bar again
        bar.SetActive(false);
    }

    public void UpdateBar(float currentValue, float maxValue)
    {
        lifeBar.value = currentValue / maxValue;
    }
}

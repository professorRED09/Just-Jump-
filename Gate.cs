using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Subject
{
    public GameManager gameManager;
    private BoxCollider2D col;
    public GameObject lightning;    

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();        
        col.enabled = false;
        lightning.SetActive(false);
    }

    public void ActivateTheGate()
    {
        col.enabled = true;
        lightning.SetActive(true);
        NotifyObserver(PlayerAction.Activate);
        print("The gate has been activated!");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameManager.Victory();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public LayerMask interactLayers;
    public Collider2D[] hits;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleInteraction();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void HandleInteraction()
    {
        print("E Detect");
        //hit[] = PhysicsScene2D.OverlapCollider(hits,)
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, interactLayers);
        if (hit == null)
        {
            print("DETECT NOTHING");
            return;
        }

        hit.GetComponent<EventTrigger>().OpenGate();       
        
    }
    
}

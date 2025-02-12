using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBG : MonoBehaviour
{
    public float maxY;
    public float speed;
    public float currentY;

    // Update is called once per frame
    void Update()
    {
        currentY += speed * Time.deltaTime;
        this.gameObject.transform.position = new Vector3(transform.position.x, currentY, 0);

        if (currentY > maxY)
        {
            this.gameObject.transform.position = new Vector3(transform.position.x, 0, 0);
            currentY = 0;
        }
    }
}

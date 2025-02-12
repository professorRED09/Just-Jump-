using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public Transform platform;
    public Transform leftPoint;
    public Transform rightPoint;

    [SerializeField] int direction = 1;
    [SerializeField] private float speed;    

    // Update is called once per frame
    void Update()
    {
        // assign target for the platform to move to
        Vector2 target = currentMovementTarget();

        // move the platform to target position with given speed
        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);

        // assign distance = distance between target(waypoint) and platform 
        float distance = (target - (Vector2)platform.position).magnitude;

        // if the distance is less than or equal to 0.1, then change it direction to opposite
        if(distance <= 0.1f)
        {
            direction *= -1;
        }
    }

    // make players move along with the platform when it's moving
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            collision.transform.SetParent(transform);
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.transform.SetParent(null);
        }
            
    }

    // this function will give a target position for the platform based on its current direction
    Vector2 currentMovementTarget()
    {
        if(direction == 1)
        {
            return leftPoint.position;
        }
        else
        {
            return rightPoint.position;
        }
    }

    private void OnDrawGizmos()
    {
        if(platform != null && leftPoint != null && rightPoint != null)
        {
            Gizmos.DrawLine(platform.position, leftPoint.position);
            Gizmos.DrawLine(platform.position, rightPoint.position);
        }
    }
}

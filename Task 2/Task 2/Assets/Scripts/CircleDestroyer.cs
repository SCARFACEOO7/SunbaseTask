using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDestroyer : MonoBehaviour
{
    [SerializeField] private List<CircleCollider2D> circles = new List<CircleCollider2D>();
    

    private void OnTriggerEnter2D(Collider2D other) 
    {
        CircleCollider2D circleCollider2D = 
        other.GetComponent<CircleCollider2D>();

        if(circleCollider2D != null && !circles.Contains(circleCollider2D))
        {
            circles.Add(circleCollider2D);
        }
    }

    private void OnDestroy() 
    {
        foreach(CircleCollider2D circleCollider2D in circles)
        {
            Destroy(circleCollider2D.gameObject);
        }
    }
}

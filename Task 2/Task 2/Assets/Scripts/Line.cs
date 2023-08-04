using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D lineCollider;

    private readonly List<Vector2> points = new List<Vector2>();

    private void Start() 
    {
        lineCollider.transform.position -= transform.position;
    }

    public void SetPosition(Vector2 newPos)
    {
        if(!CanSet(newPos))
        {
            return;
        }

        points.Add(newPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, newPos);

        lineCollider.points = points.ToArray();
    }

    private bool CanSet(Vector2 newPos)
    {
        if(lineRenderer.positionCount == 0)
        {
            return true;
        }

        return Vector2.Distance
        (lineRenderer.GetPosition(lineRenderer.positionCount-1), 
        newPos) > DrawLine.minDistance;
    }

    
}

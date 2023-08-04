using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    #region variables
    
    [SerializeField] GameObject circlePrefab;
    [SerializeField] private int numberOfCirclesToSpawn;
    Camera mainCam;

    [SerializeField]private List<GameObject> circles = new List<GameObject>();

    #endregion

    #region unity methods
    void Start()
    {
        mainCam = Camera.main;
        SpawnCircles();
    }

    #endregion
    #region public methods

    public void SpawnCircles()
    {
        DestroyPreviousCircles();
        for(int i =0; i<numberOfCirclesToSpawn; i++)
        {
            Vector3 viewportValue = new Vector3(Random.value, Random.value, 0f);
            if(viewportValue.x > 0.95f || viewportValue.x < 0.05f || 
            viewportValue.y > 0.80f || viewportValue.y < 0.05f)
            {
                i--;
                continue;
            }

            Vector3 worldPosition = mainCam.ViewportToWorldPoint(viewportValue);
            worldPosition.z = 0.1f;
            
            bool isOverlapping = CheckOverlap(worldPosition);

            if(!isOverlapping)
            {
                GameObject newCircle = Instantiate(circlePrefab, worldPosition, Quaternion.identity);
                circles.Add(newCircle);
            }
            else
            {
                i--;
            }

            
        }

    }

    private bool CheckOverlap(Vector3 worldPosition)
    {
        foreach(GameObject circle in circles)
        {
            float distance = Vector3.Distance(circle.transform.position, worldPosition);
            if(distance < 1.5f)
            {
                return true;
            }
        }

        return false;
    }

    private void DestroyPreviousCircles()
    {
        if(circles.Count == 0)
        {
            return;
        }
        else
        {
            foreach(GameObject circle in circles)
            {
                Destroy(circle);
            }
            circles.Clear();
        }
    }

#endregion
}

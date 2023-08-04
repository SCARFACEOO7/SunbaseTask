using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
#region variable
    private Camera mainCam;
    [SerializeField] private Line linePrefab;
    private Line currentLine;

    [SerializeField] private List<CircleCollider2D> circles = new List<CircleCollider2D>();

    public const float minDistance = 0.1f;

#endregion

#region unity methods
    void Start()
    {
        mainCam = Camera.main;
    }

    private void Update() 
    {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
        {
            currentLine = Instantiate(linePrefab, mousePos, Quaternion.identity);
        }

        if(Input.GetMouseButton(0))
        {
            currentLine.SetPosition(mousePos);
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(currentLine != null)
            {
                Destroy(currentLine.gameObject);
            }
        }
    }

#endregion

#region private methods


#endregion
}


using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject paintingArea;

    List<Vector2> points;

    /*void Update()
    {
        foreach (Vector2 dessiné in points)
        {
            
        }
    } */

    public void UpdateLine(Vector2 position)
    {
        if (points == null)
        {
            points = new List<Vector2>();
            SetPoint(position);
            return;
        }

        if (Vector2.Distance(points.Last(), position) > 0.1f)
        {
            SetPoint(position);
        }
    }

    void SetPoint(Vector2 point)
    {
        points.Add(point);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);
    }
}

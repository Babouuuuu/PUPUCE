using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using System.Collections;

public class LineGenerator : MonoBehaviour
{
    public GameObject linePrefab;
    public Image image;
    public PaintingArea paintingArea;
    public bool mouseIsPressed;

    void Start()
    {
        mouseIsPressed = false;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            Vector3 objectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject newLine = Instantiate(linePrefab, objectPosition, Quaternion.identity);
            newLine.transform.SetParent(image.transform);
            mouseIsPressed = true;
            Debug.Log("+ line");
        }

        if(Input.GetMouseButtonUp(0))
        {
            mouseIsPressed = false;
        }

        if(mouseIsPressed == true)
        {
            Vector3 objectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject newLine = Instantiate(linePrefab, objectPosition, Quaternion.identity);
            newLine.transform.SetParent(image.transform);
        }
    }

}

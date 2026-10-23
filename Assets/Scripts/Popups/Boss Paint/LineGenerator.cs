using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class LineGenerator : MonoBehaviour
{
    public GameObject linePrefab;
    public Image image;
    public PaintingArea paintingArea;

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && paintingArea.paintingMode == true) 
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 objectPosition = Camera.current.ScreenToWorldPoint(mousePosition);
            Instantiate(linePrefab, objectPosition, Quaternion.identity);
            linePrefab.transform.SetParent(image.transform);
        }
    }

}

using UnityEngine;
using UnityEngine.UI;

public class MasterpieceArt : MonoBehaviour
{
    public Canvas canvas;
    public Image image;
    public Image paintingArea; 
    private bool hasInstantiated = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas.sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (paintingArea == null)
        {
            Debug.Log("Le champ 'paintingArea' n'est pas assigné dans l'inspecteur.");
            paintingArea = FindAnyObjectByType<PaintingArea>().paintingArea;
            return;
        }
        else if (!hasInstantiated && paintingArea != null)
        {
            Image artPaint = Instantiate(paintingArea, image.transform.position, Quaternion.identity);
            artPaint.transform.SetParent(image.transform);
            hasInstantiated = true;
        }
    }
}

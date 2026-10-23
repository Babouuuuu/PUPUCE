using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class PaintingArea : MonoBehaviour
{
    public bool paintingMode = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("paint");
        paintingMode = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("no paint");
        paintingMode = false;
    }
}

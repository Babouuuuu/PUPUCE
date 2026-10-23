using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class PaintingArea : MonoBehaviour
{
    public GameObject mouse;
    public bool paintingMode = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("no paint");
        paintingMode = true;
    }

    void OnCoisionExit2D(Collision2D collision)
    {
        Debug.Log("no paint");
        paintingMode = false;
    }
}

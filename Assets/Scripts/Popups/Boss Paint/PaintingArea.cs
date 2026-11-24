using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PaintingArea : MonoBehaviour
{
    public bool paintingMode = false;
    public BoxCollider2D boxCollider;
    public Image paintingArea;
    MasterpieceArt masterpieceArt;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            paintingMode = true;
            Debug.Log("Le joueur est entré dans la zone de peinture.");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            paintingMode = false;
            Debug.Log("Le joueur est sorti de la zone de peinture.");
        }
    }

    void Update()
    {
        masterpieceArt = GetComponentInParent<MasterpieceArt>();

        if (masterpieceArt == null)
        {
            return;
        }
        else if (masterpieceArt != null)
        {
            Destroy(this);
        }
    }
}

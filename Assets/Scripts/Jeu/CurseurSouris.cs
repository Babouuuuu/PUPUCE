using UnityEngine;
using UnityEngine.InputSystem;

// Remplace le curseur de Windows par la notre
public class CurseurSouris : MonoBehaviour
{
    RectTransform rectTransform;
    Canvas canvas;

    void Start()
    {
        rectTransform = (RectTransform)transform;
        canvas = GetComponentInParent<Canvas>();
        Cursor.visible = false; // on cache le vrai curseur
    }

    void Update()
    {
        if (Mouse.current == null) return; // pas de souris branchée

        // convertit la position de la souris (en pixels de l'écran) en position dans l'interface
        Vector2 positionEcran = Mouse.current.position.ReadValue();
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, positionEcran,
                                                                canvas.worldCamera, out Vector2 position);
        rectTransform.anchoredPosition = position;
    }

    void OnDestroy()
    {
        Cursor.visible = true; // on rend le vrai curseur en quittant la scène
    }
}

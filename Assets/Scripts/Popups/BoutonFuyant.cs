using UnityEngine;
using UnityEngine.EventSystems;

// un bouton farceur qui s'enfuit quand la souris passe dessus (un certain nombre de fois, ensuite il se laisse faire)
// IPointerEnterHandler permet que Unity appelle OnPointerEnter quand la souris entre sur l'objet
public class BoutonFuyant : MonoBehaviour, IPointerEnterHandler
{
    public int nombreDeFuites = 3;
    [Tooltip("La zone dans laquelle le bouton peut se déplacer (son parent)")]
    public RectTransform zone;

    int fuitesRestantes;

    void Start()
    {
        fuitesRestantes = nombreDeFuites;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (fuitesRestantes <= 0) return;
        fuitesRestantes--;

        // nouvelle position au hasard dans la zone (le bouton est ancré au centre de la zone)
        RectTransform bouton = (RectTransform)transform;
        float maxX = (zone.rect.width - bouton.rect.width) / 2f;
        float maxY = (zone.rect.height - bouton.rect.height) / 2f;
        bouton.anchoredPosition = new Vector2(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY));
    }
}

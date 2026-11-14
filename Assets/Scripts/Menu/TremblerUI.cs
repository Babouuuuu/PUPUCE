using UnityEngine;

// juste le tremblement de la fenêtre, extrait de Popup (pour les éléments d'UI qui n'ont pas besoin du reste)
public class TremblerUI : MonoBehaviour
{
    [Header("Visuels")]
    public RectTransform fenetre; // la fenêtre qui tremble (ses enfants tremblent avec)

    [Header("Paramètres")]
    public float duree = 0.3f;      // durée du tremblement
    public float intensite = 4f;    // amplitude en pixels

    float finTremblement;
    Vector2 positionDeBase;

    void Start()
    {
        // on mémorise la position de départ pour y revenir après le tremblement
        positionDeBase = fenetre.anchoredPosition;
    }

    void Update()
    {
        // tremblement autour de la position de base (on ne touche pas à l'objet racine)
        if (Time.unscaledTime < finTremblement) fenetre.anchoredPosition = positionDeBase + Random.insideUnitCircle * intensite;
        else fenetre.anchoredPosition = positionDeBase;
    }

    // branchée sur le bouton (OnClick) dans l'Inspector
    public void Trembler()
    {
        finTremblement = Time.unscaledTime + duree;
    }
}
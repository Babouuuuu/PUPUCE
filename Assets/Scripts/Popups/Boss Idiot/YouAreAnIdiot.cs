using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class YouAreAnIdiot : MonoBehaviour
{
    public float vitesse = 3f;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Vector2 direction;

    public int interactionsNecessaires = 1; // pour plus tard les popup avec plusieurs interactions
    public int xp = 1;

    [Header("Visuels")]
    public RectTransform fenetre;
    [Tooltip("Le fond de la popup -> elle s'assombrit quand Clippy le petit filou bloque la popup")]
    public Image fond;
    public Text titre;

    public bool cibleeParUneSouris;
    [HideInInspector] public bool cibleeParClippy;
    [HideInInspector] public bool bloquee;

    int interactionsRestantes;
    string titreDeBase;
    Color couleurDuFond;
    float finBlocage;
    float finTremblement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        
        // trouve une direction au hasard (en diagonal)
        // btw le ? c comme un if/else
        float x = Random.value < 0.5f ? -1f : 1f;
        float y = Random.value < 0.5f ? -1f : 1f;
        direction = new Vector2(x, y).normalized;

        interactionsRestantes = interactionsNecessaires;
        titreDeBase = titre.text;
        couleurDuFond = fond.color;
        MettreAJourTitre();
    }

    void Update()
    {
        // on calcul les bord max
        // comme la camera est fixé au joueur il faut en tenir compte
        Camera cam = Camera.main;
        float limiteX = cam.orthographicSize * cam.aspect - sprite.bounds.extents.x;
        float limiteY = cam.orthographicSize - sprite.bounds.extents.y;
        Vector2 position = transform.position - cam.transform.position;

        // Rebond : si on dépasse un bord en allant vers lui, on repart dans l'autre sens
        if ((position.x > limiteX && direction.x > 0f) || (position.x < -limiteX && direction.x < 0f))
        {
            direction.x = -direction.x;
        }
        if ((position.y > limiteY && direction.y > 0f) || (position.y < -limiteY && direction.y < 0f))
        {
            direction.y = -direction.y;
        }

        rb.linearVelocity = direction * vitesse;

        if (bloquee && Time.time >= finBlocage) Debloquer(); // on update le statut de blocage

        // tremblement de la fenêtre (on bouge seulement l'image, pas la popup)
        if (Time.unscaledTime < finTremblement) fenetre.anchoredPosition = Random.insideUnitCircle * 4f;
        else fenetre.anchoredPosition = Vector2.zero;

    }

    // pour les click souris (en gros quand on veut une action style cocher une case plutot que tout faire)
    public void Action()
    {
        this.Interaction();
        // on va ajouter ici pour slider manuellement ou cocher les cases
    }

    // branchée sur les boutons (OnClick) dans l'Inspector et appelée par les scripts d'interaction, les souris et le logo DVD
    public void Interaction()
    {
        if (interactionsRestantes <= 0) return; // déjà fermée -> précaution parce que on sais jamais
    
        interactionsRestantes--;
        if (interactionsRestantes <= 0)
        {
            GameManager.instance.PopupFermee(xp); //on envoi l'xp pour la progression
            Destroy(gameObject); // et on supprime la popup
        }
        else
        {
            MettreAJourTitre();
        }
    }

    // mauvaise action (faux bouton, cases encore cochées ou dautres truc quon verra plus tard) -> la fenêtre tremble
    public void Trembler()
    {
        finTremblement = Time.unscaledTime + 0.3f; // comme le clignotement et le reste on fonctionne en définissant le temps de fin de leffet pour mieux supporter les pauses et eviter la casse
    }

    // appelé par Clippy -> Une popup "Static" ne bouge plus et bloque les autres comme un mur
    public void Bloquer(float duree)
    {
        bloquee = true;
        finBlocage = Time.time + duree;
        rb.bodyType = RigidbodyType2D.Static;
        fond.color = Color.gray; // fenêtre assombrie, comme une fenêtre inactive
    }

    // appelé pour mettre fin au bloquage 
    void Debloquer()
    {
        bloquee = false;
        cibleeParClippy = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        fond.color = couleurDuFond;
    }

    // fonction pour supprimer la popup
    public void Detruire()
    {
        interactionsRestantes = 0;
        Destroy(gameObject);
    }

    // bon je vais pas expliquer celle la 
    void MettreAJourTitre()
    {
        // pour les popups a plusieurs interactions -> on affiche combien il en reste ex: "WinRAR (3)"
        if (interactionsNecessaires > 1) titre.text = titreDeBase + " (" + interactionsRestantes + ")";
    }
}

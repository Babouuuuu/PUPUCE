using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ba c les popup quoi... jsp quoi dire de plus en gros c la classe mère, 
// toutes les popup se base dessus donc c ici quon va coder tout les comportements commun a toutes les popup
public class Popup : MonoBehaviour
{
    // la liste de toutes les popups actuellement en vie (utilisée par les souris et Clippy pour trouver une cible)
    public static List<Popup> list = new List<Popup>(); //on défini donc une list qui ne contiendra que des objets popup et qui est unique au jeu

    [Header("Paramètres")]
    public float vitesse = 0.8f;
    public int interactionsNecessaires = 1; // pour plus tard les popup avec plusieurs interactions
    public int xp = 1; // la progression quelle donne

    [Header("Visuels")]
    public RectTransform fenetre;
    [Tooltip("Le fond de la popup -> elle s'assombrit quand Clippy le petit filou bloque la popup")]
    public Image fond;
    public Text titre;

    // utilisés par les souris et Clippy pour ne pas viser tous la même popup (en gros on les definie a True quand deja cibler par le type)
    [HideInInspector] public bool cibleeParUneSouris;
    [HideInInspector] public bool cibleeParClippy;
    [HideInInspector] public bool bloquee;

    int interactionsRestantes;
    string titreDeBase;
    Color couleurDuFond;
    Rigidbody2D rb;
    float finBlocage;
    float finTremblement;

    // OnEnable / OnDisable -> appelés quand l'objet apparaît / disparaît pour les ajouter ou les virer de la list
    void OnEnable() { list.Add(this); }
    void OnDisable() { list.Remove(this); }

    void Start()
    {
        // au start on initialise les variables des components et parametres
        rb = GetComponent<Rigidbody2D>();
        interactionsRestantes = interactionsNecessaires;
        titreDeBase = titre.text;
        couleurDuFond = fond.color;
        MettreAJourTitre();
    }

    void FixedUpdate()
    {
        if (bloquee) return; // si la popup a le statut bloquee alors elle skip le deplacement

        // avance vers le joueur
        Vector2 versLeJoueur = GameManager.instance.joueur.transform.position - transform.position;
        rb.linearVelocity = versLeJoueur.normalized * vitesse;
    }

    void Update()
    {
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

    // pour faire spawn une autre popup a partir de celle ci (pour des ennemis qui sont chiant ou jsp quoi)
    public void Reproduction()
    {
        Trembler();
        SpawnerPopups.instance.FaireApparaitrePres(transform.position);
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

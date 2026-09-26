using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Le chef d'orchestre de la partie, le délégué de la classe, que dis-je le Thomas Desaunay du GD
// Il compte les vies, l'XP et le temps, ouvre les écrans d'upgrade, de défaite et de victoire
// Il est l'alpha et l'oméga le premier et le dernier le commencement et la fin 
// Amen
public class GameManager : MonoBehaviour
{
    public static GameManager instance; // on la declare en static pour que tout les scipt puisse le récuperer facilement, il est UNIQUE
    // en gros public -> access ouvert, static -> ne dépend pas de la classe (singleton donc unique), GameManager -> on donne la ref de la classe, instance -> le nom de la variable

    [Header("Monde")]
    [Tooltip("Largeur et hauteur du monde (en unités). La caméra en voit environ 19 x 11.")]
    public Vector2 tailleDuMonde = new Vector2(50f, 30f);

    [Header("Vies")]
    public int viesMax = 4;

    [Header("Progression (la barre dans la barre des tâches)")]
    [Tooltip("XP à gagner pour remplir la barre et finir le niveau")]
    public int xpPourFinir = 150;
    [Tooltip("Paliers d'upgrade : 0 = barre vide, 1 = barre pleine. Dans l'ordre croissant !")]
    public float[] paliers = { 0.07f, 0.19f, 0.32f, 0.46f, 0.63f, 0.81f }; // les 6 repères du Figma

    [Header("Objets de la scène")]
    public Joueur joueur;
    public EcranUpgrade ecranUpgrade;
    public GameObject ecranGameOver;
    public Text texteGameOver;
    public GameObject ecranVictoire;

    // ces variables sont lues par les autres scripts (HUD...) mais n'ont pas besoin d'apparaître dans l'Inspector
    [HideInInspector] public int vies;
    [HideInInspector] public int xp;
    [HideInInspector] public float tempsEcoule;
    [HideInInspector] public int popupsFermees;
    [HideInInspector] public bool partieFinie;

    int prochainPalier = 0;     // numéro du prochain palier à atteindre
    int upgradesEnAttente = 0;  // paliers franchis dont on n'a pas encore choisi l'upgrade

    void Awake()
    {
        instance = this;
        vies = viesMax;
        Time.timeScale = 1f; // au cas où on relance la partie depuis un écran en pause
    }

    void Update()
    {
        // Time.deltaTime vaut 0 quand le jeu est en pause (Time.timeScale = 0) -> le chrono s'arrête tout seul
        tempsEcoule += Time.deltaTime;
    }

    // remplissage de la barre entre 0 et 1
    public float Progression()
    {
        return Mathf.Clamp01((float)xp / xpPourFinir);
    }

    // appelé par une popup quand elle est fermée
    public void PopupFermee(int xpGagnee)
    {
        if (partieFinie) return;

        popupsFermees++;
        xp += xpGagnee;

        // A-t-on franchi un (ou plusieurs) paliers ?
        while (prochainPalier < paliers.Length && Progression() >= paliers[prochainPalier])
        {
            prochainPalier++;
            upgradesEnAttente++;
        }

        if (Progression() >= 1f) Victoire();
        else OuvrirUpgradeSiBesoin();
    }

    void OuvrirUpgradeSiBesoin()
    {
        if (upgradesEnAttente > 0 && !ecranUpgrade.gameObject.activeSelf)
        {
            upgradesEnAttente--;
            Time.timeScale = 0f; // met le jeu en pause
            ecranUpgrade.Ouvrir();
        }
    }

    // appelé par l'écran d'upgrade une fois le choix fait
    public void UpgradeChoisie()
    {
        Time.timeScale = 1f;
        OuvrirUpgradeSiBesoin(); // s'il reste un palier en attente, on rouvre l'écran
    }

    // appelé par le joueur quand une popup le touche
    public void PerdreUneVie()
    {
        if (partieFinie) return;

        vies--;
        if (vies <= 0) GameOver();
    }

    // ta perdu gros noob
    void GameOver()
    {
        partieFinie = true;
        Time.timeScale = 0f;
        texteGameOver.text = "*  Temps de survie : " + TempsFormate() + "\n*  Popups fermées : " + popupsFermees;
        ecranGameOver.SetActive(true);
    }

    // gg mon bg de gd
    void Victoire()
    {
        partieFinie = true;
        Time.timeScale = 0f;
        ecranVictoire.SetActive(true);
    }

    // branché sur les boutons "Redémarrer" et "Rejouer" : recharge la scène
    public void Rejouer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Le temps écoulé au format hh:mm
    public string TempsFormate()
    {
        int secondes = (int)tempsEcoule;
        return (secondes / 60).ToString("00") + ":" + (secondes % 60).ToString("00");
    }
}

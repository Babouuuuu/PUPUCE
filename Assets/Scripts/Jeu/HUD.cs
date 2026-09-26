using UnityEngine;
using UnityEngine.UI;

// affiche les infos de la partie dans la barre des tâches (les coeurs, la barre de progression et l'horloge)
public class HUD : MonoBehaviour
{
    [Header("Vies")]
    public Image[] coeurs;
    public Color couleurCoeurPlein = Color.white; // blanc = l'image garde ses couleurs
    public Color couleurCoeurPerdu = new Color(0.25f, 0.25f, 0.25f, 0.6f);

    [Header("Progression")]
    public RectTransform remplissage;
    public Image modeleRepere;
    public Sprite repereAtteint;
    public Sprite repereAVenir;

    [Header("Horloge")]
    public Text texteHorloge;

    Image[] reperes;

    void Start()
    {
        // on place un repère sur la barre pour chaque palier d'upgrade du GameManager
        float[] paliers = GameManager.instance.paliers;
        reperes = new Image[paliers.Length];
        for (int i = 0; i < paliers.Length; i++)
        {
            Image repere = Instantiate(modeleRepere, modeleRepere.transform.parent);
            repere.rectTransform.anchorMin = new Vector2(paliers[i], 0.5f); // 0 = début de la barre, 1 = fin
            repere.rectTransform.anchorMax = new Vector2(paliers[i], 0.5f);
            repere.gameObject.SetActive(true);
            reperes[i] = repere;
        }
    }

    void Update()
    {
        GameManager jeu = GameManager.instance;

        for (int i = 0; i < coeurs.Length; i++)
        {
            coeurs[i].color = i < jeu.vies ? couleurCoeurPlein : couleurCoeurPerdu;
        }

        // la barre verte s'arrête à l'endroit de la progression (anchorMax.x entre 0 et 1)
        float progression = jeu.Progression();
        remplissage.gameObject.SetActive(progression > 0f);
        remplissage.anchorMax = new Vector2(progression, 1f);

        // les repères deviennent verts une fois le palier atteint
        for (int i = 0; i < reperes.Length; i++)
        {
            reperes[i].sprite = progression >= jeu.paliers[i] ? repereAtteint : repereAVenir;
        }

        texteHorloge.text = jeu.TempsFormate();
    }
}

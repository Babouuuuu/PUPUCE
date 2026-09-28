using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

// gère l'apparition des popups, leurs fréquences type etc
public class SpawnerPopups : MonoBehaviour
{
    public static SpawnerPopups instance;

    // [System.Serializable] permet de voir et remplir cette petite classe dans l'Inspector
    [System.Serializable]
    public class TypeDePopup
    {
        public GameObject prefab;
        [Tooltip("La popup peut apparaître à partir de ce moment (en secondes)")]
        public float apparaitApres = 0f;
    }

    [System.Serializable]
    public class TypeBoss
    {
        public GameObject prefab;
        [Tooltip("Le boss spawn a partir de ce stade de progression (0-1)")]
        public float progression = 0f;
    }

    [Header("Popups normales")]
    [Tooltip("On peut en mettre une plusieurs fois dans la liste pour qu'elle apparaisse plus souvent")]
    public TypeDePopup[] popups;
    [Tooltip("Délai entre deux popups au début de la partie (secondes)")]
    public float delaiAuDebut = 1.2f;
    [Tooltip("Délai le plus court, atteint au bout de 'tempsPourDelaiMinimum' secondes")]
    public float delaiMinimum = 0.2f;
    [Tooltip("Le temps avant lequel on attend la fréquence max de spawn")]
    public float tempsPourDelaiMinimum = 300f;
    [Tooltip("On va éviter de bruler les pauvres petit pc de RUBIKA")]
    public int maxPopupsEnMemeTemps = 100;

    [Header("Boss")]
    [Tooltip("La liste des boss avec le moment de progression a laquel ils spawn")]
    public TypeBoss[] boss;


    float prochainePopup = 1f;
    int numeroBoss = 0;
    int ordreAffichage = 10000; // chaque nouvelle popup s'affiche derrière les précédentes ducoup on laisse de la marge

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        GameManager jeu = GameManager.instance;

        float temps = GameManager.instance.tempsEcoule;

        if (temps >= prochainePopup && Popup.list.Count < maxPopupsEnMemeTemps)
        {
            FaireApparaitre(ChoisirPopupAuHasard(temps), PositionHorsEcran());

            // Le délai diminue petit à petit de delaiAuDebut vers delaiMinimum
            float avancement = temps / tempsPourDelaiMinimum; // 0 au début, 1 (ou plus) ensuite
            prochainePopup = temps + Mathf.Lerp(delaiAuDebut, delaiMinimum, avancement);
        }


        // on fait spawn les boss
        if (numeroBoss < boss.Count() && jeu.Progression() >= boss[numeroBoss].progression)
        {
            FaireApparaitre(boss[numeroBoss].prefab, PositionHorsEcran());
            numeroBoss++;
        }
    }

    public void FaireApparaitre(GameObject prefab, Vector2 position)
    {
        if (prefab == null) return;

        GameObject nouvelle = Instantiate(prefab, position, Quaternion.identity);
        nouvelle.GetComponentInChildren<Canvas>().sortingOrder = ordreAffichage;
        ordreAffichage--;
    }

    // punition quand on clique sur le mauvais bouton : une popup surgit à côté (du côté opposé au joueur)
    public void FaireApparaitrePres(Vector2 position)
    {
        Vector2 joueur = GameManager.instance.joueur.transform.position;
        Vector2 aLOppose = (position - joueur).normalized;
        FaireApparaitre(ChoisirPopupAuHasard(GameManager.instance.tempsEcoule), position + aLOppose * 1.5f);
    }

    GameObject ChoisirPopupAuHasard(float temps)
    {
        // on garde seulement les popups déjà débloquées
        List<GameObject> disponibles = new List<GameObject>();
        foreach (TypeDePopup type in popups)
        {
            if (temps >= type.apparaitApres) disponibles.Add(type.prefab);
        }
        if (disponibles.Count == 0) return null;

        // ...puis on en tire une au hasard
        return disponibles[Random.Range(0, disponibles.Count)];
    }

    // donne un point juste en dehors de ce que voit la caméra (la caméra suit le joueur)
    Vector2 PositionHorsEcran()
    {
        Camera cam = Camera.main;
        Vector2 centre = cam.transform.position;
        float demiHauteur = cam.orthographicSize;
        float demiLargeur = cam.orthographicSize * cam.aspect;

        // On choisit un côté au hasard
        int cote = Random.Range(0, 4);
        if (cote == 0) return centre + new Vector2(Random.Range(-demiLargeur, demiLargeur), demiHauteur); // haut
        if (cote == 1) return centre + new Vector2(Random.Range(-demiLargeur, demiLargeur), -demiHauteur); // bas
        if (cote == 2) return centre + new Vector2(-demiLargeur, Random.Range(-demiHauteur, demiHauteur)); // gauche
        return centre + new Vector2(demiLargeur, Random.Range(-demiHauteur, demiHauteur)); // droite
    }
}

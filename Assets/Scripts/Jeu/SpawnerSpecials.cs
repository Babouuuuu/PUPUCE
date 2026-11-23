using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

// gère l'apparition des popups, leurs fréquences type etc
public class SpawnerSpecials : MonoBehaviour
{
    public static SpawnerSpecials instance;

    // [System.Serializable] permet de voir et remplir cette petite classe dans l'Inspector
    [System.Serializable]
    public class TypeDePopup
    {
        public GameObject prefab;
        [Tooltip("La popup peut apparaître à partir de ce moment (en secondes)")]
        public float apparaitApres = 0f;
    }
   
    [Header("Popups normales")]
    [Tooltip("On peut en mettre une plusieurs fois dans la liste pour qu'elle apparaisse plus souvent")]
    public TypeDePopup[] popups;
    [Tooltip("Délai entre deux popups au début de la partie (secondes)")]
    public float delaiAuDebut = 3f;
    [Tooltip("Délai le plus court, atteint au bout de 'tempsPourDelaiMinimum' secondes")]
    public float delaiMinimum = 1f;
    [Tooltip("Le temps avant lequel on attend la fréquence max de spawn")]
    public float tempsPourDelaiMinimum = 500f;
    [Tooltip("On va éviter de bruler les pauvres petit pc de RUBIKA")]
    public int maxPopupsEnMemeTemps = 2;

    float prochainePopup = 1f;
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
    }

    public void FaireApparaitre(GameObject prefab, Vector2 position)
    {
        if (prefab == null) return;

        GameObject nouvelle = Instantiate(prefab, position, Quaternion.identity);
        nouvelle.GetComponentInChildren<Canvas>().sortingOrder = ordreAffichage;
        ordreAffichage--;
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

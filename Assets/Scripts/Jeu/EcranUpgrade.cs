using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// l'écran de choix d'upgrade ouvert par le GameManager à chaque palier
// il tire au hasard des upgrades et un malus dans les listes remplies dans l'Inspector
public class EcranUpgrade : MonoBehaviour
{
    // une ligne de la liste -> une upgrade ou un malus les deux marche pareil
    [System.Serializable]
    public class TypeUpgrade
    {
        public string nom;
        public Sprite icone;
        [TextArea] public string description;
        public GameObject prefab;
        [Tooltip("qty max stackable")]
        public int quantiteMax = 3;
        [HideInInspector] public int quantite; // combien de fois on l'a déjà pris
    }

    // les éléments d'une carte de lécran update
    [System.Serializable]
    public class Carte
    {
        public GameObject objet;
        public Image icone;
        public Text nom;
        public Text niveau;
        public Text description;
    }

    // pour ajouter une upgrade ou un malus il suffit d'ajouter une ligne dans l'Inspector
    [Header("Les listes")]
    public TypeUpgrade[] upgrades;
    [Tooltip("Accepter un malus donne une upgrade en plus")]
    public TypeUpgrade[] malus;

    // il y a autant de tirages que de cartes
    [Header("Les cartes de l'écran")]
    public Carte[] cartesUpgrade;
    public Carte[] cartesMalus;

    List<TypeUpgrade> upgradesTirees;
    List<TypeUpgrade> malusTires;
    float heureOuverture;

    public void Ouvrir()
    {
        upgradesTirees = TirerAuHasard(upgrades, cartesUpgrade.Length);

        // Si tout est déjà au niveau maximum, il n'y a rien à choisir
        if (upgradesTirees.Count == 0)
        {
            GameManager.instance.UpgradeChoisie();
            return;
        }

        // Un malus rapporte une upgrade en plus -> on ne le propose pas s'il ne reste plus rien à gagner
        if (UpgradesRestantes() > 1 + GameManager.instance.upgradesEnAttente) malusTires = TirerAuHasard(malus, cartesMalus.Length);
        else malusTires = new List<TypeUpgrade>();

        gameObject.SetActive(true);
        // Le jeu est en pause (Time.timeScale = 0) : on utilise donc le temps "non mis à l'échelle"
        heureOuverture = Time.unscaledTime;

        AfficherCartes(cartesUpgrade, upgradesTirees);
        AfficherCartes(cartesMalus, malusTires);
    }

    // branchée sur le OnClick du bouton "Installer" de chaque carte (le numéro de la carte est réglé dans l'Inspector)
    public void ChoisirUpgrade(int numeroCarte)
    {
        if (ClicTropRapide()) return;
        Installer(upgradesTirees[numeroCarte]);
        gameObject.SetActive(false);
        GameManager.instance.UpgradeChoisie();
    }

    // branchée sur le OnClick du bouton "Accepter" de la carte de malus
    // l'écran reste ouvert pour choisir l'upgrade, puis le GameManager le rouvrira pour l'upgrade en plus
    public void ChoisirMalus(int numeroCarte)
    {
        if (ClicTropRapide()) return;
        Installer(malusTires[numeroCarte]);
        GameManager.instance.upgradesEnAttente++;
        AfficherCartes(cartesMalus, new List<TypeUpgrade>()); // une liste vide cache les cartes : un seul malus par écran
    }

    List<TypeUpgrade> TirerAuHasard(TypeUpgrade[] liste, int nombre)
    {
        // on garde seulement ceux qui n'ont pas atteint leur limite
        List<TypeUpgrade> disponibles = new List<TypeUpgrade>();
        foreach (TypeUpgrade type in liste)
        {
            if (type.quantite < type.quantiteMax) disponibles.Add(type);
        }

        // tirage sans remise : ce qui est tiré sort de la liste donc impossible de l'avoir 2 fois
        List<TypeUpgrade> tirage = new List<TypeUpgrade>();
        while (tirage.Count < nombre && disponibles.Count > 0)
        {
            int numero = Random.Range(0, disponibles.Count);
            tirage.Add(disponibles[numero]);
            disponibles.RemoveAt(numero);
        }
        return tirage;
    }

    // le nombre d'upgrades qu'on peut encore prendre avant que tout soit au maximum
    int UpgradesRestantes()
    {
        int restantes = 0;
        foreach (TypeUpgrade type in upgrades)
        {
            restantes += type.quantiteMax - type.quantite;
        }
        return restantes;
    }

    void AfficherCartes(Carte[] cartes, List<TypeUpgrade> tirage)
    {
        for (int i = 0; i < cartes.Length; i++)
        {
            // pas assez de choses à proposer -> on cache les cartes en trop
            cartes[i].objet.SetActive(i < tirage.Count);
            if (i >= tirage.Count) continue;

            cartes[i].icone.sprite = tirage[i].icone;
            cartes[i].nom.text = tirage[i].nom;
            cartes[i].niveau.text = "Niveau " + tirage[i].quantite + " / " + tirage[i].quantiteMax;
            cartes[i].description.text = tirage[i].description;
        }
    }

    // Évite de choisir par erreur si on était en train de cliquer frénétiquement sur une popup
    bool ClicTropRapide()
    {
        return Time.unscaledTime < heureOuverture + 0.4f;
    }

    void Installer(TypeUpgrade type)
    {
        type.quantite++;
        Instantiate(type.prefab, GameManager.instance.joueur.transform.position, Quaternion.identity);
    }
}

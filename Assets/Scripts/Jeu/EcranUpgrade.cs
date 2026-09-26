using UnityEngine;
using UnityEngine.UI;

// l'écran de choix d'upgrade ouvert par le GameManager à chaque palier
public class EcranUpgrade : MonoBehaviour
{
    public int niveauMax = 4;

    [Header("Souris autonome")]
    public GameObject prefabSouris;
    public Button boutonSouris;
    public Text niveauSouris;

    [Header("Clippy")]
    public GameObject prefabClippy;
    public Button boutonClippy;
    public Text niveauClippy;

    [Header("Logo DVD")]
    public GameObject prefabDVD;
    public Button boutonDVD;
    public Text niveauDVD;

    int nbSouris = 0;
    int nbClippy = 0;
    int nbDVD = 0;
    float heureOuverture;

    public void Ouvrir()
    {
        // Si tout est déjà au niveau maximum, il n'y a rien à choisir
        if (nbSouris >= niveauMax && nbClippy >= niveauMax && nbDVD >= niveauMax)
        {
            GameManager.instance.UpgradeChoisie();
            return;
        }

        gameObject.SetActive(true);
        // Le jeu est en pause (Time.timeScale = 0) : on utilise donc le temps "non mis à l'échelle"
        heureOuverture = Time.unscaledTime;

        niveauSouris.text = "Niveau " + nbSouris + " / " + niveauMax;
        niveauClippy.text = "Niveau " + nbClippy + " / " + niveauMax;
        niveauDVD.text = "Niveau " + nbDVD + " / " + niveauMax;

        // Un bouton "interactable = false" est grisé et ne peut plus être cliqué
        boutonSouris.interactable = nbSouris < niveauMax;
        boutonClippy.interactable = nbClippy < niveauMax;
        boutonDVD.interactable = nbDVD < niveauMax;
    }

    // Les 3 fonctions suivantes sont branchées sur le OnClick des boutons "Installer"
    public void ChoisirSouris()
    {
        if (ClicTropRapide()) return;
        nbSouris++;
        FaireApparaitre(prefabSouris);
    }

    public void ChoisirClippy()
    {
        if (ClicTropRapide()) return;
        nbClippy++;
        FaireApparaitre(prefabClippy);
    }

    public void ChoisirDVD()
    {
        if (ClicTropRapide()) return;
        nbDVD++;
        FaireApparaitre(prefabDVD);
    }

    // Évite de choisir par erreur si on était en train de cliquer frénétiquement sur une popup
    bool ClicTropRapide()
    {
        return Time.unscaledTime < heureOuverture + 0.4f;
    }

    void FaireApparaitre(GameObject prefab)
    {
        Instantiate(prefab, GameManager.instance.joueur.transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        GameManager.instance.UpgradeChoisie();
    }
}

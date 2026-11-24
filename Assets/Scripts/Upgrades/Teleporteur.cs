using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Teleporteur : MonoBehaviour
{
    [Header("Téléportation")]
    [Tooltip("Secondes entre deux téléportations")]
    public float delaiTeleportation = 5f;
    [Tooltip("La tornade n'apparaît jamais dans ce carré autour du joueur (moitié du côté). À garder plus petit que la moitié de la hauteur de l'écran moins la marge, sinon les zones haut et bas n'existent plus")]
    public float zoneExclusion = 3f;
    [Tooltip("Pour ne pas être coupée par le bord de l'écran")]
    public float margeEcran = 1.5f;

    public float delaiApparition = 5f;

    float prochaineTeleportation;

    float reapparition;

    float heurePremier;

    public GameObject blueClic;
    public GameObject buttonTeleport;

    void Start()
    {
        TeleporterU();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= prochaineTeleportation) TeleporterU();
        if (Time.time >= reapparition) Apparaitre();

        if (!DoubleClic())
        {
            blueClic.SetActive(false);
        }
    }

    public void Clic()
    {
        Debug.Log("Clic");
        blueClic.SetActive(true);

        if (DoubleClic())
        {
            TeleporterP();
            Debug.Log("teleporterP");
            TeleporterU();
            blueClic.SetActive(false);
            buttonTeleport.SetActive(false);
        }
        else
        {
            heurePremier = Time.unscaledTime;
            Debug.Log("heurePremier");
        }
    }

    bool DoubleClic()
    {
        Debug.Log("ClicRapide");
        return Time.unscaledTime < heurePremier + 0.4f;
    }

    void TeleporterU()
    {
        prochaineTeleportation = Time.time + delaiTeleportation;
        transform.position = PositionAutourDuJoueur();
    }

    Vector2 PositionAutourDuJoueur()
    {
        Camera cam = Camera.main;
        Vector2 joueur = GameManager.instance.joueur.transform.position;
        float limiteX = cam.orthographicSize * cam.aspect - margeEcran;
        float limiteY = cam.orthographicSize - margeEcran;

        // On choisit un côté au hasard, puis un point dans sa zone
        int cote = Random.Range(0, 4);
        if (cote == 0) return joueur + new Vector2(Random.Range(-limiteX, limiteX), Random.Range(zoneExclusion, limiteY)); // haut
        if (cote == 1) return joueur + new Vector2(Random.Range(-limiteX, limiteX), Random.Range(-limiteY, -zoneExclusion)); // bas
        if (cote == 2) return joueur + new Vector2(Random.Range(-limiteX, -zoneExclusion), Random.Range(-limiteY, limiteY)); // gauche
        return joueur + new Vector2(Random.Range(zoneExclusion, limiteX), Random.Range(-limiteY, limiteY)); // droite
    }

    void TeleporterP()
    {
        GameManager.instance.joueur.transform.position = gameObject.transform.position;
    }

    void Apparaitre()
    {
        buttonTeleport.SetActive(true);
        reapparition = Time.time + delaiApparition;
    }
}

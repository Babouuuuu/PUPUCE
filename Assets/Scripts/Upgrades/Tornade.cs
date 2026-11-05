using UnityEngine;

// Upgrade "Tornade" : un tourbillon qui attire les popups vers son centre
// et qui se téléporte ailleurs sur l'écran à un rythme donné, toujours à distance du joueur
public class Tornade : MonoBehaviour
{
    [Header("Attraction")]
    [Tooltip("Les popups à moins de cette distance sont attirées")]
    public float rayonAttraction = 3.5f;
    [Tooltip("La vitesse d'aspiration, à garder au dessus de la vitesse des popups sinon elles s'échappent")]
    public float forceAttraction = 3f;

    [Header("Téléportation")]
    [Tooltip("Secondes entre deux téléportations")]
    public float delaiTeleportation = 5f;
    [Tooltip("La tornade n'apparaît jamais dans ce carré autour du joueur (moitié du côté). À garder plus petit que la moitié de la hauteur de l'écran moins la marge, sinon les zones haut et bas n'existent plus")]
    public float zoneExclusion = 3f;
    [Tooltip("Pour ne pas être coupée par le bord de l'écran")]
    public float margeEcran = 1.5f;

    [Header("Visuel")]
    public float vitesseRotation = 360f;

    float prochaineTeleportation;

    void Start()
    {
        Teleporter(); // elle est créée sur le joueur -> on l'éloigne tout de suite
    }

    void Update()
    {
        // Time.deltaTime vaut 0 quand le jeu est en pause donc elle s'arrête toute seule
        transform.Rotate(0f, 0f, vitesseRotation * Time.deltaTime);

        if (Time.time >= prochaineTeleportation) Teleporter();
    }

    // FixedUpdate car c'est là que les popups calculent leur déplacement
    void FixedUpdate()
    {
        foreach (Popup popup in Popup.list)
        {
            Vector2 versLeCentre = transform.position - popup.transform.position;
            if (versLeCentre.magnitude > rayonAttraction) continue; // trop loin -> on passe à la suivante

            // ClampMagnitude : la force diminue dans la dernière unité pour que les popups ne tremblent pas au centre
            popup.Attirer(Vector2.ClampMagnitude(versLeCentre, 1f) * forceAttraction);
        }
    }

    void Teleporter()
    {
        prochaineTeleportation = Time.time + delaiTeleportation;
        transform.position = PositionAutourDuJoueur();
    }

    // l'écran moins le carré d'exclusion, ça laisse 4 zones autour du joueur (il est toujours au centre car la caméra le suit)
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
}

using UnityEngine;

// souris qui autonomes
public class SourisAutonome : MonoBehaviour
{
    [Tooltip("secondes pour aller d'une popup a la suivante")]
    public float delaiEntreDeuxPopups = 2f;

    Popup cible;
    float heureDuClic; // le moment où la souris arrive sur sa cible et clique

    void Update()
    {
        if (Time.deltaTime <= 0f) return; // jeu en pause donc logique elle bouge pas

        // pas de cible -> on en cherche une nouvelle
        if (cible == null)
        {
            cible = TrouverUnePopup();
            if (cible == null) return; // aucune popup à l'écran donc on ne fait rien
        
            cible.cibleeParUneSouris = true; // pour eviter que dautre souris ne prenne la même cible
            heureDuClic = Time.time + delaiEntreDeuxPopups; // le trajet commence maintenant
        }

        // elle est recalculée a chaque frame car la popup avance pendant le trajet et il faut que la souris arrive pile à l'heure
        Vector2 versLaCible = cible.transform.position - transform.position;

        // la max c pour eviter de se retrouver avec une vitesse négative si la souris est en retard -> sinon vitesse négative
        float tempsRestant = Mathf.Max(heureDuClic - Time.time, Time.deltaTime);
        // la c litéralement vitesse = ditance / temps
        float vitesse = versLaCible.magnitude / tempsRestant;

        // rotation du sprite pour que le doigt soit tourner vers la cible
        if (versLaCible.magnitude > 0.1f) transform.up = versLaCible;
        transform.position = Vector2.MoveTowards(transform.position, cible.transform.position, vitesse * Time.deltaTime);

        // la quand le temps est fini on click (on fait laction de la fenetre il faudra coder plus tard les interaction)
        if (Time.time >= heureDuClic)
        {
            cible.cibleeParUneSouris = false;
            cible.Interaction();
            cible = null;
        }
    }

    Popup TrouverUnePopup()
    {
        Popup laPlusProche = null;
        float distanceMin = Mathf.Infinity;

        // la on parcours toutes les popup pour recup la plus proche qui est pas deja visée par une souris
        foreach (Popup popup in Popup.list)
        {
            // continue sert a skip cette popup de la boucle directement si celle ci est déja ciblé par une souris
            if (popup.cibleeParUneSouris) continue;

            float distance = Vector2.Distance(transform.position, popup.transform.position);
            if (distance < distanceMin)
            {
                distanceMin = distance;
                laPlusProche = popup;
            }
        }
        return laPlusProche;
    }
}

using UnityEngine;

// Upgrade "Clippy" : il s'accroche la popup la plus proche de System32 et la bloque un temps donné
public class Clippy : MonoBehaviour
{
    public float vitesse = 6f;
    public float dureeBlocage = 3f;

    Popup cible;
    bool accroche;

    void Update()
    {
        // Pas de cible (ou elle a été fermée) : on en cherche une nouvelle
        if (cible == null)
        {
            accroche = false;
            cible = TrouverUnePopup();
            if (cible == null) return;
            cible.cibleeParClippy = true;
        }

        // Le coin en haut a droite de la popup
        // La popup est dans un Canvas réduit à 1 % (Scale 0.01) : 1 unité UI = 0,01 unité du monde.
        Vector3 coin = cible.transform.position + new Vector3(cible.fenetre.rect.width * 0.005f - 0.2f,
                                                              cible.fenetre.rect.height * 0.005f, 0f);

        // la si on est pas deja accrocher a une popup on va le faire
        if (!accroche)
        {
            // on avance vers la popup
            transform.position = Vector3.MoveTowards(transform.position, coin, vitesse * Time.deltaTime);
            // quand on est dessus on bloque
            if (Vector3.Distance(transform.position, coin) < 0.05f)
            {
                cible.Bloquer(dureeBlocage);
                accroche = true;
            }
        }
        else if (!cible.bloquee)
        {
            // le blocage est terminé -> on passe a une autre popup
            cible = null;
        }
    }

    Popup TrouverUnePopup()
    {
        Vector3 joueur = GameManager.instance.joueur.transform.position;
        Popup laPlusProche = null;
        float distanceMin = Mathf.Infinity;

        // la on parcours toutes les popup pour recup la plus proche qui est pas deja visée par un clippy
        foreach (Popup popup in Popup.list)
        {
            // continue sert a skip cette popup de la boucle directement si celle ci est déja ciblé par un clippy 
            if (popup.cibleeParClippy) continue;

            float distance = Vector3.Distance(joueur, popup.transform.position);
            if (distance < distanceMin)
            {
                distanceMin = distance;
                laPlusProche = popup;
            }
        }
        return laPlusProche;
    }
}

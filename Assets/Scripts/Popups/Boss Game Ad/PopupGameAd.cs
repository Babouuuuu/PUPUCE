using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ba c les popup quoi... jsp quoi dire de plus en gros c la classe mère, 
// toutes les popup se base dessus donc c ici quon va coder tout les comportements commun a toutes les popup
public class PopupGameAd : MonoBehaviour
{
    public GameObject[] popups;
    public GameObject slimePrefab;
    public Popup popup;

    // branchée sur les boutons (OnClick) dans l'Inspector et appelée par les scripts d'interaction, les souris et le logo DVD
    public void InteractionSlime()
    {
        popup.Interaction();
        if (popup.interactionsRestantes <= 0)
        {
            Dupliquer(transform.position);
        }
        else
        {
            popup.Trembler();
            Reproduction(transform.position);
        }
    }

    public void Reproduction(Vector2 position)
    {
        popup.Trembler();
        Vector2 offset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        SpawnerPopups.instance.FaireApparaitre(slimePrefab, position + offset);
    }

    void Dupliquer(Vector2 position)
    {
        foreach (GameObject popup in popups)
        {
            Vector2 offset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            SpawnerPopups.instance.FaireApparaitre(popup, position + offset);
        }
    }
}

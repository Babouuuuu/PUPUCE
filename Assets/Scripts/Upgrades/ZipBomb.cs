using System;
using UnityEngine;

public class ZipBomb : MonoBehaviour
{
    // Lance une ZIP BOMB qui va se décompresser au contact d'un ennemis sur lequel il tombe et faire des dégats de zones

    public float speed = 3f;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Vector2 direction;
    
    Popup cible;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (cible == true)
        {
            
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
            if (popup.cibleeParZipBombe) continue;

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
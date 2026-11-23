using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class YouAreAnIdiot : MonoBehaviour
{
    public float vitesse = 3f;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Vector2 direction;
    Popup popup;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        popup = GetComponent<Popup>();
        
        // trouve une direction au hasard (en diagonal)
        // btw le ? c comme un if/else
        float x = Random.value < 0.5f ? -1f : 1f;
        float y = Random.value < 0.5f ? -1f : 1f;
        direction = new Vector2(x, y).normalized;
    }

    void Update()
    {
        // on calcul les bord max
        // comme la camera est fixé au joueur il faut en tenir compte
        Camera cam = Camera.main;
        float limiteX = cam.orthographicSize * cam.aspect - sprite.bounds.extents.x;
        float limiteY = cam.orthographicSize - sprite.bounds.extents.y;
        Vector2 position = transform.position - cam.transform.position;

        // Rebond : si on dépasse un bord en allant vers lui, on repart dans l'autre sens
        if ((position.x > limiteX && direction.x > 0f) || (position.x < -limiteX && direction.x < 0f))
        {
            direction.x = -direction.x;
        }
        if ((position.y > limiteY && direction.y > 0f) || (position.y < -limiteY && direction.y < 0f))
        {
            direction.y = -direction.y;
        }

        rb.linearVelocity = direction * vitesse;

        popup.bloquee = true;
    }
}

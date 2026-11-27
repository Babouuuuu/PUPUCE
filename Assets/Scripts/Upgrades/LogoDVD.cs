using UnityEngine;

public class LogoDVD : MonoBehaviour
{
    public float vitesse = 3f;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Vector2 direction;
    public bool bloquee;
    public float finBlocage;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        
        // trouve une direction au hasard (en diagonal)
        // btw le ? c comme un if/else
        float x = Random.value < 0.5f ? -1f : 1f;
        float y = Random.value < 0.5f ? -1f : 1f;
        direction = new Vector2(x, y).normalized;
    }

    void FixedUpdate()
    {
        if (bloquee && Time.time >= finBlocage) Debloquer();
    }

    void Update()
    {
        if (bloquee) return;
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
    }

    // Appelé automatiquement quand le logo (collider "Is Trigger") touche un autre collider
    void OnTriggerEnter2D(Collider2D autre)
    {
        Popup popup = autre.GetComponent<Popup>();
        if (popup != null) popup.Interaction();

        if (GameManager.instance.malwareActif == true && autre.CompareTag("Player") == false && autre.CompareTag("Upgrades") == false && autre.CompareTag("Cursor") == false && bloquee == false)
        {
            Bloquer();
            Debug.Log("Bloquer");
        }
    }

    void Bloquer()
    {
        bloquee = true;
        finBlocage = Time.time + GameManager.instance.tempsImmobilitéUpgrade;
        rb.bodyType = RigidbodyType2D.Static;
        sprite.color = Color.gray;
    }

    void Debloquer()
    {
        bloquee = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        sprite.color = Color.white;
    }
}

using UnityEngine;

public class ZipBombe : MonoBehaviour
{
    public float speed = 3f;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Vector2 direction;

    Popup cible;

    public bool estLanceur = true;    
    public float cooldown = 3f;       
    public float distanceImpact = 0.3f; 
    float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

       
        sprite.enabled = !estLanceur;

        if (!estLanceur) ChoisirCible();
    }

    private void Update()
    {
        
        if (estLanceur)
        {
            transform.position = GameManager.instance.joueur.transform.position;

            timer += Time.deltaTime;
            if (timer >= cooldown)
            {
                timer = 0f;
                ZipBombe zip = Instantiate(this, transform.position, Quaternion.identity);
                zip.estLanceur = false; 
            }
            return;
        }

        // pas de cible (ou elle a été détruite entre temps) : on en cherche une autre
        if (cible == null)
        {
            ChoisirCible();
            if (cible == null) { Destroy(gameObject); return; } // aucune popup dispo
        }

        // direction vers la cible
        direction = (cible.transform.position - transform.position).normalized;

        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Vector2.Distance(transform.position, cible.transform.position) <= distanceImpact)
        {
            Exploser();
        }
    }

    
    void FixedUpdate()
    {
        if (estLanceur) return;
        rb.linearVelocity = direction * speed;
    }

    // AJOUT
    void ChoisirCible()
    {
        cible = TrouverUnePopup();
        if (cible != null) cible.cibleeParZipBombe = true; // on la marque pour qu'une autre zip bomb ne la prenne pas
    }

   
    void Exploser()
    {
        cible.Detruire(); // la cible meurt directement

        // ici on pourra ajouter plus tard : dégâts de zone, particules

        Destroy(gameObject);
    }


    void OnDestroy()
    {
        if (cible != null) cible.cibleeParZipBombe = false;
    }

    Popup TrouverUnePopup()
    {
        Vector3 joueur = GameManager.instance.joueur.transform.position;
        Popup laPlusProche = null;
        float distanceMin = Mathf.Infinity;

        // la on parcours toutes les popup pour recup la plus proche qui est pas deja visée par un clippy
        foreach (Popup popup in Popup.list)
        {
            if (popup == null) continue; 

            
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

using UnityEngine;

public class InteractionLaggy : MonoBehaviour
{
    public GameObject popupLaggy;
    public float MinX;
    public float MaxX;

    public float MinY;
    public float MaxY;
    public Popup popup;

    // Update is called once per frame
    void Update()
    {
        if (popup.interactionsRestantes <= 0) return;

        if (popup.interactionSpeciale == true)
        {
            ChooseRange();
            Vector2 joueur = GameManager.instance.joueur.transform.position;
            popupLaggy.transform.position = joueur + new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));
            Debug.Log("Popup laggy teleported to: " + popupLaggy.transform.position);
            popup.interactionSpeciale = false;
        }
        
    }

    void ChooseRange()
    {
        Random.Range(0, 2);
        if (Random.value < 0.5f)
        {
            MinX = -7f;
            MaxX = -2.5f;
            Debug.Log("MinX: " + MinX + ", MaxX: " + MaxX);
        }
        else
        {
            MinX = 7f;
            MaxX = 2.5f;
            Debug.Log("MinX: " + MinX + ", MaxX: " + MaxX);
        }

        Random.Range(0, 2);
        if (Random.value < 0.5f)
        {
            MinY = -4f;
            MaxY = -2.5f;
            Debug.Log("MinY: " + MinY + ", MaxY: " + MaxY);
        }
        else
        {
            MinY = 4f;
            MaxY = 2.5f;
            Debug.Log("MinY: " + MinY + ", MaxY: " + MaxY);
        }
    }
}

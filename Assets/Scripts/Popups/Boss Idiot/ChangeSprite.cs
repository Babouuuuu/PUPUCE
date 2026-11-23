using NUnit.Framework;
using TMPro;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    Popup popup;
    public float delai = 0.5f;
    float countdown;
    public Sprite spriteWhite;
    public Sprite spriteBlack;
    bool isWhite = false;
    bool isBlack = false;
    
    void Start()
    {
        countdown = delai;
        if (popup == null)
        {
            popup = GetComponent<Popup>();
        }

        if (popup.fond.sprite == spriteBlack)
        {
            isBlack = true;
        }

        if (popup.fond.sprite == spriteWhite)
        {
            isWhite = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0 && isBlack == true)
        {
            popup.fond.sprite = spriteWhite;
            popup.titre.color = Color.black;
            countdown = delai;
            isBlack = false;
            isWhite = true;
        }
        else if (countdown <= 0 && isWhite == true)
        {
            popup.fond.sprite = spriteBlack;
            popup.titre.color = Color.white;
            countdown = delai;
            isWhite = false;
            isBlack = true;
        }
    }
}

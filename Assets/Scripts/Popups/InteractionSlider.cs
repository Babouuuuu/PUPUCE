using UnityEngine;
using UnityEngine.UI;

// interaction "faire glisser le slider jusqu'au bout".
public class InteractionSlider : MonoBehaviour
{
    public Popup popup;
    public Slider slider;

    void Update()
    {
        if (slider.value >= 0.99f)
        {
            slider.value = 0f; // on remet à zéro (utile pour les popups a plusieurs interactions)
            popup.Interaction();
        }
    }
}

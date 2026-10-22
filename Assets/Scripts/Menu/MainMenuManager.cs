using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
public class MainMenuManager : MonoBehaviour
 
{
    /// Fait le son quand le bouton est cliquer, requiers le menu manager dans audioSource et le son jouer dans SonClick
    public AudioSource audioSource;
    public void ClickSound(AudioClip Son)
    {
        audioSource.PlayOneShot(Son);
    }
    /// Permet de changer de scéne
    public void LancerPartie()
    {
        Invoke("ChargerPartie", 0.5f);
    }

    void ChargerPartie()
    {
        SceneManager.LoadSceneAsync("Jeu");
    }
    /// Permet d'etteindre l'app
    public void Quitter()
    {
        Debug.Log("Le jeux devrais s'etteindre la mtn tout desuite");
        Application.Quit();
    }

    [SerializeField] private AudioMixer mixeuraudio;
    [SerializeField] private Slider slidermusic;
    
    /// Slider Son
    public void Setvolumeson()
    {
        float Volume = slidermusic.value;
        mixeuraudio.SetFloat("Volumemain", Mathf.Log10(Volume) * 20);
    }

    /// Slider interaction text
    public TMP_Text SliderText;

    public void UpdateSliderText(float value)
    {
        
        Debug.Log("UpdateSliderText called with " + value);
        SliderText.text = value.ToString();
        SliderText.text ="Votre age favoris est: " + Mathf.RoundToInt(value*100).ToString();
    }

    public void Start()
    {
        UpdateSliderText(slidermusic.value);
    }
}
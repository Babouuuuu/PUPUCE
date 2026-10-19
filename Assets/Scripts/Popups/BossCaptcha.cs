using UnityEngine;

public class BossCaptcha : MonoBehaviour
{
    [HideInInspector] public int randomNumber;
    [HideInInspector] public int randomNumber2;

    public GameObject captchaRobot;
    public GameObject captchaCar;
    public GameObject captchaDog;
    public GameObject captchaGame;


    public Popup popupCaptcha;


    public void StartCaptcha()
    {
        randomNumber = Random.Range(1,4);

        if (randomNumber == 1)
        {
            captchaCar.SetActive(true);
        }
        else if (randomNumber == 2)
        {
            captchaDog.SetActive(true);
        }
        else if (randomNumber == 3)
        {
            captchaGame.SetActive(true);
        }
    
    }

    public void ResetCaptcha()
    {
        captchaCar.SetActive(false);
        captchaDog.SetActive(false);
        captchaGame.SetActive(false);

        randomNumber2 = Random.Range(1,4);

        if (randomNumber2 == randomNumber)
        {
            randomNumber2 = randomNumber2 + 1;
        }

        if (randomNumber2 == 1)
        {
            captchaCar.SetActive(true);
        }
        else if (randomNumber2 == 2)
        {
            captchaDog.SetActive(true);
        }
        else if (randomNumber2 == 3)
        {
            captchaGame.SetActive(true);
        }
        if (randomNumber2 == 4)
        {
            captchaCar.SetActive(true);
        }
    }
}

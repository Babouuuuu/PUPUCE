using UnityEngine;
using UnityEngine.UI;
public class UIachievement : MonoBehaviour
{
    public string achievementName;   // Doit match le titre
    public Image icon;               // l'objet qui display l'image
    public Sprite lockedSprite;
    public Sprite unlockedSprite;

    private void Awake()
    {
        if (icon == null) icon = GetComponent<Image>();
    }

   // le slot s'inscrit auprès du manager quand il apparaît
    private void OnEnable()
    {
        Achievementmanager.Register(this);
    }

    // le slot se désinscrit quand il disparaît 
    private void OnDisable()
    {
        Achievementmanager.Unregister(this);
    }

    public void Refresh(bool unlocked)
    {
        if (icon == null) return;
        icon.sprite = unlocked ? unlockedSprite : lockedSprite;
    }
}
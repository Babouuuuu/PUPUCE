using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Achievementmanager : MonoBehaviour
// L'humble list des achievements, avec un integer de test afin de voir si sa marche ou pas <3
{
    public static Achievementmanager Instance; // NEW
    private static List<UIachievement> registeredSlots = new List<UIachievement>(); // NEW
    public static List<Achievement> achievements;

    public int integer;
    public float floating_point;
    private float idleTimer;
    public int idleSeconds;
    private Vector3 lastMousePos;

 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeAchievements();
        RefreshSlots();
    }

 
    private void OnEnable()
    {
        Achievement.OnUnlocked += HandleUnlocked;
    }

    private void OnDisable()
    {
        Achievement.OnUnlocked -= HandleUnlocked;
    }

    private void HandleUnlocked(Achievement a)
    {
        RefreshSlots();
    }

   
    public static void Register(UIachievement slot)
    {
        registeredSlots.Add(slot);
        slot.Refresh(AchievementUnlocked(slot.achievementName));
    }

    public static void Unregister(UIachievement slot)
    {
        registeredSlots.Remove(slot);
    }

   
    public static void RefreshSlots()
    {
        foreach (var slot in registeredSlots)
        {
            if (slot == null) continue;
            slot.Refresh(AchievementUnlocked(slot.achievementName));
        }
    }

//Une booléenne qui est dans l'inspectueur, et qui accept un string pour le nom de l'achievement
    public static bool AchievementUnlocked(string achievementName) // FIX: now static
    {
        bool result = false;
//Si t'a pas l'achievement...T'a pas l'achievement.
        if (achievements == null)
           // en gros pour les singe qui comprénne pas, "return" c "ah je quitte cette fonction", c'est une manniére de stop le script quand il a un résultat, genre "si sex=false, return." Sa permet aussi de faire en sorte qu'une méthode rapporte quelque chose (si tu veut pas tu met void)
            return false;
//En gros sa met les achievement de la liste en array, sa leur demande "existe tu?" si c'est oui, "est tu accomplis?" 
        Achievement[] achievementsArray = achievements.ToArray();
        Achievement a = Array.Find(achievementsArray, ach => achievementName == ach.titre);
//Fuyez pauvre fous!
        if (a == null)
            return false;

        result = a.achieved;

        return result;
    }
    

    private void InitializeAchievements()
    {
        if (achievements != null)
            return;
// Dieux dit alor, "mon fils, tu listera les achievement ici et pas en bordelle dans mon script"
        achievements = new List<Achievement>(); 
        // Des exemmples test du script que j'ai piqué
        achievements.Add(new Achievement("Step By Step", "Set your integer to 110 or above.", (object o) => integer >= 110)); // FIX: 100 -> 110
        achievements.Add(new Achievement("Not So Precise", "Set your floating point to 230.5 or above.", (object o) => floating_point >= 230.5));
        achievements.Add(new Achievement("Faire une Yannis","Vous branler les couilles 30 secondes",(object o)=>idleSeconds>=30)); // FIX: <= -> >=
    }
//Chaque update sa recheck si ta complete un achievement (pas opti mais bon)
    private void Update()
    {
        CheckBranlette();
        CheckAchievementCompletion();
        
        
    }
    //Les conditions d'achievement c'est a L'ARRIERE du bus

    private void CheckBranlette()
    {
        bool active = Input.anyKey || Input.mousePosition != lastMousePos;
        lastMousePos = Input.mousePosition;

        if (active)
        {
            idleTimer = 0;
            idleSeconds = 0;
        }
        else
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= 1f)
            {
                idleTimer -= 1f;
                idleSeconds += 1;
            }
        }
    }

 

    private void CheckAchievementCompletion()
    {
        if (achievements == null)
            return;

        foreach (var achievement in achievements)
        {
            achievement.UpdateCompletion();
        }
    }
}

public class Achievement
{
    public static event Action<Achievement> OnUnlocked; // NEW

    public string titre;
    public string description;
    public Predicate<object> prérequis;
    public bool achieved;

    // Un predicate c'est une liste de questions en gros (jsuis pas sur d'avoir pigé)
    public Achievement(string titre, string description, Predicate<object> prérequis)
    {
        this.titre = titre;
        this.description = description;
        this.prérequis = prérequis;
        //Quand on boot, sa load les achievement unlocked
        achieved = PlayerPrefs.GetInt("Achievement_" + titre, 0) == 1;
    }



    public void UpdateCompletion()
    {
        if (achieved)
            return;

        if (Prerequisvalidé())
        {
            Debug.Log($"{titre}: {description}");
            achieved = true;
            //Quand tu unlock un achievement sa le save
            PlayerPrefs.SetInt("Achievement_" + titre, 1);
            PlayerPrefs.Save();
            OnUnlocked?.Invoke(this); 
        }
    }

    public bool Prerequisvalidé()
    {
        return prérequis.Invoke(null);
    }
    
 
    
}
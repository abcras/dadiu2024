using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class XPContainer : MonoBehaviour
{

    [SerializeField]
    private XPContainerConfig _config;


    [SerializeField]
    static float XpCount;
    [SerializeField]
    static float XPToNextLevel;
    [SerializeField]
    float XPLevelMultiplier;

    int CurrentLevel = 1;

    public static UnityEvent<int> LevelUp = new UnityEvent<int>();

    public void AddXp(float amount)
    {
        XpCount += amount;
        Debug.Log("The current Total XP, " + XpCount + this);

        if (XpCount >= XPToNextLevel)
        {
            var rest = XpCount - XPToNextLevel;
            XPToNextLevel *= XPLevelMultiplier;
            XpCount = rest;
            CurrentLevel++;
            LevelUp.Invoke(CurrentLevel);

            Debug.Log("Leveled up to, " + CurrentLevel);
        }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        XPToNextLevel = _config.StartingXpToFirstLevel;
        XPLevelMultiplier = _config.XPMultiplierPerLevelUp;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

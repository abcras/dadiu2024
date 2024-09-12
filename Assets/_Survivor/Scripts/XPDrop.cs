using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPDrop : MonoBehaviour
{

    static List<XPDrop> XPDropList = new List<XPDrop>();

    public static int MaxXPDropsOnField = 50;

    public float XpAmount;

    private static XPDrop LatestXPDrop;

    //TODO make it a pool, and it adds extra Xp to the last one it created if all are used.


    void OnEnable()
    {
        if (XPDropList.Count < MaxXPDropsOnField)
        {
            XPDropList.Add(this);
            LatestXPDrop = this;
        }
        else
        {
            Destroy(this);
            LatestXPDrop.XpAmount += XpAmount;
        }
    }

    void OnDisable()
    {
        XPDropList.Remove(this);
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<XPContainer>(out var container))
        {
            container.AddXp(XpAmount);
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class Health : MonoBehaviour
{

    public UnityEvent Died = new UnityEvent();

    public struct DamageTakenArgs
    {
        public float CurrentRatio;
    }

    public UnityEvent<DamageTakenArgs> DamageTaken = new UnityEvent<DamageTakenArgs>();


    public float CurrentHealth;
    [SerializeField]
    public float MaxHealth;


    int _countPerFrame;
    int _lastCountPerFrame;

    void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    void FixedUpdate()
    {
        _lastCountPerFrame = _countPerFrame;
        _countPerFrame = 0;
    }

    public void UpdateMaxHealth(float value)
    {
        MaxHealth = value;
        CurrentHealth = value;
    }

    public void TakeDamage(float dmg)
    {
        var prevHealth = CurrentHealth;
        CurrentHealth -= dmg;

        DamageTaken?.Invoke(new DamageTakenArgs
        {
            CurrentRatio = CurrentHealth / MaxHealth,
        });

        if (prevHealth > 0 && CurrentHealth <= 0)
        {
            Died?.Invoke();
        }

        _countPerFrame += 1;
    }

}

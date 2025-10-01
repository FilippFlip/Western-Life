using System;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public float minimumDamagePeriod = 0.25f;
    private StatsHandler statsHandler;
    private float timer;
    private bool canDamage = true;
    private void Start()
    {
        statsHandler = GetComponentInParent<StatsHandler>();
    }

    private void Update()
    {
        if (canDamage == false)
        {
            timer += Time.deltaTime;
        }
        if (timer >= minimumDamagePeriod)
        {
            timer = 0;
            canDamage = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StatsHandler stats))
        {
            if (statsHandler.fraction != stats.fraction  && canDamage)
            {
                stats.ChangeHealth(-25);
                canDamage = false;
            }
        }
    }
}

public enum Fraction
{
    Friendly,
    Enemy,
    Neutral,
}

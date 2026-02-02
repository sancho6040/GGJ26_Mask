using UnityEngine;

public class BaseAbility : ScriptableObject
{
    public new string Name;
    public float CooldpwnTime;
    public float ActiveTime;

    public virtual void Activate(GameObject parent) { }
    public virtual void BeginCooldown(GameObject parent) { }

}

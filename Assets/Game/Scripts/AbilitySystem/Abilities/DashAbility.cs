using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Create Abilities/Dash")]
public class DashAbility : BaseAbility
{
    public float dashVelocity;

    public override void Activate(GameObject parent)
    {
        //PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        //movement.acceleration = dashVelocity;
    }

    public override void BeginCooldown(GameObject parent)
    {
        //PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        //movement.acceleration = movement.normalAcceleration;
    }
}

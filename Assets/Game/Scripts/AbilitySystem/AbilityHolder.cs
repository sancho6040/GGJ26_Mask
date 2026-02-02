using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public BaseAbility Ability;
    private float _cooldownTime;
    private float _activeTime;

    enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }

    private AbilityState _state = AbilityState.Ready;

    public KeyCode key;

    private void Update()
    {
        switch (_state)
        {
            case AbilityState.Ready:
                if (Input.GetKeyDown(key))
                {
                    //Activate
                    Ability.Activate(gameObject);
                    _state = AbilityState.Active;
                    _activeTime = Ability.ActiveTime;
                }
                break;
            case AbilityState.Active:
                if (_activeTime > 0)
                {
                    _activeTime -= Time.deltaTime;
                }
                else
                {
                    Ability.BeginCooldown(gameObject);
                    _state = AbilityState.Cooldown;
                    _cooldownTime = Ability.CooldpwnTime;
                }
                break;
            case AbilityState.Cooldown:
                if (_cooldownTime > 0)
                {
                    _cooldownTime -= Time.deltaTime;
                }
                else
                {
                    _state = AbilityState.Ready;
                    _cooldownTime = 0;
                    _activeTime = 0;
                }
                break;
        }

    }

}

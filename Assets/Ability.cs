using UnityEngine;

public abstract class Ability
{
    private bool active = true;
    public void TryActivate(Vector3 position)
    {
        if (active)
            Activate(position);
        active = false;

    }

    protected abstract void Activate(Vector3 position);
    public abstract string GetName();
}

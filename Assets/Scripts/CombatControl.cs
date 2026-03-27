using UnityEngine;

public abstract class CombatControl : MonoBehaviour
{
    public virtual void SomethingPolymorphismHurt(object hurtObj) { }
    public virtual object SomethingPolymorphismHit() { return null; }
}

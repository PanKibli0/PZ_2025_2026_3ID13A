using UnityEngine;

public class CombatControlTestTrigger : CombatControl
{
    public int dmgval = 1;
    public override object SomethingPolymorphismHit() { return dmgval; }
}

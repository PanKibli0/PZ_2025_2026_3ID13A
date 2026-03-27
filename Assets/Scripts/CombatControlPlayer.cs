using UnityEngine;
[RequireComponent(typeof(PlayerControl))]
public class CombatControlPlayer : CombatControl
{
    private PlayerControl PlayerControl;
    private void Start()
    {
        PlayerControl = GetComponent<PlayerControl>();
    }
    public override void SomethingPolymorphismHurt(object dmgObj)
    {
        if(dmgObj is int dmg)
        {
            PlayerControl.takeDmg(dmg);
            if(dmg>0)
                Debug.Log("AUAUA");
            if (dmg < 0)
                Debug.Log("Much obliged.");
            if (dmg == 0)
                Debug.Log("WHY THE F WOULD YOU MAKE A HITBOX ATTACK THAT DEALS ZERO DMG? WHYEVEN? ITS A PROTOTYPE, YOU DONT EVEN AADD ANY SLOW/DEBUFF/ANYTHING!?");
        }
    }
}

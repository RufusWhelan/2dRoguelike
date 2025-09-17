using UnityEngine;

public class BaseEffect : MonoBehaviour
{
    private bool hasGlow; //sends out a wave of light
    private bool hasVoid; //delays damage dealt but increases damage
    private bool hasBleed; //makes each hit deal more damage but it wears off quickly and deals less damage by default
    private bool hasShock; //chains damage to one more enemy
    private bool hasFreeze; //briefly slows enemies
    private bool hasBurn; // damage overtime - strong but fast
    private bool hasWeakness; // reduces enemy damage
    private bool hasShatter; //crits briefly stun enemies

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

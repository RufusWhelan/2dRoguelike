using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponParentClass : MonoBehaviour
{
    public Weapon currentWeapon;
    [SerializeField] private Collider2D hitbox;
    [SerializeField] private float attackSwing;
    [SerializeField] private float attackDistance;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackDuration;

    [System.Serializable]
    public class Weapon
    {
        protected float attackSwing; //how wide the attack is
        protected float attackDistance; //how far the attack reaches (like spear goes out more)
        protected float attackDamage;
        protected float attackDuration;
        protected float attackCooldown;
        protected bool isAttacking = false;
        protected bool canAttack = true;
        public Weapon(float swing, float distance, float damage, float cooldown, bool isAttacking, bool canAttack)
        {
            this.attackSwing = swing;
            this.attackDistance = distance;
            this.attackDamage = damage;
            this.attackCooldown = cooldown;
            this.isAttacking = isAttacking;
            this.canAttack = canAttack;

        }
    }
}
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponParentClass : MonoBehaviour
{
    public playerInputScript inputScript;
    public Weapon currentWeapon;
    [SerializeField] private Collider2D hitbox;
    [SerializeField] private float weaponLength;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackDuration;
    public int weaponType;

    [System.Serializable]
    public class Weapon
    {
        protected float weaponLength;
        protected float attackDamage;
        protected float attackDuration;
        protected float attackCooldown;
        protected bool isAttacking = false;
        protected bool canAttack = true;
        public Weapon(float length, float damage, float cooldown, bool isAttacking, bool canAttack)
        {
            this.weaponLength = length;
            this.attackDamage = damage;
            this.attackCooldown = cooldown;
            this.isAttacking = isAttacking;
            this.canAttack = canAttack;

        }

        public void attack()
        {
        }
    }

    public class Sword : Weapon
    {
        public Sword(float length, float damage, float cooldown, bool isAttacking, bool canAttack) : base(length, damage, cooldown, isAttacking, canAttack)
        {

        }

    }

    public class Spear : Weapon
    {
        public Spear(float length, float damage, float cooldown, bool isAttacking, bool canAttack) : base(length, damage, cooldown, isAttacking, canAttack)
        {

        }

    }

    public class Gun : Weapon
    {
        public Gun(float length, float damage, float cooldown, bool isAttacking, bool canAttack) : base(length, damage, cooldown, isAttacking, canAttack)
        {

        }

    }

    private void OCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            if (!inputScript.attackInput) return;
            currentWeapon.attack();
        }
    }
}
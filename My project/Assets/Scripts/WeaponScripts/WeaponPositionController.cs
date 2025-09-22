using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using UnityEngine.Rendering;

public class WeaponPositionController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public playerInputScript InputScript;
    private Plane plane = new Plane(Vector3.forward, new Vector3(0f, 0f, 0f)); //2d plane infront of the camera on 0,0,0
    [SerializeField] private float attackCooldown;
    private bool canAttack = true;
    [SerializeField] private float swingTime;
    [SerializeField] private float attackSwing;
    [SerializeField] private float breakSwing;
    [SerializeField] private bool isSwinging;
    [SerializeField] private Transform attackPivot;
    private Quaternion targetRotation;
    private float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        RotateWeapon();
    }

    void FixedUpdate()
    {
        if (InputScript.attackInput && canAttack)
        {
            float angleDiff = Quaternion.Angle(transform.rotation, targetRotation);
            if (angleDiff < 5f)
            {
                StartCoroutine(onAttackInput());
            }
        }
    }

    void RotateWeapon()
    {
        if (isSwinging) return;
        Ray mouseRay = Camera.main.ScreenPointToRay(InputScript.mousePosition);

        float distance;
        if (plane.Raycast(mouseRay, out distance))
        {
            Vector3 hitPoint = mouseRay.GetPoint(distance);
            Vector3 direction = hitPoint - transform.position;

            float targetZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            targetRotation = Quaternion.Euler(0, 0, targetZ);

            if (!InputScript.attackInput) rotateSpeed = 15f;
            else rotateSpeed = 25f;

            if (attackPivot.localRotation != quaternion.Euler(0, 0, 0))
                attackPivot.localRotation = Quaternion.Lerp(attackPivot.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rotateSpeed);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }
    }

    IEnumerator onAttackInput()
    {
        InputScript.attackInput = false;
        isSwinging = true;
        canAttack = false;
        float startAttackAngle = attackSwing / 2;
        float endAttackAngle = -attackSwing / 2;

        float counter = 0;
        float startupAttackTime = 0.1f;
        Quaternion neutralRot = Quaternion.identity;
        Quaternion startupRot = Quaternion.Euler(0f, 0f, startAttackAngle);

        while (counter < startupAttackTime)
        {
            counter += Time.deltaTime;
            float t = counter / startupAttackTime;

            attackPivot.localRotation = Quaternion.Lerp(neutralRot, startupRot, t);
            yield return null;
        }

        counter = 0f;
        while (counter < swingTime)
        {
            counter += Time.deltaTime;
            float t = counter / swingTime;

            attackPivot.localRotation = Quaternion.Lerp(Quaternion.Euler(0f, 0f, startAttackAngle), Quaternion.Euler(0f, 0f, endAttackAngle), t);
            yield return null;

        }
        isSwinging = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
using UnityEngine;
using System.Collections;
using Unity.Mathematics;

public class WeaponPositionController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public playerInputScript InputScript;
    [SerializeField] private Transform attackPivot;
    private Plane plane = new Plane(Vector3.forward, new Vector3(0f, 0f, 0f)); //2d plane infront of the camera on 0,0,0
    [SerializeField] private float swingTime;
    [SerializeField] private float attackSwingAngle;
    [SerializeField] private float breakSwingAngle;
    [SerializeField] private bool isSwinging;

    private bool lockedRotSet = false;
    private Quaternion lockedRot;
    private Quaternion targetRotation;
    private float rotateSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        RotateWeapon();
    }

    void FixedUpdate()
    {
        float angleDiff = Quaternion.Angle(transform.rotation, lockedRot);
        if (InputScript.attackInput && !isSwinging)
        {
            if (!lockedRotSet)
            {
                lockedRotSet = true;
                lockedRot = targetRotation; //ensures that the players attack goes off where they left clicked. if statement is necessary so that set locked rotation doesn't get overwritten.
            }

            if (angleDiff < 10)
            {
                StartCoroutine(onAttackInput());
            }
        }
        if (angleDiff > 120) //temporary fix for bug where player input does not get read if you move to quickly
        {
            lockedRotSet = false;
        }
    }

    void RotateWeapon()
    {
        if (isSwinging) return; //if the player is attacking do not rotate
        Ray mouseRay = Camera.main.ScreenPointToRay(InputScript.mousePosition);

        float distance;
        if (plane.Raycast(mouseRay, out distance))
        {
            Vector3 hitPoint = mouseRay.GetPoint(distance);
            Vector3 direction = hitPoint - transform.position; //finds the mouses position on the 2d plane relative to players position

            float targetZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            targetRotation = Quaternion.Euler(0, 0, targetZ);


            if (attackPivot.localRotation != quaternion.Euler(0, 0, 0))
                attackPivot.localRotation = Quaternion.Lerp(attackPivot.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rotateSpeed);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }
    }
    
    IEnumerator onAttackInput()
    {
        InputScript.attackInput = false;
        isSwinging = true;

        float startAttackAngle = attackSwingAngle / 2;
        float endAttackAngle = -attackSwingAngle / 2;

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

            attackPivot.localRotation = Quaternion.Lerp(Quaternion.Euler(0f,0f,startAttackAngle), Quaternion.Euler(0f,0f,endAttackAngle), t);

            yield return null;
        }
        lockedRotSet = false;
        isSwinging = false;
    }
}

using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using UnityEngine.Rendering;

public class WeaponPositionController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public playerInputScript InputScript;
    private Plane plane = new Plane(Vector3.forward, new Vector3(0f, 0f, 0f)); //2d plane infront of the camera on 0,0,0
    [SerializeField] private float swingTime;
    [SerializeField] private float attackSwing;
    [SerializeField] private float breakSwing;
    [SerializeField] private bool isSwinging;
    [SerializeField] private Transform attackPivot;

    // Update is called once per frame
    void Update()
    {
        RotateWeapon();
    }

    void FixedUpdate()
    {
        if (InputScript.attackInput && !isSwinging) StartCoroutine(onAttackInput());
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
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetZ);

            float rotateSpeed = 10f;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }
    }
    
    IEnumerator onAttackInput()
    {
        InputScript.attackInput = false;
        isSwinging = true;
        float elapsed = 0;
        float startAngle = attackSwing / 2;
        float endAngle = -attackSwing / 2;

        float startupTime = 0.05f; // tweak for anticipation
        Quaternion neutralRot = Quaternion.identity;
        Quaternion startupRot = Quaternion.Euler(0f, 0f, startAngle);

        while (elapsed < startupTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / startupTime;

            attackPivot.localRotation = Quaternion.Lerp(neutralRot, startupRot, t);

            yield return null;
        }

        elapsed = 0f;
        while (elapsed < swingTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / swingTime;

            attackPivot.localRotation = Quaternion.Lerp(Quaternion.Euler(0f,0f,startAngle), Quaternion.Euler(0f,0f,endAngle), t);

            yield return null;
        }


        elapsed = 0f;
        float returnTime = 0.1f;
        Quaternion startRot = attackPivot.localRotation;
        Quaternion targetRot = Quaternion.identity;

        while (elapsed < returnTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / returnTime;

            attackPivot.localRotation = Quaternion.Lerp(startRot, targetRot, t);

            yield return null;
        }
        isSwinging = false;
    }
}

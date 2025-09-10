using Unity.Mathematics;
using UnityEngine;

public class WeaponPositionController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public playerInputScript InputScript;

    // Update is called once per frame
    void Update()
    {
        RotateWeapon();
    } 

    void RotateWeapon()
    {
        Vector3 rotation = InputScript.mousePosition - transform.position;
        float zRot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0,0,zRot);
    } 
}

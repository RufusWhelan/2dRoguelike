using UnityEngine;
using System.Collections.Generic;


public class WeaponControllerScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> cellPrefabs; // WeaponAttachmentTypes
    [SerializeField] private float bodyLength = 1f; // length as a body part of the blade
    [SerializeField] private GameObject handleGameobject;
    [SerializeField] private Transform handle;

    [SerializeField] private List<GameObject> cells = new List<GameObject>(); //Current WeaponCells

    public void Start()
    {
        cells.Add(handleGameobject);
    }
    public void AddCell(int cellIndex)
    {

        if (cells.Count > 1) // Checks if any attachments exist
        {
            setToBody(cells[cells.Count - 1]);
        }

        float newY = cells.Count * bodyLength;
        Vector3 localPos = new Vector3(0f, newY, 0f); // position of new cell relative to handle to ensure nothing freaky happens with rotations
        GameObject newCell = Instantiate(cellPrefabs[cellIndex], handle); //spawns the new cell of cellIndex type off of the handle

        newCell.transform.localPosition = localPos;
        setToTip(newCell);
        cells.Add(newCell); // adds the newcell to the list of cells.
    }

    private void setToTip(GameObject cell)
    {
        Transform tip = cell.transform.Find("asTip");
        Transform body = cell.transform.Find("asBody");

        tip.gameObject.SetActive(true);
        body.gameObject.SetActive(false);
    }
    private void setToBody(GameObject cell)
    {
        Transform tip = cell.transform.Find("asTip");
        Transform body = cell.transform.Find("asBody");

        tip.gameObject.SetActive(false);
        body.gameObject.SetActive(true);
    }
}

using UnityEngine;
using System.Collections.Generic;


public class WeaponControllerScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> segmentPrefabs; // WeaponAttachmentTypes
    [SerializeField] private float cellLength = 1f; // length as a body part of the blade
    [SerializeField] private float tipLength = 1.5f; // Length as tip of blade
    [SerializeField] private GameObject handleGameobject;
    [SerializeField] private Transform handle;

    [SerializeField] private List<GameObject> cells = new List<GameObject>(); //Current WeaponCells
 
    public void Start()
    {
        cells.Add(handleGameobject);
    }
    public void AddCell(int segmentIndex) 
    {

        if (cells.Count > 1) // Checks if any attachments exist
        {
            BoxCollider2D prevCol = cells[cells.Count - 1].GetComponent<BoxCollider2D>();
            prevCol.size = new Vector2(prevCol.size.x, cellLength); //turns the collider from a tip to a cell
            prevCol.offset = new Vector2(0f, 0f); // unity extends objects in both directions when they are made longer, this makes it so the object extends only upwards
        }

        float newY = cells.Count * cellLength;
        Vector3 localPos = new Vector3(0f, newY, 0f);
        // position of new cell relative to handle to ensure nothing freaky happens with rotations

        GameObject newCell = Instantiate(segmentPrefabs[segmentIndex], handle); //spawns the new cell off of the handle
        newCell.transform.localPosition = localPos;

        BoxCollider2D newCol = newCell.GetComponent<BoxCollider2D>();
        newCol.size = new Vector2(newCol.size.x, tipLength); //sets the new cells position
        newCol.offset = new Vector2(0f, cellLength/4);

        cells.Add(newCell); // adds the newcell to the list of cells.
    }
}

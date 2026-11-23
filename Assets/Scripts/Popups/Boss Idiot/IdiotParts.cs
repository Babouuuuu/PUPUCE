using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class IdiotParts : MonoBehaviour
{
    public bool isHead = false;

    public YouAreAnIdiot youAreAnIdiot;
    [SerializeField] private GameObject idiotHead;
    [SerializeField] private int followDistance;
    private List<Vector2> storedPositions;


    void Awake()
    {
        storedPositions = new List<Vector2>(); //create a blank list
        
        if (idiotHead == null)
        {
            idiotHead = FindAnyObjectByType<YouAreAnIdiot>().gameObject;
        }
    }

    void LateUpdate()
    {
        if (idiotHead == null)
        {
            youAreAnIdiot.enabled = true;
            this.enabled = false;
            return;
        }

        Vector2 idiotPos = idiotHead.transform.position;

        if (storedPositions.Count == 0) //check if the list is empty
        {
            storedPositions.Add(idiotPos); //store the players currect position
            return;
        }
        else if (storedPositions[storedPositions.Count - 1] != idiotPos)
        {
            storedPositions.Add(idiotPos); //store the position every frame
        }

        else if(transform.position.y != idiotPos.y) //check if the ally is airborne and correct it if necessary
        {
            storedPositions.Add(idiotPos);
        }

        if (storedPositions.Count > followDistance)
        {
            transform.position = storedPositions[0]; //move
            storedPositions.RemoveAt(0); //delete the position that player just move to
        }
        
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int Width;
    public int Height;
    public int X;
    public int Y;

    private bool updatedDoors = false;

    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    public List<Door> doors = new List<Door>();

    // Start is called before the first frame update
    // If the wrong scene is played then there will be a debug log message, the roomcontroller is only on the basementmain scene.
    void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.Log("You pressed play in the wrong scene!");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>(); 
        foreach(Door d in ds)
        {
            doors.Add(d);
            switch(d.doorType)
            {
                case Door.DoorType.right:
                rightDoor = d;
                break;
                case Door.DoorType.left:
                leftDoor = d;
                break;
                case Door.DoorType.top:
                topDoor = d;
                break;
                case Door.DoorType.bottom:
                bottomDoor = d;
                break;
            }
        }

        RoomController.instance.RegisterRoom(this);
    }

    //If the Boss Room is spawned and the doors are not updated (removed) then this will run and remove them.
    void Update()
    {
        if(name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }
    }


    //This removes door objects but keeps the colliders, to stop players from walking through walls.
    public void RemoveUnconnectedDoors()
    {
        Debug.Log("removing doors");
        foreach(Door door in doors)
        {
            switch(door.doorType)
            {
                case Door.DoorType.right:
                    if(GetRight() == null)
                        door.gameObject.SetActive(false);
                    door.doorCollider.SetActive(true);
                    break;
                case Door.DoorType.left:
                    if(GetLeft() == null)
                        door.gameObject.SetActive(false);
                    door.doorCollider.SetActive(true);
                    break;
                case Door.DoorType.top:
                    if(GetTop() == null)
                        door.gameObject.SetActive(false);
                    door.doorCollider.SetActive(true);
                    break;
                case Door.DoorType.bottom:
                if(GetBottom() == null)
                        door.gameObject.SetActive(false);
                    door.doorCollider.SetActive(true);
                    break;
            }
        }
    }

    //Getting location of rooms left, right, up or down from each instance. This allows for the right doors to be removed if there is not any rooms to the right for example.
    public Room GetRight()
    {
        if(RoomController.instance.DoesRoomExist(X + 1, Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    }
    public Room GetLeft()
    {
        if(RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }
    public Room GetTop()
    {
        if(RoomController.instance.DoesRoomExist(X, Y + 1))
        {
            return RoomController.instance.FindRoom(X, Y + 1);
        }
        return null;
    }
    public Room GetBottom()
    {
        if(RoomController.instance.DoesRoomExist(X, Y - 1))
        {
            return RoomController.instance.FindRoom(X, Y - 1);
        }
        return null;
    }

    //Draws a rectangle around each floor, this allows the calculations to be made for the procedural dungeon generation.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }

    //Gets the centre of the room
    public Vector3 GetRoomCentre()
    {
        return new Vector3( X * Width, Y * Height);
    }

    //Triggers room functions when the player enters the room
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
}

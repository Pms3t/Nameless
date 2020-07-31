using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageDoors : MonoBehaviour
{
    [SerializeField] private GameObject[] _manageableDoors;

    Animator anim;

    public void CloseDoors()
    {
        foreach (GameObject door in _manageableDoors)
        {
            // Get animator component from the game object and change value of Open boolean to false.
            anim = door.GetComponent<Animator>();
            anim.SetBool("Open", false);
        }
    }

    public void OpenSingleDoor(GameObject door)
    {
        anim = door.GetComponent<Animator>();
        anim.SetBool("Open", true);
    }

    public GameObject GetDoor(int index)
    {
        return _manageableDoors[index];
    }
}

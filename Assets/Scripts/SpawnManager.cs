// 24.59
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 24.59 
public class SpawnManager : MonoBehaviour
{
    // 24.59 
    public HandVisual RightHand;
    // 24.59 
    public HandVisual LeftHand;

    // 24.59 position of spawn right hand
    public Transform RightIndexTip;
    // 24.59 position of spawn left hand
    public Transform LeftIndexTip;

    // 24.59 prefab that will be spawned
    public GameObject SpawnObject;

    // 24.59 contains all prefabs spawned
    GameObject ObjectContainer;

    // 24.59 if we dont control the spawning of objects with a boolean
    // 24.59 we will have infinite spawning of objects
    // 24.59 
    bool flagR = true;
    // 24.59 
    bool flagL = true;
    // 24.60
    bool toggleG = true;

    // Start is called before the first frame update
    void Start()
    {
        // 24.59 
        ObjectContainer = new GameObject("ObjectContainer");
    }

    // Update is called once per frame
    void Update()
    {
        // 24.59 
        if (RightHand.Hand.IsTrackedDataValid)
        {
            // 24.59 
            if (RightHand.Hand.GetIndexFingerIsPinching() && flagR)
            {
                // 24.59 
                SpawnRight(RightIndexTip);
                // 24.59 no more objects will be spawned
                flagR = false;
            }
            // 24.59 
            else if (!RightHand.Hand.GetIndexFingerIsPinching())
            {
                // 24.59 when fingers are apart
                flagR = true;
            }
        }

        // 24.59 
        if (LeftHand.Hand.IsTrackedDataValid)
        {
            // 24.59 
            if (LeftHand.Hand.GetIndexFingerIsPinching() && flagL)
            {
                // 24.59 
                SpawnLeft(LeftIndexTip);
                // 24.59 no more objects will be spawned
                flagL = false;
            }
            // 24.59 
            else if (!LeftHand.Hand.GetIndexFingerIsPinching())
            {
                // 24.59 when fingers are apart
                flagL = true;
            }
        }
    }

    // 24.59 spawnPoint is the index
    void SpawnRight(Transform spawnPoint)
    {
        // 24.59 reference for the respective object in prefab
        GameObject tempObject;
        // 24.59 parameters object, position , orientation
        tempObject = Instantiate(SpawnObject, spawnPoint.position, Quaternion.identity);
        // 24.59 randomly changing color and properties assigned are:
        // 24.59 min max so 0 and 1, saturation full so min and max saturation as 1, value as
        // 24.59 0.5f and 1 so that the colors are not that darki
        tempObject.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0, 1, 1, 1, 0.5f, 1);
        tempObject.GetComponent<Rigidbody>().isKinematic = toggleG;
        // 24.59 making it a child of object container transform parent object like its child
        tempObject.transform.SetParent(ObjectContainer.transform);
    }

    // 24.59
    void SpawnLeft(Transform spawnPoint)
    {
        // 24.59 reference for the respective object in prefab
        GameObject tempObject;
        // 24.59 parameters object, position , orientation
        tempObject = Instantiate(SpawnObject, spawnPoint.position, Quaternion.identity);
        // 24.59 randomly changing color and properties assigned are:
        // 24.59 min max so 0 and 1, saturation full so min and max saturation as 1, value as
        // 24.59 0.5f and 1 so that the colors are not that darki
        tempObject.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0, 1, 1, 1, 0.5f, 1);
        tempObject.GetComponent<Rigidbody>().isKinematic = toggleG;
        // 24.59 making it a child of object container transform parent object like its child
        tempObject.transform.SetParent(ObjectContainer.transform);
    }

    // 24.59 here we will need prefab not material
    public void changePrefab(GameObject prefab)
    {
        // 24.59
        SpawnObject = prefab;
    }

    // 24.59 
    public void Undo()
    {
        // 24.59 
        if (ObjectContainer!=null && (ObjectContainer.transform.childCount!=0))
            // 24.59 
            Destroy((ObjectContainer.transform.GetChild(ObjectContainer.transform.childCount - 1)).gameObject);
    }

    // 24.59 
    public void EraseAll()
    {
        // 24.59 
        Destroy(ObjectContainer);
        // 24.59 
        ObjectContainer = new GameObject("ObjectContainer");
    }

    // 24.60
    public void toggleGravity()
    {
        // 24.60 rigidbody isKinematic enable disable done with this
        toggleG = !toggleG;
        // 24.60 
        for (int i = 0; i < ObjectContainer.transform.childCount; i++)
        {
            // 24.60
            ObjectContainer.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = toggleG;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{

    public void CombineMeshes()
    {
        //Array of meshes that are a child of game object
        MeshFilter[] filters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combiners = new CombineInstance[filters.Length];

        //Declare position info
        Quaternion oldRot = transform.rotation;
        Vector3 oldPos = transform.position;

        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;
        //Log number of meshes
        Debug.Log(name + " is combining " + filters.Length + " meshes");

        //declare the final mesh as a mesh for the new game object
        Mesh finalMesh = new Mesh();


        //for each mesh in game object (set in scene as 'Gun')
        for (int a = 0; a < filters.Length; a++)
        {
            if (filters[a].transform == transform) // do not combine its own mesh with itself
                continue;

            combiners[a].subMeshIndex = 0;
            //pass the mesh position
            combiners[a].mesh = filters[a].sharedMesh;
            combiners[a].transform = filters[a].transform.localToWorldMatrix;
        }

        finalMesh.CombineMeshes(combiners);
        //shared mesh is the asset mesh
        //mesh cant be used in editor
        GetComponent<MeshFilter>().sharedMesh = finalMesh;

        //move to original position
        transform.rotation = oldRot;
        transform.position = oldPos;

        //deletes the children
        var childCount = transform.childCount;
        foreach (Transform child in transform.Cast<Transform>().ToArray())
        {
            DestroyImmediate(child.gameObject);
        }

        //for (int a = 0; a < childCount; a++)
        //{
        //   // DestroyImmediate(transform.GetChild(a).gameObject);
        //    //hide game object
        //    transform.GetChild(a).gameObject.SetActive(false);
        //   // childCount = childCount - 1;

        //}

    }
}

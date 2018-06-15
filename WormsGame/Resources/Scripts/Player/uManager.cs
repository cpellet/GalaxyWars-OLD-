using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class uManager : NetworkBehaviour
{
    // we need a nice script here.
    // this is where the server communicates with the player for the first time, this object will never get deleted unless the player leaves.


    void Start()
    {
        if (isServer)
        {
            GameObject myObject = (GameObject)GameObject.Instantiate(Resources.Load(@"Prefabs/Player/(SCRIPT SPAWN) Player"), Vector3.zero, Quaternion.identity);

            NetworkServer.SpawnWithClientAuthority(myObject, connectionToClient);
            
            myObject.GetComponent<uController>().Owner = 0;
        }
    }
}

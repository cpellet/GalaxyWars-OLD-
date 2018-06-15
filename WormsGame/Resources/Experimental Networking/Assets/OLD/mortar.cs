// using UnityEngine;
// using UnityEngine.Networking;

// public class mortar : NetworkBehaviour
// {
//     public void Update()
//     {
//         if (!hasAuthority || (isServer && GetComponent<NetworkIdentity>().clientAuthorityOwner == null))
//             return;

//         Debug.LogError("this mortar is controlled by us!");
//     }
// }




    // if (Input.GetKeyDown(KeyCode.E))
    // {
    //     Debug.Log("ATTEMPTING TO TAKE OWNERSHIP OF THE TEST MORTAR");
    //     Cmd_GETOWNER();
    // }

    // [Command]
    // public void Cmd_GETOWNER()
    // {
    //     GameObject.Find("MORTAR").GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    // }
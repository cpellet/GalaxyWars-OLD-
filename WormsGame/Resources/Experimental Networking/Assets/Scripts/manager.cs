using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using uDoil;

public class manager : NetworkManager
{
    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);
        foreach (KeyValuePair<NetworkInstanceId, NetworkIdentity> objects in NetworkServer.objects)
        {
            ObjectPosition targetedObject = NetworkServer.FindLocalObject(objects.Key)?.GetComponent<ObjectPosition>();
            if (targetedObject != null)
            {
                targetedObject.Target_SyncObject(conn, targetedObject.transform.name, targetedObject.transform.position);
            }
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        List<NetworkInstanceId> objects = conn?.clientOwnedObjects?.ToList();

        if (objects != null)
        {
            foreach (var obj in objects)
            {
                GameObject target = NetworkServer.FindLocalObject(obj);
                if (target != null && conn.playerControllers.Where(x => x.gameObject == target).FirstOrDefault() == null)
                {
                    target.GetComponent<NetworkIdentity>().RemoveClientAuthority(conn);
                    Debug.Log($"{target.name} is no longer owned");
                }
            }
        }
        NetworkServer.DestroyPlayersForConnection(conn);
    }

    public override void OnStopHost()
    {
        base.OnStopHost();
        ClientScene.DestroyAllClientObjects();
        ClientScene.localPlayers.Clear();
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        ClientScene.DestroyAllClientObjects();
        ClientScene.localPlayers.Clear();
    }

    private Dictionary<int, float> playerLastPressed = new Dictionary<int, float>();
    
    public void Update()
    {
        if (client == null)
            return;

        for (int player = 0; player < Input.GetJoystickNames().Length; player++)
        {
            bool hasObject = ClientScene.localPlayers.Where(x => x.playerControllerId == player).FirstOrDefault() != null;

            if (player.GetButtonDown("R_SELECT"))
            {
                if (!hasObject)
                    ClientScene.AddPlayer(client.connection, (short)player);

                playerLastPressed.Add(player, Time.time);
            }

            if (!player.GetButton("R_SELECT") && playerLastPressed.ContainsKey(player))
            {
                playerLastPressed.Remove(player);
            }

            if (playerLastPressed.ContainsKey(player) && playerLastPressed[player] + 1 <= Time.time)
            {
                if (hasObject)
                    ClientScene.RemovePlayer((short)player);

                playerLastPressed.Remove(player);
            }
        }
    }
}
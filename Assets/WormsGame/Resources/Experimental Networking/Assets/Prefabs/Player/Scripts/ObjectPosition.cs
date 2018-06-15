using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class ObjectPosition : NetworkBehaviour
{
    private float tps = 15;
    private float curTps;
    private Vector3 moveDirection;
    private Vector3 smooth;

    public new Transform transform
    {
        get
        {
            for (int i = 0; i < gameObject.transform.Find("Models").childCount; i++)
            {
                if (gameObject.transform.Find("Models").GetChild(i) != null && gameObject.transform.Find("Models").GetChild(i).gameObject.activeSelf == true)
                {
                    return gameObject.transform.Find("Models").GetChild(i);
                }
            }
            Debug.LogError($"{this.gameObject.name} has no active models!");
            return null;
        }
    }

    public void Update()
    {
        if (transform == null)
            return;

        if (hasAuthority)
        {
            curTps += Time.deltaTime;

            if (curTps >= (1 / tps))
            {
                curTps -= (1 / tps);
                Cmd_SyncObject(transform.position, transform.rotation, transform.name);
            }
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, moveDirection, ref smooth, 0.08f);
        }
    }

    [Command]
    public void Cmd_SyncObject(Vector3 position, Quaternion direction, string activeModel)
    {
        moveDirection = position;
        transform.rotation = direction;
        Rpc_SyncObject(position, direction, activeModel);
    }

    [ClientRpc]
    private void Rpc_SyncObject(Vector3 position, Quaternion direction, string activeModel)
    {
        if (!isLocalPlayer)
        {
            moveDirection = position;
            transform.rotation = direction;
        }
    }

    [TargetRpc]
    public void Target_SyncObject(NetworkConnection target, string activeModel, Vector3 position)
    {
        for (int i = 0; i < gameObject.transform.Find("Models").childCount; i++)
        {
            if (gameObject.transform.Find("Models").GetChild(i).name == activeModel)
            {
                gameObject.transform.Find("Models").GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                gameObject.transform.Find("Models").GetChild(i).gameObject.SetActive(false);
            }
        }
        moveDirection = position;
    }
}
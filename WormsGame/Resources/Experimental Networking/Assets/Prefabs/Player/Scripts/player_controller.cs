using uDoil;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class player_controller : NetworkBehaviour
{
    [SerializeField] private float speed;
    private int owner => GetComponent<NetworkIdentity>().playerControllerId;
    private CharacterController controller => GetComponent<CharacterController>();
    private Vector3 direction;
    private float gravity = -0.1f;
    [SerializeField] private float gravityStrength = 2;

    public void Update()
    {
        if (!isLocalPlayer)
            return;

        direction = new Vector2(owner.GetAxis("L_VERTICAL"), owner.GetAxis("L_HORIZONTAL")).normalized;

        if (controller.isGrounded)
        {
            gravity = -0.1f;
        }

        gravity -= gravityStrength * Time.deltaTime;
        controller.Move(new Vector3(direction.x, gravity, direction.y) * speed * Time.deltaTime);
    }

    private void OnGUI()
    {
        if (isServer)
        {
            GUI.Label(new Rect(10, 10, 500, 50), $"Connections: {playerCount()} \nSlots used: {slotsInUse()}");
        }
    }

    public int slotsInUse()
    {
        int counter = 0;

        foreach (NetworkConnection ply in NetworkServer.connections)
        {
            if (ply != null)
                if (ply.clientOwnedObjects != null)
                    counter += Mathf.Clamp(ply.clientOwnedObjects.Count, 1, 4);
                else
                    counter += 1;
        }
        return counter;
    }

    public int playerCount()
    {
        return NetworkServer.connections.Count;
    }
}

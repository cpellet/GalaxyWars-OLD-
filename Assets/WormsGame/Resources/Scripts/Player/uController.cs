using UnityEngine;
using UnityEngine.Networking;
using uDoil;

public class uController : NetworkBehaviour
{
    private double tick;
    private double tps => 1 / 10;
    private Vector3 velocity;
    private Vector3 moveTo { get; set; }
    private Quaternion rotateTo { get; set; }

    private uMovement playerMovement;
    private uCamera playerCamera;

    private int _owner = -1;
    public int Owner
    {
        get
        {
            return _owner;
        }
        set
        {
            if (_owner == -1 || this.GetComponent<NetworkIdentity>().clientAuthorityOwner == null)
            {
                _owner = GetComponent<NetworkIdentity>().playerControllerId;
                Debug.Log("Set owner: " + _owner);
                if (isServer)
                {
                    Rpc_SetOwner(_owner);
                }
            }
        }
    }

    private void Start()
    {
        Owner = 0;
    }

    [ClientRpc]
    private void Rpc_SetOwner(int value)
    {
        Owner = value;

        Initialize();
    }

    private void Initialize()
    {
        if (hasAuthority)
        {
            transform.Find("camera").gameObject.SetActive(true);

            GetComponent<CharacterController>().enabled = true;

            playerMovement = GetComponent<uMovement>();
            playerMovement.enabled = true;

            playerCamera = GetComponent<uCamera>();
            playerCamera.enabled = true;
        }
    }


    private void Update()
    {
        if (hasAuthority)
        {
            Tick();

            ApplyInput();
        }
        else
        {
            ApplyPosition();
        }
    }


    private void ApplyInput()
    {
        playerMovement.Move(new Vector2(Owner.GetAxis("L_HORIZONTAL"), -Owner.GetAxis("L_VERTICAL")));

        playerCamera.RotateCamera(Owner.GetButton("L_BUTTON"), Owner.GetButton("R_BUTTON"));

        playerCamera.MoveCamera();

        playerCamera.ZoomCamera(Owner.GetButton("DPAD_UP"), Owner.GetButton("DPAD_DOWN"));

        playerCamera.AngleCamera(Owner.GetButton("DPAD_LEFT"), Owner.GetButton("DPAD_RIGHT"));

        playerMovement.Rotate(new Vector3(Owner.GetAxis("R_HORIZONTAL"), 0, -Owner.GetAxis("R_VERTICAL")));
    }


    private void ApplyPosition()
    {
        Vector3 pos = Vector3.SmoothDamp(transform.position, moveTo, ref velocity, 0.2f);

        transform.position = pos;

        Quaternion rot = Quaternion.RotateTowards(transform.rotation, rotateTo, 180 * Time.deltaTime);

        transform.rotation = rot;
    }


    private void Tick()
    {
        tick += Time.deltaTime;

        if (tick > tps)
        {
            Cmd_SetPosition(transform.position, transform.rotation);

            tick -= tps;
        }
    }


    [Command]
    private void Cmd_SetPosition(Vector3 position, Quaternion rotation)
    {
        moveTo = position;

        rotateTo = rotation;

        Rpc_SetPosition(position, rotation);
    }


    [ClientRpc]
    private void Rpc_SetPosition(Vector3 position, Quaternion rotation)
    {
        moveTo = position;

        rotateTo = rotation;
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class playerUsername : NetworkBehaviour
{
    public NetworkVariable<FixedString32Bytes> username = new NetworkVariable<FixedString32Bytes>(
    writePerm: NetworkVariableWritePermission.Owner,
    readPerm: NetworkVariableReadPermission.Everyone
    );

    public NetworkVariable<bool> positionAnnounced = new NetworkVariable<bool>(
    writePerm: NetworkVariableWritePermission.Server,
    readPerm: NetworkVariableReadPermission.Everyone
    );

    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner) return;
        username.Value = NetCodeUI.Instance.username;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

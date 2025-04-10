using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;
using Unity.Collections;

public class MultiplayerCarController : NetworkBehaviour
{

    public NetworkVariable<bool> canMove = new NetworkVariable<bool>(
    writePerm: NetworkVariableWritePermission.Owner,
    readPerm: NetworkVariableReadPermission.Everyone
    );

    public NetworkVariable<FixedString32Bytes> playerName = new NetworkVariable<FixedString32Bytes>(
    writePerm: NetworkVariableWritePermission.Owner,
    readPerm: NetworkVariableReadPermission.Everyone
    );

    [ClientRpc]
    public void AllowMovementOnClientsClientRpc()
    {
        if (IsOwner)
        {
            canMove.Value = true;
        }
    }

    private Rigidbody playerRB;
    public WheelColliders colliders;
    public WheelMeshes wheelMeshes;
    public WheelParticles wheelParticles;
    public float gasInput;
    public float brakeInput;
    public float steeringInput;
    public GameObject smokePrefab;
    public float motorPower;
    public float brakePower;
    public float slipAngle;
    public float speed;
    public AnimationCurve steeringCurve;

    public TMPro.TMP_InputField nameInput;

    //public Mybutton gasPedal;
    //public Mybutton brakePedal;
    //public Mybutton leftButton;
    //public Mybutton rightButton;

    public bool isDrifting1;
    public bool isDrifting2;
    public bool isDrifting3;
    public bool isDrifting4;

    public float driftCounter;
    public float driftPoints;

    //public exp expManager;
    //public multiplierManager multManager;
    //public driftTextManager driftTextManager;

    //public EngineAudioScript carEngineAudioScript;

    public bool EnableVR;

    // Start is called before the first frame update
    void Start()
    {
        if (NetworkManager.Singleton != null && IsOwner && !IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += (clientId) =>
            {
                if (clientId == NetworkManager.Singleton.LocalClientId)
                {
                    Debug.Log("Disconnected from host!");
                    HandleHostDisconnect();
                }
            };
        }

        //carEngineAudioScript = transform.gameObject.GetComponent<EngineAudioScript>();
        InstantiateSmoke();

        if (!IsOwner)
        {
            return;
        }

        //playerName.Value = nameInput.text;
        //nameInput.gameObject.SetActive(false);

        wheelParticles.RRWheel.Play();
        wheelParticles.RLWheel.Play();
        wheelParticles.RRWheel.emissionRate = 0;
        wheelParticles.RLWheel.emissionRate = 0;

        playerRB = gameObject.GetComponent<Rigidbody>();
        gameObject.transform.rotation = Quaternion.Euler(0,180,0);

        GameObject smartCam = GameObject.Find("==== Default Stuff ====/CM vcam1");
        CinemachineVirtualCamera virtualCam = smartCam.GetComponent<CinemachineVirtualCamera>();
        virtualCam.Follow = transform;
        virtualCam.LookAt = transform;

        int rng = Random.Range(150, 160);
        transform.position = new Vector3(rng, 2, 20);
        //transform.rotation = Quaternion.Euler(0,160,0);
    }

    private void HandleHostDisconnect()
    {
        NetworkManager.Singleton.Shutdown();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    void InstantiateSmoke()
    {
        wheelParticles.FRWheel = Instantiate(smokePrefab, colliders.FRWheel.transform.position-Vector3.up*colliders.FRWheel.radius, Quaternion.identity, colliders.FRWheel.transform)
            .GetComponent<ParticleSystem>();
        wheelParticles.FLWheel = Instantiate(smokePrefab, colliders.FLWheel.transform.position- Vector3.up * colliders.FRWheel.radius, Quaternion.identity, colliders.FLWheel.transform)
            .GetComponent<ParticleSystem>();
        wheelParticles.RRWheel = Instantiate(smokePrefab, colliders.RRWheel.transform.position- Vector3.up * colliders.FRWheel.radius, Quaternion.identity, colliders.RRWheel.transform)
            .GetComponent<ParticleSystem>();
        wheelParticles.RLWheel = Instantiate(smokePrefab, colliders.RLWheel.transform.position- Vector3.up * colliders.FRWheel.radius, Quaternion.identity, colliders.RLWheel.transform)
            .GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (!IsOwner) return;
        if (canMove.Value == false) return;
        

        speed = playerRB.velocity.magnitude;
        CheckInput();
        ApplyMotor();
        ApplySteering();
        ApplyBrake();
        CheckParticles();
        ApplyWheelPositions();
        //Debug.Log("driving");
    }

    void CheckInput()
    {
        if (EnableVR)
        {
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            {
                gasInput = 1;
            }
            else if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
            {
                gasInput = -1;
            }
            else
            {
                gasInput = 0;
            }

            steeringInput = Input.GetAxis("Horizontal");
            slipAngle = Vector3.Angle(transform.forward, playerRB.velocity - transform.forward);

            float movingDirection = Vector3.Dot(transform.forward, playerRB.velocity);
            if (movingDirection < -0.5f && gasInput > 0)
            {
                brakeInput = Mathf.Abs(gasInput);
            }
            else if (movingDirection > 0.5f && gasInput < 0)
            {
                brakeInput = Mathf.Abs(gasInput);
            }
            else
            {
                brakeInput = 0;
            }
        }
        else
        {
            gasInput = Input.GetAxis("Vertical");
            steeringInput = Input.GetAxis("Horizontal");
            slipAngle = Vector3.Angle(transform.forward, playerRB.velocity - transform.forward);

            float movingDirection = Vector3.Dot(transform.forward, playerRB.velocity);
            if (movingDirection < -0.5f && gasInput > 0)
            {
                brakeInput = Mathf.Abs(gasInput);
            }
            else if (movingDirection > 0.5f && gasInput < 0)
            {
                brakeInput = Mathf.Abs(gasInput);
            }
            else
            {
                brakeInput = 0;
            }
        }

    }
    void ApplyBrake()
    {
        colliders.FRWheel.brakeTorque = brakeInput * brakePower* 0.7f ;
        colliders.FLWheel.brakeTorque = brakeInput * brakePower * 0.7f;

        colliders.RRWheel.brakeTorque = brakeInput * brakePower * 0.3f;
        colliders.RLWheel.brakeTorque = brakeInput * brakePower *0.3f;
    }
    void ApplyMotor() {

        colliders.RRWheel.motorTorque = motorPower * gasInput;
        colliders.RLWheel.motorTorque = motorPower * gasInput;
    }
    void ApplySteering()
    {

        float steeringAngle = steeringInput*steeringCurve.Evaluate(speed);
        if (slipAngle < 120f)
        {
            steeringAngle += Vector3.SignedAngle(transform.forward, playerRB.velocity + transform.forward, Vector3.up);
        }
        steeringAngle = Mathf.Clamp(steeringAngle, -90f, 90f);
        colliders.FRWheel.steerAngle = steeringAngle;
        colliders.FLWheel.steerAngle = steeringAngle;
    }

    void ApplyWheelPositions()
    {
        UpdateWheel(colliders.FRWheel, wheelMeshes.FRWheel);
        UpdateWheel(colliders.FLWheel, wheelMeshes.FLWheel);
        UpdateWheel(colliders.RRWheel, wheelMeshes.RRWheel);
        UpdateWheel(colliders.RLWheel, wheelMeshes.RLWheel);
    }
    void CheckParticles() {
        WheelHit[] wheelHits = new WheelHit[4];
        colliders.FRWheel.GetGroundHit(out wheelHits[0]);
        colliders.FLWheel.GetGroundHit(out wheelHits[1]);

        colliders.RRWheel.GetGroundHit(out wheelHits[2]);
        colliders.RLWheel.GetGroundHit(out wheelHits[3]);

        float slipAllowance = 1.4f;
        
        //if ((Mathf.Abs(wheelHits[0].sidewaysSlip) + Mathf.Abs(wheelHits[0].forwardSlip) > slipAllowance)){
        //    wheelParticles.FRWheel.emissionRate = 100;
        //    isDrifting1 = true;
        //}
        //else
        //{
        //    wheelParticles.FRWheel.emissionRate = 0;
        //    isDrifting1 = false;
        //}
        //if ((Mathf.Abs(wheelHits[1].sidewaysSlip) + Mathf.Abs(wheelHits[1].forwardSlip) > slipAllowance)){
        //    wheelParticles.FLWheel.emissionRate = 100;
        //    isDrifting2 = true;
        //}
        //else
        //{
        //    wheelParticles.FLWheel.emissionRate = 0;
        //    isDrifting2 = false;
        //}
        if ((Mathf.Abs(wheelHits[2].sidewaysSlip) + Mathf.Abs(wheelHits[2].forwardSlip) > slipAllowance)){
            wheelParticles.RRWheel.emissionRate = 100;
            isDrifting3 = true;
        }
        else
        {
            wheelParticles.RRWheel.emissionRate = 0;
            isDrifting3 = false;
        }
        if ((Mathf.Abs(wheelHits[3].sidewaysSlip) + Mathf.Abs(wheelHits[3].forwardSlip) > slipAllowance)){
            wheelParticles.RLWheel.emissionRate = 100;
            isDrifting4 = true;
        }
        else
        {
            wheelParticles.RLWheel.emissionRate = 0;
            isDrifting4 = false;
        }

        checkIfDrifting();

    }

    void checkIfDrifting()
    {
        if (isDrifting1 || isDrifting2 || isDrifting3 || isDrifting4)
        {
            //carEngineAudioScript.drifting = true;
            if (driftCounter > 1)
            {
                driftCounter += Time.deltaTime * 2;
            }
            else if (driftCounter > 5)
            {
                driftCounter += Time.deltaTime * 4;
            }
            else if (driftCounter > 15)
            {
                driftCounter += Time.deltaTime * 8;
            }
            else
            {
                driftCounter += Time.deltaTime;
            }
            //driftTextManager.updateText(driftCounter);
        }
        else
        {
            //carEngineAudioScript.drifting = false;
            driftPoints += driftCounter;
            //expManager.addDriftPoints(driftCounter);
            //multManager.getDrift(driftCounter);
            driftCounter = 0;
            //driftTextManager.stopText();
        }
    }

    void UpdateWheel(WheelCollider coll, MeshRenderer wheelMesh)
    {
        Quaternion quat;
        Vector3 position;
        coll.GetWorldPose(out position, out quat);
        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = quat;
    }


}

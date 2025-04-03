using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExplodeCar : MonoBehaviour
{
    public static ExplodeCar instance;
    public SavingSystem saveSystem;

    [SerializeField] GameObject slowCar;
    [SerializeField] GameObject mediumCar;
    [SerializeField] GameObject fastCar;

    [SerializeField] Cinemachine.CinemachineVirtualCamera gameCam;

    public AudioClip explosionAudio;

    public GameObject tempParent;
    public GameObject currentCar;
    public string carType;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        tempParent = GameObject.Find("destroyed");
        saveSystem = GameObject.Find("SaveManager").GetComponent<SavingSystem>();
        gameCam = GameObject.Find("CM vcam1").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        carType = saveSystem.carTypeStr;
    }

    public void DestroyCar()
    {
        currentCar.SetActive(false);
        gameCam.LookAt = null;
        if (carType == "slow")
        {
            GameObject instance = Instantiate(slowCar, currentCar.transform.position, currentCar.transform.rotation, tempParent.transform);
        }
        else if (carType == "medium")
        {
            GameObject instance = Instantiate(mediumCar, currentCar.transform.position, currentCar.transform.rotation, tempParent.transform);
        }
        else if (carType == "fast")
        {
            GameObject instance = Instantiate(fastCar, currentCar.transform.position, currentCar.transform.rotation, tempParent.transform);
        }
        Invoke("RespawnCar", 2f);
        CameraShakeManager.Instance.CameraShake(GetComponent<Cinemachine.CinemachineImpulseSource>());
        AudioManager.instance.PlaySfx(explosionAudio, instance.transform, 0.5f);
    }

    public void RespawnCar()
    {

        currentCar.SetActive(true);
        currentCar.GetComponent<EngineAudioScript>().enableEngine();
        gameCam.LookAt = currentCar.transform;
    }



    void Update()
    {
        
    }
}

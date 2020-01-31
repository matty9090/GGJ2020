using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercharacter : MonoBehaviour
{
    [SerializeField] Camera Cam;
    [SerializeField] float MoveSpeed;
    [SerializeField] Vector2 RotationSpeeds;
    [SerializeField] float MinYRotation;
    [SerializeField] float MaxYRotation;

    float yRotation = 0;

    GameObject CamLocation;

    // Start is called before the first frame update
    void Start()
    {
        CamLocation = transform.Find("CameraPos").gameObject;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        //player controls  
        if(Input.GetKey(KeyCode.W))
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward * MoveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.position += transform.right * MoveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.A))
            transform.position -= transform.right * MoveSpeed * Time.deltaTime;

        //rotation
        float turnAmountX = Input.GetAxis("Mouse X") * Time.deltaTime * RotationSpeeds.x;
        yRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * RotationSpeeds.y;
        transform.RotateAround(transform.position, Vector3.up, turnAmountX);
        yRotation = Mathf.Clamp(yRotation, MinYRotation, MaxYRotation);

        CamLocation.transform.localEulerAngles = new Vector3(-yRotation, CamLocation.transform.localEulerAngles.y, CamLocation.transform.localEulerAngles.z);


        //set camera position
        Cam.transform.position = CamLocation.transform.position;
        Cam.transform.rotation = CamLocation.transform.rotation;

    }
}

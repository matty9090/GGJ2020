using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercharacter : MonoBehaviour
{
    [SerializeField] Camera Cam = null;
    [SerializeField] GameObject CamDummy;
    [SerializeField] GameObject CamLocation;
    [SerializeField] float MoveSpeed = 50.0f;
    [SerializeField] float RunMultiplier = 1.4f;
    [SerializeField] Vector2 RotationSpeeds = new Vector2(500.0f, 200.0f);
    [SerializeField] float MinYRotation = -45.0f;
    [SerializeField] float MaxYRotation = 45.0f;

    float yRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = Vector3.zero;

        // Player controls  
        if (Input.GetKey(KeyCode.W))
            vel += transform.forward * MoveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            vel -= transform.forward * MoveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            vel += transform.right * MoveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.A))
            vel -= transform.right * MoveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            vel *= RunMultiplier;
            GetComponent<Animator>().SetFloat("SpeedMultiplier", RunMultiplier);
        }
        else
        {
            GetComponent<Animator>().SetFloat("SpeedMultiplier", 1.0f);
        }

        transform.position += vel;

        // Rotation
        float turnAmountX = Input.GetAxis("Mouse X") * Time.deltaTime * RotationSpeeds.x;
        yRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * RotationSpeeds.y;
        transform.RotateAround(transform.position, Vector3.up, turnAmountX);
        yRotation = Mathf.Clamp(yRotation, MinYRotation, MaxYRotation);

        CamDummy.transform.localEulerAngles = new Vector3(-yRotation, CamLocation.transform.localEulerAngles.y, CamLocation.transform.localEulerAngles.z);

        

        // Set camera position
        Cam.transform.position = CamLocation.transform.position;
        Cam.transform.rotation = CamLocation.transform.rotation;

        // Animation
        GetComponent<Animator>().SetFloat("Speed", vel.magnitude);
    }
}

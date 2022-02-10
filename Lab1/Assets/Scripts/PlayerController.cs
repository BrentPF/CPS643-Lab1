using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timeText;
    public GameObject winTextObject;
    public Camera mainCamera;
    public GameObject gate;
    private Rigidbody rb;
    private int count;
    private float time;
    private float movementY;
    private float mouseX;
    private float mouseY;
    private float sensitivityX = 0.5f;
    private float sensitivityY = 0.3f;
    private bool win = false;
    // Start is called before the first frame update
    void Start()
    {

        //Set Cursor to not be visible
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        count = 0;
        time = 0.0f;
        SetCountText();
        winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!win) {
            time += Time.deltaTime;
            timeText.text = ((int)time).ToString();
        }
        mainCamera.transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0.0f);
    }
    private void LateUpdate()
    {
       transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, mainCamera.transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementY = movementVector.y;
    }

    void OnMouseX(InputValue lookX)
    {
        float lookXValue = lookX.Get<float>();
        mouseX+= lookXValue * sensitivityX;
    }

    void OnMouseY(InputValue lookY)
    {
        float lookYValue = lookY.Get<float>();
        Debug.Log(lookYValue);
        mouseY += lookYValue * sensitivityY;
    }

    void OnFire(InputValue fire)
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 6, 0), ForceMode.Impulse);
    }

    void SetCountText() 
    {
        countText.text = count.ToString() + "/33";
        if (count >= 33)
        {
            gate.SetActive(false);
        }
    }

    void FixedUpdate() 
    {
        Vector3 movement = new Vector3(0.0f, 0.0f, movementY);

        rb.AddRelativeForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
        else if (other.gameObject.CompareTag("Exit")) 
        {
            win = true;
            winTextObject.SetActive(true);
        }
        
    }


}

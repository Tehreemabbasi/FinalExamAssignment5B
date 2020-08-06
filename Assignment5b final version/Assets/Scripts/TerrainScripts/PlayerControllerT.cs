using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class PlayerControllerT : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text winText;
    public TextMeshPro cubeText;
    public GameObject Panel;
    private Rigidbody rb;
    private int c;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        c = 0;
        SetCountText();
        winText.text = "";

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("parenthesis"))
        {
            other.gameObject.SetActive(false);
            c = c + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = " Count: " + c.ToString();
        if (c >18)
        {
            winText.text = "All balanced brackets strings are catured!!" + countText.text;
            Panel.SetActive(true);
        }
    }


}
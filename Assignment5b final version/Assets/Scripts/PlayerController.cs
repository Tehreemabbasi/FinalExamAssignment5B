using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text winText;
    public TextMeshPro cubeText;
  
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
        if (other.gameObject.CompareTag("palindrome"))
        {
            other.gameObject.SetActive(false);
            c = c + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = " Count: " + c.ToString();
        if (c==Spawner.count)
        {
            winText.text = "All Palindromes are captured!!"+ countText.text;
        }
    }
    
    
}
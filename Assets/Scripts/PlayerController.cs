using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rig;
    [SerializeField] private int pos;
    [SerializeField] private int maxSideSwipe;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxSidePosX;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float slideSpeed;

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject pointTakePrefab;
    private Vector3 desiredPos;
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {

       
    }
    private void FixedUpdate()
    {
        UpdatePosition(); 
    }



    private void UpdatePosition()
    {
        rig.MovePosition(Vector3.Lerp(rig.position, desiredPos, slideSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Vehicle"))
        {
            ExplosionVFX(collision);
            GameManager.instance.GameOver();
            gameObject.SetActive(false);
        }
    }

    private void ExplosionVFX(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;
        Instantiate(explosionPrefab, position, rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            Instantiate(pointTakePrefab, transform.position, Quaternion.identity);
            PlayerStats.instance.AddScore(1);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;

    }

    public void Move(int side)
    {
        if (side == 1 && pos < maxSideSwipe)
        {
            desiredPos += (Vector3.right ) * maxSidePosX;
            pos++;
        }


        if (side == 0 && pos > -maxSideSwipe)
        {
            desiredPos += (Vector3.left ) * maxSidePosX;
            pos--;
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rig.AddForce(Vector3.up * (jumpForce * 1000));
            isGrounded = false;
        }
            
    }
}

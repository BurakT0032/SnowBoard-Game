using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 15f;
    [SerializeField] float boostSpeed = 35f;
    [SerializeField] float baseSpeed = 25f;
    SurfaceEffector2D surfaceEffector;
    Rigidbody2D rgb2d;
    CapsuleCollider2D capsuleCollider2D;
    EdgeCollider2D edgeCollider2D;
    bool canMove = true;
    [SerializeField] float pozZ= 5f;
    [SerializeField] float negZ = 15f;

    // Start is called before the first frame update
    void Start()
    {

        rgb2d = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        surfaceEffector = FindObjectOfType<SurfaceEffector2D>(); // oyun objelerinin ozelliklerinde surface effector 2D olan yeri arar. Eger birden fazla surface effector 2D varsa calismaz.
        edgeCollider2D = FindObjectOfType<EdgeCollider2D>();

    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            RotatePlayer(); // oyunu baslatirken ilk seferde torku hep az aliyor update icinde calistirirsan o yuzden fixedupdate uzerinden calistirdim
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            BoostPlayer();
            jumpPlayer();
        }
    }
    public void DisableMove()
    {
        canMove = false;
    }

    private void BoostPlayer()
    {
        // oyuncu yukari tusuna basarsa boostspeedi aktif hale getir.
        if (Input.GetKey(KeyCode.UpArrow)) {
            surfaceEffector.speed = boostSpeed;
        }
        else
        {
            surfaceEffector.speed = baseSpeed;
        }
    }

    private void RotatePlayer()
    {
        if (!capsuleCollider2D.IsTouching(edgeCollider2D))
        {
            if (Input.GetKey(KeyCode.LeftArrow)) //Basili tutmamizi belirtmek icin getkey kullaniriz bir kere basma olsaydi getkeydown kullanirdik
            {

                rgb2d.AddTorque(torqueAmount);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rgb2d.AddTorque(-torqueAmount);
            }
        }
    }

    void jumpPlayer()
    {
        if (capsuleCollider2D.IsTouching(edgeCollider2D))
        {
            if (transform.rotation.z >= 0) //daha detayli ayar yap mesela z degeri 30 45 iken jumpAmount daha az artsin cunku dikey olunca daha cok zipliyor.
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rgb2d.AddForce(Vector2.up * pozZ, ForceMode2D.Impulse);
                }
            }
            if(transform.rotation.z < 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rgb2d.AddForce(Vector2.up * negZ, ForceMode2D.Impulse);
                }
            }
        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("rock"))
        {
            DisableMove();
            Invoke("ReloadScene", 0.5f);
        }
    }

}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovimiento : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public GameObject BulletPrefab;

    public Rigidbody2D RigidbodyD2;
    private float Horizontal;
    private bool Grounded; //para saber si John toca el suelo y permitirle saltat de nuevo
    private Animator Animator;
    private float LastShoot;
    private int Health = 5; //Vida
    //public Joystick joystick;

    void Start()
    {
        RigidbodyD2 = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

    }

    // Update is called once per frame
   public void Update()
    {
        //Capturar movimiento horizontal
        Horizontal = Input.GetAxisRaw("Horizontal");

        //Para validar y cambiar la direccion del personaje(imagen) si va para la izquierda o derecha
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running", Horizontal != 0.0f);

        //Para validar, si toca el suelo para permitirle saltar de nuevo, de lo contrario ya no saltaria(Evitar que se valla volando el personaje
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }


        if (Input.GetKeyDown(KeyCode.UpArrow) && Grounded)
        {
            Jump();
        }


        if (Input.GetKey(KeyCode.Space)&& Time.time > LastShoot + 0.25f)
        {
            Shoot();
            //Para controlar la cantidad de disparos, espera un tiempo para disparar
            LastShoot = Time.time;
        }
    }

    public void Shoot()
    {
        //Quaternion.identity indica rotacion cero
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position+ direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Jump()
    {
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }

        if (Grounded)
        {
            RigidbodyD2.AddForce(Vector2.up * JumpForce);
        }
        
    }

    private void FixedUpdate()
    {
        RigidbodyD2.velocity = new Vector2(Horizontal, RigidbodyD2.velocity.y);

        if (isLeft)
        {
            RigidbodyD2.AddForce(new Vector2(-Speed, 0) * Time.deltaTime);
            if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

        if (isRight)
        {
            RigidbodyD2.AddForce(new Vector2(Speed, 0) * Time.deltaTime);
            if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        //con tuto del joystick
        //Horizontal = joystick.Horizontal * Speed;

        //transform.position += new Vector3(Horizontal, 0, 0) * Time.deltaTime;

    }

    public void Hit()
    {
        Health--;
        if (Health == 0) Destroy(gameObject);
    }

    bool isLeft = false;
    bool isRight = false;

    public void ClicLeft()
    {
        isLeft = true;
    }
    public void ReleaseLeft()
    {
        isLeft = false;
    }

    public void ClicRight()
    {
        isRight = true;
    }
    public void ReleaseRight()
    {
        isRight = false;
    }

}

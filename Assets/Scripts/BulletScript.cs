using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public AudioClip Sound;
    public float Speed;

    private Vector2 Direction;
    private Rigidbody2D RigidbodyD2;
    void Start()
    {
        RigidbodyD2 = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RigidbodyD2.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Obtenemos la referencia de john y grunt para saber si han chocado
        JohnMovimiento john = collision.GetComponent<JohnMovimiento>();
        GruntScript grunt = collision.GetComponent<GruntScript>();

        if (john != null)
        {
            john.Hit();
        }
        if (grunt != null)
        {
            grunt.Hit();
        }

        DestroyBullet();
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //Obtenemos la referencia de john y grunt para saber si han chocado
    //    JohnMovimiento john = collision.collider.GetComponent<JohnMovimiento>();
    //    GruntScript grunt = collision.collider.GetComponent<GruntScript>();

    //    if(john!=null)
    //    {
    //        john.Hit();
    //    }
    //    if(grunt!=null)
    //    {
    //        grunt.Hit();
    //    }

    //    DestroyBullet();
    //}
}

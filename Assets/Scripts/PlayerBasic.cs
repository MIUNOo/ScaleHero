using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class PlayerBasic : MonoBehaviour
{
    public float speed = 5f;

    public GameObject bulletPrefab;

    public float spawnRange = 10f;
    public float bulletSpeed = 10f;

    private Rigidbody2D rb2D;
    private Vector3 movement;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Time.fixedDeltaTime = 0.02f;
    }

    void Update()
    {
        // Input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Movement vector
        movement = new Vector3(horizontalInput, verticalInput, 0);

        // Normalize to prevent faster movement diagonally
        movement.Normalize();


        //// ��ȡ���λ��
        //Vector3 mousePos = Input.mousePosition;

        //// �����������Ƕ�
        //float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        //transform.rotation = Quaternion.Euler(0, 0, angle);

        RotateTowardsMouse();


        if (Input.GetMouseButton(0))
        {

            //// ˮƽ������������ӵ�
            //Vector3 spawnPos = transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0);

            //// �����ӵ������ٶ�Ϊ������
            //GameObject bullet = Instantiate(bulletPrefab, spawnPos, transform.rotation);
            //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            //rb.velocity = transform.up * speed;

            ShootBullet();
        }



            //Debug.Log(movement.ToString());

            //transform.position += movement * speed * Time.deltaTime;
            // Move the player
            // MovePlayer(movement);
        }

        private void FixedUpdate()
    {
        //rb2D.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);

        //transform.Translate(movement * speed * Time.fixedDeltaTime);

        MovePlayer(movement * speed);

        

        //if (movement.x == 0 && movement.y ==0)
        //{
        //    rb2D.velocity = Vector2.zero;
        //    rb2D.AddForce(-rb2D.velocity);
        //}

    }


    void MovePlayer(Vector2 direction)
    {
        // Apply movement to the rigidbody
        rb2D.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }


    void RotateTowardsMouse()
    {
        // ��ȡ���λ��
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // ���㳯�����ķ�������
        Vector2 direction = (mousePos - transform.position).normalized;

        // ������ת�Ƕ�
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ͨ����ֵ��ת����
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 20*Time.deltaTime);
    }

    void ShootBullet() // FIX: HORIZON SHOOTING
    {
        // �����ӵ����������
        float randomAngle = Random.Range(-Mathf.PI / 4f, Mathf.PI / 4f); // ��-45�ȵ�45��֮�����ѡ��һ���Ƕ�
        Vector2 bulletDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));

        // ʵ�����ӵ�������λ�úͷ���
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;

        // ע�⣺��������ӵ��� Rigidbody2D �����ȷ���ӵ�Ԥ�����а��� Rigidbody2D ���
    }
}

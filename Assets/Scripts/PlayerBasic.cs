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


        //// 获取鼠标位置
        //Vector3 mousePos = Input.mousePosition;

        //// 计算面向鼠标角度
        //float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        //transform.rotation = Quaternion.Euler(0, 0, angle);

        RotateTowardsMouse();


        if (Input.GetMouseButton(0))
        {

            //// 水平方向随机生成子弹
            //Vector3 spawnPos = transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0);

            //// 设置子弹刚体速度为面向方向
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
        // 获取鼠标位置
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // 计算朝向鼠标的方向向量
        Vector2 direction = (mousePos - transform.position).normalized;

        // 计算旋转角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 通过插值旋转物体
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 20*Time.deltaTime);
    }

    void ShootBullet() // FIX: HORIZON SHOOTING
    {
        // 生成子弹的随机方向
        float randomAngle = Random.Range(-Mathf.PI / 4f, Mathf.PI / 4f); // 在-45度到45度之间随机选择一个角度
        Vector2 bulletDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));

        // 实例化子弹并设置位置和方向
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;

        // 注意：这里假设子弹有 Rigidbody2D 组件，确保子弹预制体中包含 Rigidbody2D 组件
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using static Logger;

public class PlayerBasic : MonoBehaviour
{
    public float speed = 5f;

    public GameObject bulletPrefab;

    public float spawnRange = 10f;
    public float bulletSpeed = 10f;
    public float minShootInterval = 0.2f;

    public float squeezingSpeed = 1f;
    public float minScale = 1f;

    public float recoilForce = 1f;

    private Rigidbody2D rb2D;
    private Vector3 movement;

    private float lastShootTime;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Time.fixedDeltaTime = 0.02f;
        // NEED CHANGE
        minScale = transform.localScale.x;
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

        #region Slow Mo
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Time.timeScale = 0.5f;
        //}

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    Time.timeScale = 1f;
        //}
        #endregion

        //// 获取鼠标位置
        //Vector3 mousePos = Input.mousePosition;

        //// 计算面向鼠标角度
        //float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        //transform.rotation = Quaternion.Euler(0, 0, angle);

        RotateTowardsMouse();



        if (Input.GetMouseButton(0)&&Time.time - lastShootTime >= minShootInterval&&transform.localScale.x!=minScale)   // 按下左键，若仍有射击时间且有子弹
        {
            ShootBullet();
            lastShootTime = Time.time;  // 更新上一次射击的时间
            StartCoroutine(SqueezePlayer(bulletPrefab.transform.localScale.x));
            ApplyRecoil();  // 后座力
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
        // Change Speed accroding to scale
        float speedWithScale = speed / transform.localScale.x;
        // FIXED: VELOCITY ACCUMULATE DURING SLOW MOTION

        // LOG(direction.ToString());

        float adjustedSpeed = speedWithScale * (Time.fixedDeltaTime / 0.02f);

        rb2D.velocity += new Vector2(direction.x * adjustedSpeed, direction.y * adjustedSpeed);


        // rb2D.AddForce(new Vector2(direction.x * speedWithScale, direction.y * speedWithScale), ForceMode2D.Force);

    }

    IEnumerator SqueezePlayer(float bulletSize)
    {

        var squeezeQt = transform.localScale.x - minScale;
        var targetScale = transform.localScale.x - bulletSize * squeezingSpeed;
        while (transform.localScale.x > Mathf.Clamp(targetScale,minScale,transform.localScale.x))
        {
            transform.localScale -= Vector3.one * squeezingSpeed * Time.deltaTime * bulletSize;
            yield return null;
        }

        transform.localScale = Vector3.one * Mathf.Clamp(targetScale, minScale, transform.localScale.x); // 确保最终大小不小于 minScale
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
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 10*Time.deltaTime);
    }

    void ShootBullet() 
    {
        // 生成子弹的随机方向
        //float randomAngle = Random.Range(-Mathf.PI / 4f, Mathf.PI / 4f); // 在-45度到45度之间随机选择一个角度
        //Vector2 bulletDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        Vector3 randomPos = transform.up * Random.Range(-spawnRange, spawnRange);


        // 实例化子弹并设置位置和方向
        GameObject bullet = Instantiate(bulletPrefab, transform.position+randomPos, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
        GetComponent<ScrollZoom>().StopAllCoroutines();
        //StopAllCoroutines();
        StartCoroutine(SqueezePlayer(bullet.transform.localScale.x));

        // 注意：这里假设子弹有 Rigidbody2D 组件，确保子弹预制体中包含 Rigidbody2D 组件
    }

    void ApplyRecoil()
    {
        // 获取子弹的发射方向
        Vector2 bulletDirection = transform.right;

        // 将后座力方向设置为与子弹发射方向相反
        Vector2 recoilDirection = -bulletDirection;

        // 将后座力应用到玩家的速度上
        rb2D.velocity += recoilDirection * recoilForce * Time.timeScale;
        //rb2D.AddForce(recoilDirection * recoilForce);
    }
}

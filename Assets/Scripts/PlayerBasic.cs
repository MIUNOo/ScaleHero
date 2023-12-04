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

        //// ��ȡ���λ��
        //Vector3 mousePos = Input.mousePosition;

        //// �����������Ƕ�
        //float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        //transform.rotation = Quaternion.Euler(0, 0, angle);

        RotateTowardsMouse();



        if (Input.GetMouseButton(0)&&Time.time - lastShootTime >= minShootInterval&&transform.localScale.x!=minScale)   // ������������������ʱ�������ӵ�
        {
            ShootBullet();
            lastShootTime = Time.time;  // ������һ�������ʱ��
            StartCoroutine(SqueezePlayer(bulletPrefab.transform.localScale.x));
            ApplyRecoil();  // ������
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
        float speedWithScale = speed/transform.localScale.x;
        // NEEDFIX: VELOCITY ACCUMULATE DURING SLOW MOTION


        rb2D.velocity += new Vector2(direction.x * speedWithScale, direction.y * speedWithScale);
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

        transform.localScale = Vector3.one * Mathf.Clamp(targetScale, minScale, transform.localScale.x); // ȷ�����մ�С��С�� minScale
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
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 10*Time.deltaTime);
    }

    void ShootBullet() 
    {
        // �����ӵ����������
        //float randomAngle = Random.Range(-Mathf.PI / 4f, Mathf.PI / 4f); // ��-45�ȵ�45��֮�����ѡ��һ���Ƕ�
        //Vector2 bulletDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        Vector3 randomPos = transform.up * Random.Range(-spawnRange, spawnRange);


        // ʵ�����ӵ�������λ�úͷ���
        GameObject bullet = Instantiate(bulletPrefab, transform.position+randomPos, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
        GetComponent<ScrollZoom>().StopAllCoroutines();
        //StopAllCoroutines();
        StartCoroutine(SqueezePlayer(bullet.transform.localScale.x));

        // ע�⣺��������ӵ��� Rigidbody2D �����ȷ���ӵ�Ԥ�����а��� Rigidbody2D ���
    }

    void ApplyRecoil()
    {
        // ��ȡ�ӵ��ķ��䷽��
        Vector2 bulletDirection = transform.right;

        // ����������������Ϊ���ӵ����䷽���෴
        Vector2 recoilDirection = -bulletDirection;

        // ��������Ӧ�õ���ҵ��ٶ���
        rb2D.velocity += recoilDirection * recoilForce;
        //rb2D.AddForce(recoilDirection * recoilForce);
    }
}

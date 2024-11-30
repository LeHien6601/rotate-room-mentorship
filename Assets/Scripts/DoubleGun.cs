using Unity.Mathematics;
using UnityEngine;
public class DoubleGun : Gun
{
    // Start is called before the first frame upd
    void Start()
    {
        GunStyle = "DoubleGun";
        bulletCount = 30;
    }
	public override void Shoot(Vector3 direction)
    {
        if(bulletCount <= 0) return;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = mousePos - (Vector2)transform.position;
        for(int i = 1; i <= 2; ++i)
        {
            Vector2 dir = Quaternion.AngleAxis(60f/(2*i) * i * math.pow(-1, i), Vector3.forward) * shootDirection.normalized;
            GameObject gameObject = Instantiate(bullet, transform.position, Quaternion.identity);
            gameObject.GetComponent<Rigidbody2D>().AddForce(dir.normalized * power);
        }
        bulletCount -= 2;
    }
}


using UnityEngine;
public class NormalGun : Gun
{
    // Start is called before the first frame upd
    void Start()
    {
        GunStyle = "NormalGun";
        bulletCount = 15;
    }
	public override void Shoot(Vector3 direction)
    {
        if(bulletCount <= 0) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = mousePos - (Vector2)transform.position;
        GameObject createdBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        createdBullet.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize(shootDirection) * power);
        bulletCount--;
    }
}


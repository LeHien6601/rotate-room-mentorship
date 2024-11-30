using UnityEngine;
public class NormalGun : Gun
{
    // Start is called before the first frame upd
    void Start()
    {
        GunStyle = "NormalGun";
    }
	public override void Shoot(Vector3 direction)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shootDirection = mousePos - transform.position;
        GameObject createdBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        createdBullet.GetComponent<Rigidbody2D>().AddForce(shootDirection.normalized * power);
    }
}


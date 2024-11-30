using Unity.Mathematics;
using UnityEngine;
public class DoubleGun : Gun
{
    // Start is called before the first frame upd
    void Start()
    {
        GunStyle = "DoubleGun";
    }
	public override void Shoot(Vector3 direction)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shootDirection = mousePos - transform.position;
        for(int i = 1; i <= 2; ++i)
        {
            Vector3 dir = Quaternion.AngleAxis(60f/(2*i) * i * math.pow(-1, i), Vector3.forward) * shootDirection.normalized;
            GameObject gameObject = Instantiate(bullet, transform.position, Quaternion.identity);
            gameObject.GetComponent<Rigidbody2D>().AddForce(dir.normalized * power);
        }
    }
}


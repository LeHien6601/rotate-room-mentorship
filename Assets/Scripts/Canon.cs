using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Canon : MonoBehaviour
{
    [SerializeField] private GameObject canonBallPrefab;
    [SerializeField] private Transform firePosition;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float delayTime;
    [SerializeField] private AudioClip canonSoundClip;
    public bool canFire;
    private float timer = 0f;
    private void Start()
    {
        particle.Stop();
    }
    private void Update()
    {
            timer += Time.deltaTime;
            if (timer >= delayTime)
            {
                if (canFire)
                {
                    Fire();
                    timer = 0f;
                }
            }
    }

    void Fire()
    {
        particle.Play();
        GameObject canonBall = Instantiate(canonBallPrefab, firePosition.position, Quaternion.identity, transform.parent);
        float rad = transform.eulerAngles.z * Mathf.Deg2Rad;
        canonBall.GetComponent<CanonBullet>().direction = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(canonSoundClip);
    }

    private void OnDrawGizmos()
    {
    }
}

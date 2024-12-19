﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float rotateDuration = 0.2f;
    [SerializeField] private Player player;
    public float timer = 0f;
    [SerializeField] private List<float> limits = new List<float>() { 6, 10, -6, -10 };
    private float height;
    private float width;
    private float oldDegree = 0f;
    private float targetDegree = 0f;
    private bool[] isLimit = new bool[2] { false, false };
    
    
    private bool preparing = true;
    public bool playing = false;
    [SerializeField] private Transform targetObject;
    public float prepareTimer = 0;
    public float prepareDuration = 4;
    public float moveSpeed = 1;


    private void Start()
    {
        //if (GameController.instance.KeysNeededToContinue == 0) targetObject = GameController.instance.finishGate.transform;
        targetObject = GameObject.FindGameObjectWithTag("Finish").transform;
        height = Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
    }

    //Modifies camera's transform
    void FixedUpdate()
    {
        if (!playing) return;
        if (timer > 0f)
        {
            if (targetDegree > 0f)
            {
                transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(oldDegree, targetDegree, (rotateDuration - timer) / rotateDuration));
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(oldDegree, targetDegree, (rotateDuration - timer) / rotateDuration));
            }
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                timer = 0f;
                transform.eulerAngles = new Vector3(0, 0, Mathf.RoundToInt(transform.eulerAngles.z / 90) * 90);
            }
        }
        CameraLimit();
        CameraFollow();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playing = true;
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
        if (!playing) CheckFinishGate();
    }

    private void CheckFinishGate()
    {
        if (preparing)
        {
            // Đếm thời gian focus vào vật thể
            prepareTimer += Time.deltaTime;

            if (targetObject != null)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(targetObject.position.x, targetObject.position.y, transform.position.z), moveSpeed * Time.deltaTime);
            }

            if (prepareTimer >= prepareDuration)
            {
                // Chuyển sang trạng thái di chuyển về phía người chơi
                preparing = false;
                prepareTimer = 0.0f; // Reset bộ đếm thời gian
            }
        }
        else
        {
            prepareTimer += Time.deltaTime;
            if (prepareTimer >= prepareDuration)
            {
                // Trả về trạng thái playing
                playing = true;
                prepareTimer = 0.0f; // Reset bộ đếm thời gian
            }
            // Từ từ di chuyển camera về phía người chơi
            if (player != null)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
            }
        }
    }
    private void CameraFollow()
    {
        Vector2 newCamPos = Vector2.Lerp(transform.position, player.transform.position,
              Mathf.Min(Vector2.Distance(transform.position, player.transform.position), followSpeed * Time.unscaledDeltaTime));
        if (isLimit[0] && isLimit[1])
        {
            //Debug.Log("Limit all");
            return;
        }
        if (isLimit[0])
        {
            //Debug.Log("Limit vertical");
            transform.position = new Vector3(newCamPos.x, transform.position.y, transform.position.z);
        }
        else if (isLimit[1])
        {
            //Debug.Log("Limit horizontal");
            transform.position = new Vector3(transform.position.x, newCamPos.y, transform.position.z);
        }
        else
        {
            //Debug.Log("No limit");
            transform.position = new Vector3(newCamPos.x, newCamPos.y, transform.position.z);
        }
    }
    private void CameraLimit()
    {
        float newX, newY;
        float W, H;
        float leftLimit, rightLimit, topLimit, bottomLimit;
        if (Mathf.RoundToInt(Mathf.Abs(transform.eulerAngles.z) / 90) % 2 == 1)
        {
            W = height; H = width;
        }
        else
        {
            W = width; H = height;
        }
        leftLimit = Mathf.Min(limits[3] + W, limits[1] - W);
        rightLimit = Mathf.Max(limits[3] + W, limits[1] - W);
        topLimit = Mathf.Max(limits[2] + H, limits[0] - H);
        bottomLimit = Mathf.Min(limits[2] + H, limits[0] - H);

        newX = Mathf.Clamp(player.transform.position.x, leftLimit, rightLimit);
        newY = Mathf.Clamp(player.transform.position.y, bottomLimit, topLimit);
        isLimit[1] = newX == leftLimit || newX == rightLimit;
        isLimit[0] = newY == topLimit || newY == bottomLimit;

        Vector3 newPos = new Vector3(newX, newY, transform.position.z);
        Vector3 diff = newPos - transform.position;
        if (diff.magnitude > 0.3f)
        {
            transform.position += diff.normalized * 0.3f;
        }
        else
        {
            transform.position = newPos;
        }
    }
    public void RotateCamera(float deg)
    {
        timer = rotateDuration;
        oldDegree = transform.eulerAngles.z;
        if (oldDegree >= 180 && deg > 0) oldDegree -= 360;
        if (oldDegree <= -180 && deg < 0) oldDegree += 360;
        oldDegree = Mathf.RoundToInt(oldDegree / 90) * 90;
        if ((transform.eulerAngles.z + deg) > +180)
        {
            targetDegree = transform.eulerAngles.z + deg - 360;
        }
        else if ((transform.eulerAngles.z + deg) < -180)
        {
            targetDegree = transform.eulerAngles.z + deg + 360;
        }
        else
        {
            targetDegree = transform.eulerAngles.z + deg;
        }
        targetDegree = Mathf.RoundToInt(targetDegree / 90) * 90;
    }
    public void SetLimits(float top, float right, float bottom, float left)
    {
        limits[0] = top; limits[1] = right;
        limits[2] = bottom; limits[3] = left;
    }
    public Vector4 GetLimits()
    {
        return new Vector4(limits[0], limits[1], limits[2], limits[3]);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(limits[1], limits[0]), new Vector2(limits[1], limits[2]));
        Gizmos.DrawLine(new Vector2(limits[1], limits[2]), new Vector2(limits[3], limits[2]));
        Gizmos.DrawLine(new Vector2(limits[3], limits[2]), new Vector2(limits[3], limits[0]));
        Gizmos.DrawLine(new Vector2(limits[3], limits[0]), new Vector2(limits[1], limits[0]));
    }
}
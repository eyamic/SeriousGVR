using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeBus : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 1.0f;

    private float startTime;
    private float distanceLength;

    private bool canMove = false;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        distanceLength = Vector3.Distance(startPoint.position, endPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            float distanceCovered = (Time.time - startTime) * speed;
            float fractionJourney = distanceCovered / distanceLength;

            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, fractionJourney);

            // 计算车辆的移动偏移量
            Vector3 movementOffset = (endPoint.position - startPoint.position) * fractionJourney;
            // 玩家跟着车移动
            player.position = startPoint.position + movementOffset;

            if (fractionJourney >= 1f)
            {
                Debug.Log("arrived");
                canMove = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 当玩家碰撞到车辆时才开始记录时间（如果写在update里，只要游戏开始车就会移动但是看不到，玩家只要上车车辆就会瞬移到该移动的到位置）
            startTime = Time.time;
            canMove = true;
        }
    }
}

using System;
using UnityEngine;

public class SmoothFollowTarget : MonoBehaviour
{
    public GameObject target;
    Vector3 offset;

    bool b;

    private void LateUpdate() // 플레이어 먼저 움직이게 하고, 카메라 동작을 그 다음에 하기 위해
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            return;
        }
        else
        {
            if (!b)
            {
                offset = transform.position - target.transform.position;
                b = true;
            }

            transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, Time.deltaTime * 5); // 처음엔 빨리 쫒아갔다가 점점 느리게 쫒아가게
            return;
        }
    }
}


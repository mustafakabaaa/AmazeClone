using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public const float MAX_SWIPE_TIME = 0.5f;

    
    public const float MIN_SWIPE_DISTANCE = 0.17f;
    [HideInInspector]
    Vector2 startPos;
    float startTime;
    private bool ballMove;

    public bool BallMove
    {
        get => ballMove;
        set => ballMove = value;
    }

    private Vector2 direction;

    public Vector2 Direction
    {
        get => direction;
        set => direction = value;
    }

    private void Update()
    {
        // Top hareket ederken ba�ka y�ne hareket ettirilmemesi i�in kontrol konulmu�tur.
        if (!ballMove)
        {
#if UNITY_EDITOR

            if (Input.GetKeyDown(KeyCode.UpArrow))
                direction = Vector2.up;
            if (Input.GetKeyDown(KeyCode.DownArrow))
                direction = Vector2.down;
            if (Input.GetKeyDown(KeyCode.RightArrow))
                direction = Vector2.right;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                direction = Vector2.left;
#endif
            
            if (Input.touches.Length > 0)
            {
                Touch t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Began)
                {
                    startPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);
                    startTime = Time.time;
                }
                if (t.phase == TouchPhase.Ended)
                {
                    if (Time.time - startTime > MAX_SWIPE_TIME) // press too long
                        return;

                    Vector2 endPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);

                    Vector2 swipe = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);

                    if (swipe.magnitude < MIN_SWIPE_DISTANCE) // Too short swipe
                        return;

                    if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                    { // Horizontal swipe
                        if (swipe.x > 0)
                        {
                            direction = Vector2.right;
                        }
                        else
                        {
                            direction = Vector2.left;
                        }
                    }
                    else
                    { // Vertical swipe
                        if (swipe.y > 0)
                        {
                            direction = Vector2.up;
                        }
                        else
                        {
                            direction = Vector2.down;
                        }
                    }
                }
            }
        }
    }
}

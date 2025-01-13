using Unity.Mathematics;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private float swipeThreshold = 50f; // Minimum distance for a swipe to be registered
    private Animator Anim;
    private int desiredLane=1;//0:left 1:middle 2:right
    public float laneDistance=4;//dist btw 2 lanes
    public float sideMoveSpeed=1;

    void Start()
    {
        Anim=GetComponent<Animator>();
    }

    void Update()
    {
        DetectSwipe();
        CalculateLane();
    }

    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Record the start position of the touch
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Record the end position of the touch
                endTouchPosition = touch.position;

                // Check the direction of the swipe
                Vector2 swipeDelta = endTouchPosition - startTouchPosition;

                if (swipeDelta.magnitude > swipeThreshold)
                {
                    // Swipe direction
                    float x = swipeDelta.x;
                    float y = swipeDelta.y;

                    if (Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        // Horizontal swipe
                        if (x > 0)
                        {
                            OnSwipeRight();
                        }
                        else
                        {
                            OnSwipeLeft();
                        }
                    }
                    else
                    {
                        // Vertical swipe
                        if (y > 0)
                        {
                            OnSwipeUp();
                        }
                        else
                        {
                            OnSwipeDown();
                        }
                    }
                }
            }
        }
    }

    private void OnSwipeLeft()
    {
        Debug.Log("Swipe Left");
        //change lane trial
        desiredLane--;
        if(desiredLane==-1)
        desiredLane=0;
        else{
            //changed lane succesfully
            transform.rotation=Quaternion.Euler(0f,0f,7f);
            Invoke("ResetTurnRotation",0.4f);
        }
        
    }

    private void OnSwipeRight()
    {
        //change lane trial
        Debug.Log("Swipe Right");
        desiredLane++;
        if(desiredLane==3)
        desiredLane=2;
        else{
            //changed lane succesfully
            transform.rotation=Quaternion.Euler(0f,0f,-7f);
            Invoke("ResetTurnRotation",0.4f);
        }
    }

    private void OnSwipeUp()
    {
        Debug.Log("Swipe Up");
        Anim.SetTrigger("Jump");
    }

    private void OnSwipeDown()
    {
        Debug.Log("Swipe Down");
        Anim.SetTrigger("Slide");
    }

    private void CalculateLane()
    {
        //mechanic for changing of lane 
        Vector3 targetPosition=transform.position.z*transform.forward+transform.position.y*transform.up;
        if(desiredLane==0)
        {
            targetPosition+=Vector3.left*laneDistance;
        }else if(desiredLane==2)
        {
            targetPosition+=Vector3.right*laneDistance;
        }
        
        transform.position=Vector3.Lerp(transform.position,targetPosition,sideMoveSpeed*Time.deltaTime);
    }
    private void ResetTurnRotation()
    {
        transform.rotation=Quaternion.Euler(0f,0f,0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAnim : MonoBehaviour
{
    //REFERENCES
    public Paddle paddle;
    private Animator animator;
    //DATA
    public float moveDetectionDelay;
    private float detectionTime;
    private bool considerMoving;

    private void OnEnable() {
        paddle.onStrike += TriggerStrike;
    }

    private void OnDisable() {
        paddle.onStrike -= TriggerStrike;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        detectionTime = moveDetectionDelay;
    }
    
    void Update()
    {
        if (paddle.isMoving) {
            if (!considerMoving) {
                considerMoving = true;
                animator.SetBool("moving", true);
            }
        } else {
            if (considerMoving) {
                if (detectionTime == 0f) {
                    detectionTime = moveDetectionDelay;
                    considerMoving = false;
                    animator.SetBool("moving", false);
                } else {
                    detectionTime -= Time.deltaTime;
                }
            }
        }
    }

    private void TriggerStrike(float force) {
        animator.SetTrigger("strike");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigidbodyController : MonoBehaviour {
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;
    private float distToGround;
    private Coroutine JumpCoroutine;

    [SerializeField] private Rigidbody PlayerBody;
    [SerializeField] private Animator PlayerAnimator;
    [Space]
    [SerializeField] private float Speed = 0.1f;
    // [SerializeField] private float Sensitivity = 10;
    [SerializeField] private float Jumpforce = 2f;

    private void Start() {
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void Update() {
        playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        JumpPlayer();
        SlowRunningPlayer();
    }

    private void FixedUpdate () {
        MovePlayer();
    }

    private bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    private void JumpPlayer() { 
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            if(JumpCoroutine != null) {
                StopCoroutine(JumpCoroutine);
            }
            JumpCoroutine = StartCoroutine(JumpDelay());
        }
    }

    private IEnumerator JumpDelay() {
        PlayerAnimator.SetTrigger("jump");
        yield return new WaitForSeconds(0.1f);
        PlayerBody.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
    }

    private void ClimbLadderPlayer() {
        if (Input.GetKeyDown(KeyCode.E)) {
            PlayerAnimator.SetBool("isClimbingLadder", true);
        } else {
            PlayerAnimator.SetBool("isClimbingLadder", false);
        }
    }

    private void SlowRunningPlayer() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            PlayerAnimator.SetBool("isSlowRunning", true);
        } else {
            PlayerAnimator.SetBool("isSlowRunning", false);
        }
    }

    private void MovePlayer() {
        ClimbLadderPlayer();
        PlayerAnimator.SetBool("isWalking", true);

        Vector3 movement = playerMovementInput.normalized;
        if (movement != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.fixedDeltaTime);
            PlayerBody.MovePosition(PlayerBody.position + movement * Speed * Time.fixedDeltaTime);
            PlayerBody.MoveRotation(targetRotation);
        }

        if (movement == Vector3.zero) {
            PlayerAnimator.SetBool("isWalking", false);
        }
    }
 }

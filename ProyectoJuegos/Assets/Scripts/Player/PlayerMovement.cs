using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    Vector3 movemoent;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;
    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }
    private void Move(float h, float v)
    {
        movemoent.Set(h, 0f, v);
        movemoent = movemoent.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movemoent);
    }
    private void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay,out floorHit,camRayLength, floorMask)) {
            Vector3 playerToMouse = floorHit.point - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }
    void Animating(float h, float v)
    {
        bool walking = (h != 0f || v != 0f);
        anim.SetBool("IsWalking", walking);

    }
}

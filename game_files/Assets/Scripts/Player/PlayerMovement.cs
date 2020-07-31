using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2;
    [SerializeField] private float runSpeed = 6;
    [SerializeField] private float gravity = -12;
    [SerializeField] private float jumpHeight = 2;
    [Range(0,1)][SerializeField] private float airControl = 1;

    private float temporaryCooldown;
    [SerializeField] private float evadeCooldown = 1;
    [SerializeField] private float evadeDistance = 3f;
    [SerializeField] private bool inEvade = false;

    [SerializeField] private float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    [SerializeField] private float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;

    Animator animator;
    Transform cameraT;
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cameraT = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Get inputs
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDirection = input.normalized;
        bool running = Input.GetKey(KeyCode.LeftShift);
        bool evade = Input.GetKey(KeyCode.LeftControl);

        // Movement
        if (controller.enabled)
        {
            Move(inputDirection, running);
        }
        
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            animator.ResetTrigger("Jump");
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") ||
                !animator.GetCurrentAnimatorStateInfo(0).IsName("Falling") ||
                !animator.GetCurrentAnimatorStateInfo(0).IsName("Landing"))
            {
                animator.SetTrigger("Jump");
                Jump();
            }
        }

        // Evade
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Evade"))
        {
            animator.ResetTrigger("evade");

            // if left control is pressed -> 
            if (evade)
            {
                // Reset everything else
                //animator.SetInteger("AttackAnimation", 0);

                // Excecute evade animation
                inEvade = true;
                animator.SetTrigger("evade");
            }
        }

        // Move certain amount in every update.
        if (inEvade)
            transform.position += transform.forward * Time.deltaTime * evadeDistance;

        // animation's speed is changed by dividing current speed by run speed or current speed by walk 
        // speed which in turn is multiplied by 0.5. 
        float animationSpeed = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * 0.5f);
        animator.SetFloat("speed", animationSpeed, speedSmoothTime, Time.deltaTime);

        CheckPlayerState();
    }

    void CheckPlayerState()
    {
        // allows player to move if player isn't attacking.
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack01") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack02") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack03"))
            controller.enabled = false;

        else
            controller.enabled = true;
    }

    void Move(Vector2 inputDirection, bool running)
    {
        // if inputDirection contains vector that doesn't have value of 0 do something
        if (inputDirection != Vector2.zero)
        {
            // calculate rotation
            float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }

        // if running is true targetSpeed gets value of runSpeed multiplied by magnitude. Else targetSpeed gets value of walkSpeed multiplied by magnitude.
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDirection.magnitude;

        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        velocityY += Time.deltaTime * gravity;
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            velocityY = 0;
        }
    }

    void Jump()
    {
        if (controller.isGrounded)
        {
            animator.SetBool("Landing", true);
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
        }
    }

    public void LandingOff()
    {
        animator.SetBool("Landing", false);
    }

    // used for disabling inEvade through animation
    public void InEvadeFalse()
    {
        inEvade = false;
    }

    float GetModifiedSmoothTime(float smoothTime)
    {
        if(controller.isGrounded)
            return smoothTime;

        if(airControl == 0)
            return float.MaxValue;

        return smoothTime / airControl;
    }
}

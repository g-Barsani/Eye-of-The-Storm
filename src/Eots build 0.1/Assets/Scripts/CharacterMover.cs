using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public bool isRunning = false;

    public bool isDodging = false;

    public bool isTargetable = true;



    [SerializeField]
    private float maxSpeed = 2.7f, acceleration = 40f, deacceleration = 100;

    [SerializeField]
    private float currentSpeed = 0;
    private Vector2 oldMovementInput;
    public Vector2 MovementInput { get; set; }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
       
    }

    private void FixedUpdate()
    {

        maxSpeed = isRunning ? 4f : 2.7f;

        if (isDodging)
        {
            Dodge();
        }

        if (MovementInput.magnitude > 0 && currentSpeed >= 0)
        {
            oldMovementInput = MovementInput;//.normalized;
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
        }
        else
        {
            // oldMovementInput = Vector2.zero;
            currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
       // currentSpeed = Run(isRunning);

        rb2d.velocity = oldMovementInput * currentSpeed;
        

        
    }

    private void Dodge()
    {
        //Coroutines are used to perform actions for a certain amount of time
        StartCoroutine(DodgeCoroutine());

    }

    //IEnumerator is an interface used to implement the
    //'yield return new WaitForSeconds(x);' that will perform an action for the
    //'x' amount of time, and after we write what to do when this is over
    private IEnumerator DodgeCoroutine()
    {
        // Executing dodge method for 0.25 seconds
        float dodgeDuration = 0.25f;
        this.maxSpeed = 3;
        isTargetable = false;

        yield return new WaitForSeconds(dodgeDuration);

        // backing to normal state after dodge
        this.maxSpeed = isRunning ? 4f : 2.7f;
        isDodging = false;
        isTargetable = true;
    }



    //private float Run(bool isrunning)
    //{
    //    if (isrunning)
    //    {
    //        maxSpeed = 4;

    //        return Mathf.Clamp(currentSpeed, 0, maxSpeed);

    //    }

    //    else {
    //        maxSpeed = 2.7f;
    //       return Mathf.Clamp(currentSpeed, 0, maxSpeed);
    //    }
    // }
}

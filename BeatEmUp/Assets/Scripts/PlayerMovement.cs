using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ComboState
{
    NONE,
    PUNCH_1,
    PUNCH_2,
    KICK_1,
    KICK_2

}

public class PlayerMovement : MonoBehaviour
{
    public SoundManager soundmanager;
    public int runSpeed;
    private  AudioSource AudioSource;
    private AudioClip currentAttackSound;

    private bool activateTimerToReset;
    public float defaultComboTimer = 0.4f;
    private float currentComboTimer;

    private ComboState currentComboState;

    public float horizontal;
    public float vertical;
    Animator animator;
    bool facingRight;

    bool isPunching;

    bool leftPunch = true;

    Button punchButton;
    public float maxYpos = -2.21f;
    public float minYpos = -4.90f;

    public Transform PunchPoint;
    public float punchRange = 0.5f;

    public Transform KickPoint;
    public float kickRange = 0.5f;

    public LayerMask enemyLayers;

    public int punchDamage = 10;
    public int kickDamage = 20;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        punchButton = GameObject.Find("PunchButton").GetComponent<Button>();
    }

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        currentComboTimer = defaultComboTimer;
        currentComboState = ComboState.NONE;
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal != 0 ? horizontal : vertical));

        ResetComboState();

    }

    
    public void Kick()
    {
        if (currentComboState == ComboState.KICK_2 || currentComboState == ComboState.PUNCH_1)
        {
            return;
        }

        if (currentComboState == ComboState.PUNCH_2)
        {
            currentComboState = ComboState.KICK_2;
            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;
        }
        if (currentComboState == ComboState.NONE)
        {
            currentComboState = ComboState.KICK_1;
            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;
        }
        else if (currentComboState == ComboState.KICK_1)
        {
            currentComboState = ComboState.KICK_2;
            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;
        }


        if (currentComboState == ComboState.KICK_1)
        {
            animator.SetTrigger("KickLeft");
            currentAttackSound = soundmanager.Kick;
        }
        if (currentComboState == ComboState.KICK_2)
        {
            animator.SetTrigger("KickRight");
            currentAttackSound = soundmanager.Kick2;
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(KickPoint.position, kickRange, enemyLayers);
        if (hitEnemies != null)
        {
            AudioSource.clip = currentAttackSound;
            AudioSource.Play();
        }
        foreach (Collider2D enemy in hitEnemies)
        {
            if(!enemy.GetComponent<Fighter2Enemy>().isDead)
            {
                enemy.GetComponent<Fighter2Enemy>().TakeDamage(kickDamage);
                enemy.GetComponent<Fighter2Enemy>().KnockUp();
            }        
        }
    }

    public void Punch()
    {
        if (currentComboState == ComboState.PUNCH_2 || currentComboState == ComboState.KICK_1 || currentComboState == ComboState.KICK_2)
        {
            return;
        }

        
            currentComboState++;
            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;
        
       

        if(currentComboState == ComboState.PUNCH_1)
        {
            animator.SetTrigger("PunchLeft");
            currentAttackSound = soundmanager.Punch1;
        }
        if (currentComboState == ComboState.PUNCH_2)
        {
            animator.SetTrigger("PunchRight");
            currentAttackSound = soundmanager.Punch2;
        }   

      Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(PunchPoint.position,punchRange,enemyLayers);

        if(hitEnemies != null)
        {
            AudioSource.clip = currentAttackSound;
            AudioSource.Play();
        }

        foreach (Collider2D enemy in hitEnemies)
        {
            if (!enemy.GetComponent<Fighter2Enemy>().isDead)
            {
                enemy.GetComponent<Fighter2Enemy>().TakeDamage(punchDamage);
            }
            
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (PunchPoint == null)
            return;
        Gizmos.DrawWireSphere(PunchPoint.position, punchRange);
    }

    void ResetComboState()
    {
        if (activateTimerToReset)
        {
            currentComboTimer -= Time.deltaTime;
            if(currentComboTimer <= 0f)
            {
                currentComboState = ComboState.NONE;
                activateTimerToReset = false;
                currentComboTimer = defaultComboTimer;
            }
        }
    }

    void FixedUpdate()
    {
        if((horizontal != 0 || vertical != 0) && !isPunching)
        {
            Vector3 movement = new Vector3(horizontal * runSpeed, vertical * runSpeed, 0.0f);
            transform.position = transform.position + movement * Time.deltaTime;
        }
       

        if (transform.position.y >= maxYpos)
        {
            transform.position = new Vector2(transform.position.x, maxYpos - 0.01f);
        }

        else if (transform.position.y <= minYpos)
        {
            transform.position = new Vector2(transform.position.x, minYpos-0.01f);
        }





        Flip(horizontal);

    }

    public void AlertObservers(string message)
    {
        if(message == "PunchEnded")
        {
            isPunching = false;
        }
    }

    private void Flip(float horizontal)
    {
        if(horizontal < 0 && !facingRight || horizontal > 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}

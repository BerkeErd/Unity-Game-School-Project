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
    public bool TestMode = false;
    public int maxHealth = 100;
    public int currentHealth;


    public Text HealthText;
    public HealthBar healthBar;
    public SoundManager soundmanager;
    public int runSpeed;
    private  AudioSource AudioSource;
    private AudioClip currentAttackSound;

    private bool activateTimerToReset;
    public float defaultComboTimer;
    private float currentComboTimer;

    private ComboState currentComboState;

    public float horizontal;
    public float vertical;
    Animator animator;
    bool facingRight;

    bool isPunching;
    bool isKicking;
    public bool isDead = false;
    public bool isFrozen = false;

    public Skills skills;
     


    
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
        skills = GetComponent<Skills>();
    }

    private void Start()
    {
        HealthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthBar = GameObject.Find("FighterHealtbar").GetComponent<HealthBar>();
        AudioSource = GetComponent<AudioSource>();
        soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        maxHealth += skills.sta * 10;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(currentHealth);

        defaultComboTimer = 0.4f - skills.agi / 100;

        currentComboTimer = defaultComboTimer;
        currentComboState = ComboState.NONE;

        punchDamage = punchDamage + skills.str + skills.str / 10;
        kickDamage = kickDamage + skills.str + skills.str /10;

        HealthText.text = "HP: " + currentHealth + "/" + maxHealth;

        animator.SetFloat("AttackSpeed", 1 + skills.agi / 10);

    }
    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal != 0 ? horizontal : vertical));

        ResetComboState();

    }

    
    public void Kick()
    {
        if(!isKicking && !isPunching)

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
                isKicking = true;
            animator.SetTrigger("KickLeft");
            currentAttackSound = soundmanager.Kick;
        }
        if (currentComboState == ComboState.KICK_2)
        {
                isKicking = true;
                animator.SetTrigger("KickRight");
            currentAttackSound = soundmanager.Kick2;
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(KickPoint.position, kickRange, enemyLayers);
        if (hitEnemies.Length > 0)
        {
            AudioSource.clip = currentAttackSound;
            AudioSource.Play();
        }
        foreach (Collider2D enemy in hitEnemies)
        {
                if(enemy.GetComponent<Enemy>())
                {
                    if (!enemy.GetComponent<Enemy>().isDead)
                    {
                        enemy.GetComponent<Enemy>().TakeDamage(kickDamage);
                        if (currentComboState == ComboState.KICK_2)
                        {
                            enemy.GetComponent<Enemy>().KnockUp();
                        }

                    }
                }
                else if(enemy.GetComponent<EnemyBoss>())
                {
                    if (!enemy.GetComponent<EnemyBoss>().isDead)
                    {
                        enemy.GetComponent<EnemyBoss>().TakeDamage(kickDamage);
                       

                    }
                }
            
        }
        }
    }

    public void Punch()
    {
        if (!isPunching && !isKicking)
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
                isPunching = true;
            animator.SetTrigger("PunchLeft");
            currentAttackSound = soundmanager.Punch1;
        }
        if (currentComboState == ComboState.PUNCH_2)
        {
                isPunching = true;
                animator.SetTrigger("PunchRight");
            currentAttackSound = soundmanager.Punch2;
        }   

      Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(PunchPoint.position,punchRange,enemyLayers);

        if(hitEnemies.Length > 0)
        {
            AudioSource.clip = currentAttackSound;
            AudioSource.Play();
        }

        foreach (Collider2D enemy in hitEnemies)
        {

                if (enemy.GetComponent<Enemy>())
                {
                    if (!enemy.GetComponent<Enemy>().isDead)
                    {
                        enemy.GetComponent<Enemy>().TakeDamage(punchDamage);
                    }
                }
           
                else if (enemy.GetComponent<EnemyBoss>())
                {
                    if (!enemy.GetComponent<EnemyBoss>().isDead)
                    {
                        enemy.GetComponent<EnemyBoss>().TakeDamage(punchDamage);

                    }
                }
            }
        }
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //  tookDamage = true;
        //  isAttacking = false;
        float pushPower = (float)damage / 200 * 7;
        if (!facingRight)
        {
           
           transform.position = new Vector2(transform.position.x - pushPower, transform.position.y);
            
            
        }
        else if (facingRight)
        {
            transform.position = new Vector2(transform.position.x + pushPower, transform.position.y);
        }
        
      //  animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
        healthBar.SetHealth(currentHealth);
        HealthText.text = "HP: " + currentHealth + "/" + maxHealth;
    }
    void Die()
    {
        isDead = true;
       // animator.SetBool("IsDead", isDead);


    }
    void FixedUpdate()
    {
        if((horizontal != 0 || vertical != 0) && !isPunching && !isKicking)
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



        if (Camera.main.WorldToViewportPoint(transform.position).x <= 0)
         {
            
             transform.position = new Vector2(transform.position.x+0.2f, transform.position.y);
         }

         else if (Camera.main.WorldToViewportPoint(transform.position).x >= 1)
         {
           
            transform.position = new Vector2(transform.position.x-0.2f, transform.position.y);
         }
         

       


        Flip(horizontal);

    }

    public void AlertObservers(string message)
    {
        if(message == "PunchEnded")
        {
            isPunching = false;
        }

        if(message == "KickEnded")
        {
            isKicking = false;
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

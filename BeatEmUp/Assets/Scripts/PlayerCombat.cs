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

public class PlayerCombat : MonoBehaviour
{
    public Text HealthText;
    public HealthBar healthBar;

    public int maxHealth = 100;
    public int currentHealth;

    public Transform PunchPoint;
    public float punchRange = 0.5f;

    public Transform KickPoint;
    public float kickRange = 0.5f;

    public SoundManager soundmanager;

    private bool activateTimerToReset;
    public float defaultComboTimer;
    private float currentComboTimer;
    private ComboState currentComboState;

    private AudioSource AudioSource;
    private AudioClip currentAttackSound;
    public bool isPunching;
    public bool isKicking;
    public int punchDamage;
    public int kickDamage;

    public bool isDead = false;
    
    private int changeSpeed = 50;

    Animator animator;
    public Skills skills;
    public PlayerMovement PlayerMovement;
    public LayerMask enemyLayers;
    public Slider ExpBar;
    public Text LevelText;
    public LevelManager levelManager;
    public SaveData saveData;
    public GameObject RestartMenu;

    // Start is called before the first frame update
    private void Awake()
    {
        LevelText = GameObject.Find("LevelText").GetComponent<Text>();
        animator = GetComponent<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();
        skills = GetComponent<Skills>();
        HealthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthBar = GameObject.Find("FighterHealtbar").GetComponent<HealthBar>();
        ExpBar = GameObject.Find("ExpBar").GetComponent<Slider>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        saveData = GameObject.Find("Main Camera").GetComponent<SaveData>();
    }
    void Start()
    {
        saveData.load();
        AudioSource = GetComponent<AudioSource>();
        
        punchDamage = skills.punchDamage;
        kickDamage = skills.kickDamage;
        soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        RestartMenu = GameObject.Find("RestartMenu");

        ExpBar.maxValue = skills.PlayerLevel * 20;

        maxHealth += skills.extraHealth;

        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);

        defaultComboTimer = 0.4f - skills.agiRatio;

        currentComboTimer = defaultComboTimer;
        currentComboState = ComboState.NONE;

        animator.SetFloat("AttackSpeed", 1 + skills.agiRatio * 5);

        LevelText.text = "Level : " + skills.PlayerLevel;
        RestartMenu.SetActive(false);

        UpdatehealthBar();
    }

    // Update is called once per frame
    void Update()
    {

        

        ResetComboState();
    }

    private void FixedUpdate()
    {
       
            ExpBar.value = Mathf.MoveTowards(ExpBar.value, skills.Exp, changeSpeed * Time.fixedDeltaTime);
        
    }

    public void Kick()
    {
        if (!isKicking && !isPunching && !isDead)

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

                BoxCollider2D collider = enemy as BoxCollider2D;

                if (collider != null)
                {
                    if (enemy.GetComponent<Enemy>())
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
                    else if (enemy.GetComponent<EnemyBoss>())
                    {
                        if (!enemy.GetComponent<EnemyBoss>().isDead)
                        {
                            enemy.GetComponent<EnemyBoss>().TakeDamage(kickDamage);


                        }
                    }
                }



            }
        }
    }

    public void Punch()
    {
        if (!isPunching && !isKicking && !isDead)
        {


            if (currentComboState == ComboState.PUNCH_2 || currentComboState == ComboState.KICK_1 || currentComboState == ComboState.KICK_2)
            {
                return;
            }


            currentComboState++;
            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;



            if (currentComboState == ComboState.PUNCH_1)
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

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(PunchPoint.position, punchRange, enemyLayers);

            if (hitEnemies.Length > 0)
            {
                AudioSource.clip = currentAttackSound;
                AudioSource.Play();
            }

            foreach (Collider2D enemy in hitEnemies)
            {
                BoxCollider2D collider = enemy as BoxCollider2D;

                if (collider != null)
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
    }

    public void Heal(int Healamount)
    {
        int NextHealth = currentHealth + Healamount;

        if (NextHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = NextHealth;
        }

        UpdatehealthBar();
    }

    void ResetComboState()
    {
        if (activateTimerToReset)
        {
            currentComboTimer -= Time.deltaTime;
            if (currentComboTimer <= 0f)
            {
                currentComboState = ComboState.NONE;
                activateTimerToReset = false;
                currentComboTimer = defaultComboTimer;
            }
        }
    }

    public void TakeDamage(int damage ,GameObject enemy)
    {
        isKicking = false;
        isPunching = false;
        currentHealth -= damage;
        //  tookDamage = true;
        //  isAttacking = false;
        float pushPower = (float)damage / 200 * 7;
        if (enemy.GetComponent<Enemy>())
        {
            if (!enemy.GetComponent<Enemy>().facingRight) //sola dönük
            {
                transform.position = new Vector2(transform.position.x - pushPower, transform.position.y);
            }
            else if (enemy.GetComponent<Enemy>().facingRight) //sağa dönük
            {
                transform.position = new Vector2(transform.position.x + pushPower, transform.position.y);
            }
        }

        else if (enemy.GetComponent<EnemyBoss>())
        {
            if (!enemy.GetComponent<EnemyBoss>().facingRight) //sola dönük
            {
                transform.position = new Vector2(transform.position.x - pushPower, transform.position.y);
            }
            else if (enemy.GetComponent<EnemyBoss>().facingRight) //sağa dönük
            {
                transform.position = new Vector2(transform.position.x + pushPower, transform.position.y);
            }
        }

        animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
        UpdatehealthBar();
    }

    void Die()
    {
        isDead = true;
        animator.SetBool("IsDead", isDead);
    }

    public void UpdatehealthBar()
    {
        healthBar.SetHealth(currentHealth);
        HealthText.text = "HP: " + currentHealth + "/" + maxHealth;
    }


    public void AlertObservers(string message)
    {
        if (message == "PunchEnded")
        {
            isPunching = false;
        }

        if (message == "KickEnded")
        {
            isKicking = false;
        }

        if (message == "PlayerDie")
        {
            RestartMenu.SetActive(true);
            Time.timeScale = 0;
        }

    }

    public void GainExp(int EnemyExp)
    {
        skills.Exp += EnemyExp + levelManager.Level * 5; 
        if (skills.Exp >= skills.PlayerLevel * 20)
        {
            skills.PlayerLevel++;
            skills.skillpoints++;
            skills.Exp = 0;
            ExpBar.maxValue = skills.PlayerLevel * 20;
        }
        LevelText.text = "Level : " + skills.PlayerLevel;
    }
}

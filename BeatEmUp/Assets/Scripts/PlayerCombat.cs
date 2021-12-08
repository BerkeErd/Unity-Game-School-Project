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

    public AudioSource AudioSource;
    public AudioClip currentAttackSound;
    public bool isPunching;
    public bool isKicking;
    public bool isTakeHit;
    public bool isUsingKickSkill;
    public bool isUsingPunchSkill;
    public bool isGoingUp;
    public bool isGoingDown;
    public int punchDamage;
    public int kickDamage;

    public bool isDead = false;

    private int changeSpeed = 50;
    public int SkillSpeed = 1;

    Animator animator;
    public Skills skills;
    public PlayerMovement PlayerMovement;
    public LayerMask enemyLayers;
    public Slider ExpBar;
    public Text LevelText;
    public LevelManager levelManager;
    public SaveData saveData;
    public GameObject RestartMenu;
    public Joystick Joystick;
    public Vector2 FirstPos;

    public Button PunchSkillButton, KickSkillButton;

    // Start is called before the first frame update
    private void Awake()
    {
        Joystick = GetComponent<Joystick>();
        animator = GetComponent<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();
        skills = GetComponent<Skills>();
        RestartMenu = GameObject.Find("RestartMenu");
        LevelText = GameObject.Find("LevelText").GetComponent<Text>();
        HealthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthBar = GameObject.Find("FighterHealtbar").GetComponent<HealthBar>();
        ExpBar = GameObject.Find("ExpBar").GetComponent<Slider>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        saveData = GameObject.Find("Main Camera").GetComponent<SaveData>();
        // saveData.load();
        KickSkillButton = GameObject.Find("KickSkillButton").GetComponent<Button>();
        PunchSkillButton = GameObject.Find("PunchSkillButton").GetComponent<Button>();
    }
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();

        punchDamage = skills.punchDamage + saveData.PunchUpgrade * 5;
        kickDamage = skills.kickDamage + saveData.KickUpgrade * 5;

        soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

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

        if(saveData.KickSkill <= 0)
        {
            KickSkillButton.gameObject.SetActive(false);
        }
        if (saveData.PunchSkill <= 0)
        {
            PunchSkillButton.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ResetComboState();
    }

    private void FixedUpdate()
    {
        if (isUsingKickSkill == true || isUsingPunchSkill == true)
        {
            PlayerMovement.isFrozen = true ;
            if (isGoingUp == true && isUsingKickSkill)
            {
               if(PlayerMovement.facingRight == false)
                {
                    transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, FirstPos.x + 1, SkillSpeed / 10 * Time.fixedDeltaTime)
                    , Mathf.MoveTowards(transform.position.y, FirstPos.y + 10, SkillSpeed/10 * Time.fixedDeltaTime));
                }
               else
                {
                    transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, FirstPos.x - 1, SkillSpeed / 10 * Time.fixedDeltaTime)
                    , Mathf.MoveTowards(transform.position.y, FirstPos.y + 10, SkillSpeed/10 * Time.fixedDeltaTime));
                }
            }
            else if (isGoingDown == true && isUsingKickSkill)
            {
                if (PlayerMovement.facingRight == false)
                {
                    transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, FirstPos.x + 1, SkillSpeed / 10 * Time.fixedDeltaTime)
                    , Mathf.MoveTowards(transform.position.y, FirstPos.y, SkillSpeed/10 * Time.fixedDeltaTime));
                }
                else
                {
                    transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, FirstPos.x - 1, SkillSpeed / 10 * Time.fixedDeltaTime)
                    , Mathf.MoveTowards(transform.position.y, FirstPos.y, SkillSpeed/10 * Time.fixedDeltaTime));
                }

            }

            

        }

        ExpBar.value = Mathf.MoveTowards(ExpBar.value, skills.Exp, changeSpeed * Time.fixedDeltaTime);
    }

    public void Kick()
    {
        if (!isKicking && !isPunching && !isDead && !isTakeHit && !PlayerMovement.isFrozen)
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

            HitEnemy(kickDamage, KickPoint, kickRange);
        }
    }

    public void Punch()
    {
        if (!isPunching && !isKicking && !isDead && !isTakeHit && !PlayerMovement.isFrozen)
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

            HitEnemy(punchDamage, PunchPoint, punchRange);

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

    public void TakeDamage(int damage, GameObject enemy)
    {
        isKicking = false;
        isPunching = false;
        isTakeHit = true;
        PlayerMovement.isFrozen = false;
        currentHealth -= damage;
        //  tookDamage = true;
        //  isAttacking = false;
        float pushPower = (float)damage / 200 * 2;
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

        if(!isUsingKickSkill && !isUsingPunchSkill)
        {
            animator.SetTrigger("Hit");
        }
        

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

        if (message == "Kicking")
        {
            isKicking = true;
        }

        if (message == "TakeHit")
        {
            isTakeHit = false;
        }

        if (message == "Death")
        {
            Time.timeScale = 0;
            RestartMenu.SetActive(true);
        }
        if (message == "SkillEnded")
        {
            
            
            if (isUsingKickSkill)
            {
                isTakeHit = false;
                PlayerMovement.isFrozen = false;
                isUsingKickSkill = false;                
                isGoingDown = false;             
            }

            else if(isUsingPunchSkill)
            {
                isTakeHit = false;
                PlayerMovement.isFrozen = false;
                isUsingPunchSkill = false;           
            }
            
        }
        if (message == "GoingUp")
        {
            isGoingUp = true;
            isGoingDown = false;
        }
        if (message == "GoingDown")
        {
            isGoingUp = false;
            isGoingDown = true;
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
            ExpBar.maxValue = skills.PlayerLevel * 30;
        }
        LevelText.text = "Level : " + skills.PlayerLevel;
    }




    public void KickSkill()
    {
        if (!isPunching && !isKicking && !isDead && !isTakeHit && !PlayerMovement.isFrozen)
        {
            //isKicking = true;
            isUsingKickSkill = true;
            animator.SetTrigger("KickSkill");
            currentAttackSound = soundmanager.Kick;
            FirstPos = transform.position;

            StartCoroutine(KickSkillKicking()); 

        }
    }

  



    IEnumerator KickSkillKicking()
    {
        int KickSkillDamage = saveData.KickSkill * (kickDamage * 2);
        yield return new WaitUntil(() => isKicking==true);
        while (isUsingKickSkill && isKicking)
        {
            HitEnemy(KickSkillDamage, KickPoint, kickRange * 1.2f);
            yield return new WaitForSeconds(0.1f);
        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.GetComponent<Collider2D>().enabled == false) //(enemy.GetComponent<Collider2D>().enabled == false && enemy.GetComponent<Enemy>().currentHealth >= 0) EnemyBoss olduğunda null referans veriyor
            {
                enemy.GetComponent<Collider2D>().enabled = true;
            }
        }
        isKicking = false;
        
        yield return null;
    }

    
    public void SpecialPunchStart()
    {
        if(!isPunching && !isKicking && !isDead && !isTakeHit && !PlayerMovement.isFrozen)
        {
            isPunching = true;
            isUsingPunchSkill = true;
            animator.SetTrigger("PunchSkill");
        } 
    }


    public void SpecialPunch(int Number)
    {
        int PunchSkillDamage = saveData.PunchSkill * (punchDamage * 3);
        if (Number == 3)
        {
            currentAttackSound = soundmanager.Punch1;
            HitEnemy(PunchSkillDamage, PunchPoint, punchRange);
        }
        else
        {
            currentAttackSound = soundmanager.Punch2;
            HitEnemy(PunchSkillDamage/6, PunchPoint, punchRange);
        }

    }

    public void HitEnemy(int damage, Transform DamagePoint, float Range)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(DamagePoint.position, Range, enemyLayers);

       /* if (hitEnemies.Length > 0)
        {
            AudioSource.clip = currentAttackSound;
            AudioSource.Play();
        } */

        foreach (Collider2D enemy in hitEnemies)
        {
            BoxCollider2D collider = enemy as BoxCollider2D;

            if (collider != null)
            {
                if (enemy.GetComponent<Enemy>())
                {
                    if (!enemy.GetComponent<Enemy>().isDead)
                    {
                        enemy.GetComponent<Enemy>().TakeDamage(damage);

                        if (isUsingKickSkill == true)
                        {
                            enemy.enabled = false;
                        }
                    }
                }

                else if (enemy.GetComponent<EnemyBoss>())
                {
                    if (!enemy.GetComponent<EnemyBoss>().isDead)
                    {
                        enemy.GetComponent<EnemyBoss>().TakeDamage(damage);

                        if (isUsingKickSkill == true)
                        {
                            enemy.enabled = false;
                        }
                    }
                }
            }
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instancePlayerController;

    [Header(" >> player Components: "), SerializeField] private Rigidbody2D rbPlayer;
    [SerializeField] private Animator anim;
    /// <anim>
    /// anim :
    ///     0: idle
    ///     1: run
    ///     2: jump
    ///     3: fall
    /// </anim>
    /// 
    [SerializeField] private Transform transformFirePoint;
    [SerializeField] private Animator animFirePoint;
    [SerializeField] private PlayerJumpCheck playerJumpCheck;


    [Header(" >> player Stats: ")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private int damage;

    private bool isJumpAble;
    private bool isHurt;

    [SerializeField] private float speed;
    private float speedFocus, speedControl;

    [Header(" --- ")]
    [SerializeField] private bool isTalking;

    [Header(" --- ")]
    [SerializeField] private float playerHurtCdTime;
    [SerializeField] private bool isCanHurt = true;

    [Header(" --- ")]
    [SerializeField] private float playerShieldCdTime;
    [SerializeField] private Timer shieldTimer;
    private bool isCanShield = true;

    [Header(" --- ")]
    [SerializeField] private float playerAttackCdTime;
    [SerializeField] private Timer attackTimer;
    [SerializeField] private Slider attackCooldownSlider;

    [Header(" >> Player Skills: ")]
    [SerializeField] private GameObject playerShield;
    [SerializeField] private GameObject playerNormalAttack;
    [SerializeField] private GameObject playerUpAttack;

    [SerializeField] private GameObject playerFocusEffect;
    private GameObject playerFocusEffectControl;

    [SerializeField] private GameObject playerControlEffect;
    private GameObject playerControlEffectControl;

    [Header(" >> UI: ")]
    [SerializeField] private Slider hpBar;
    [SerializeField] private Text hpText;

    [SerializeField] private Slider mpBar;
    [SerializeField] private Text mpText;



    public bool IsJumpAble { get => isJumpAble; set => isJumpAble = value; }
    public float JumpHeight { get => jumpHeight; set => jumpHeight = value; }
    public float Speed { get => speed; set => speed = value; }
    public int Damage { get => damage; set => damage = value; }
    public bool IsTalking { get => isTalking; set => isTalking = value; }

    private float GetPlayerSpeed()
    {
        return speed + speedFocus + speedControl;
    }

    private void Awake()
    {
        GameData.PLAYER_HP = 5;
        GameData.PLAYER_HP_MAX = 10;
        GameData.PLAYER_MP = 3;
        GameData.PLAYER_MP_MAX = 5;

        if (instancePlayerController == null)
        {
            instancePlayerController = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        isJumpAble = playerJumpCheck.IsCanJump();
        if (!isHurt)
        {
            PlayerMovement();
            if (!isTalking)
            {
                PlayerFire();
            }
            else
            {
                if (Input.GetButtonDown("Fire"))
                {
                    FindObjectOfType<DialogueManager>().DisplayNextSentence();
                }
            }
        }
        Animation(AnimationStates());

        PlayerSkill();

        UpdateUI();

        PlayerDie();
    }

    private void PlayerDie()
    {
        if (GameData.PLAYER_HP <= 0)
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                SceneManager.LoadScene(0);
                transform.position = new Vector3(0f, 0f);
            }
        }
    }

    private void PlayerSkill()
    {
        // Change Up Fire Point
        if (Input.GetButtonDown("Up"))
        {
            animFirePoint.SetInteger("States", 1);
        }
        if (Input.GetButtonUp("Up"))
        {
            animFirePoint.SetInteger("States", 0);
        }

        // player Focus mode
        isCanShield = shieldTimer.IsCompleted();
        if (Input.GetButtonDown("Left Shift"))
        {
            playerFocusEffectControl = Instantiate(playerFocusEffect, transform.position, transform.rotation);
            speedFocus = -3f;

            if (isCanShield)
            {
                CreateShield();
                shieldTimer.SetTime(playerShieldCdTime);
            }
        }

        if (Input.GetButtonUp("Left Shift"))
        {
            speedFocus = 0f;
            Destroy(playerFocusEffectControl);
        }

        // Player Control mode
        if (Input.GetButtonDown("Left Ctrl"))
        {
            speedControl = 4f;
            playerControlEffectControl = Instantiate(playerControlEffect, transform.position, transform.rotation);
        }
        if (Input.GetButtonUp("Left Ctrl"))
        {
            speedControl = 0;
            Destroy(playerControlEffectControl.gameObject);
        }

        // Healing skills
        if (Input.GetButtonDown("A"))
        {
            if (GameData.PLAYER_MP > 0)
            {
                GameData.PLAYER_HP++;
                GameData.PLAYER_MP--;
            }
        }
    }

    public void Hurt()
    {
        if (isCanHurt && !isTalking)
        {
            GameData.PLAYER_HP--;
            // debug
            // print(GameData.PLAYER_HP.ToString());
            anim.SetTrigger("Hurt");
            isHurt = true;
            rbPlayer.velocity = transform.right * -speed;
            rbPlayer.velocity = new Vector3(rbPlayer.velocity.x, jumpHeight);
            isCanHurt = false;
            StartCoroutine(PlayerHurtCD());
        }
    }

    public void ChangeHurtToFalse()
    {
        isHurt = false;
    }

    public void CreateShield()
    {
        Instantiate(playerShield, transform.position, transform.rotation);
    }

    private void Animation(int state)
    {
        anim.SetInteger("MainAnim", state);
    }

    private int AnimationStates()
    {
        if (!isJumpAble)
        {
            if (rbPlayer.velocity.y < 0.1)
            {
                return 3; // fall
            }
            if (rbPlayer.velocity.y > 0.1)
            {
                return 2; // jump
            }
        }
        if (rbPlayer.velocity.x != 0)
        {
            return 1; // run
        }
        return 0; // idle
    }

    private void UpdateUI()
    {
        attackCooldownSlider.value = attackTimer.timeTotal;

        hpBar.maxValue = GameData.PLAYER_HP_MAX;
        hpBar.value = GameData.PLAYER_HP;
        hpText.text = GameData.PLAYER_HP.ToString();

        mpBar.maxValue = GameData.PLAYER_MP_MAX;
        mpBar.value = GameData.PLAYER_MP;
        mpText.text = GameData.PLAYER_MP.ToString();


    }

    public void PlayerFire()
    {
        if (attackTimer.IsCompleted())
        {
            if (Input.GetButtonDown("Fire"))
            {
                if (animFirePoint.GetInteger("States") == 1)
                {
                    Instantiate(playerUpAttack, transformFirePoint.position, transformFirePoint.rotation);
                    attackTimer.SetTime(playerAttackCdTime / 2);
                    attackCooldownSlider.maxValue = playerAttackCdTime / 2;
                }
                else
                {
                    Instantiate(playerNormalAttack, transformFirePoint.position, transformFirePoint.rotation);
                    attackTimer.SetTime(playerAttackCdTime);
                    attackCooldownSlider.maxValue = playerAttackCdTime;
                }
            }
        }

    }

    private void PlayerMovement()
    {
        // Player Movement
        if (!isTalking)
        {
            if (Input.GetButton("MoveLeft"))
            {
                rbPlayer.velocity = new Vector2(-GetPlayerSpeed(), rbPlayer.velocity.y);
                transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
            }
            else if (Input.GetButton("MoveRight"))
            {
                rbPlayer.velocity = new Vector2(GetPlayerSpeed(), rbPlayer.velocity.y);
                transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
            else
            {
                rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
            }
        }
        else
        {
            rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && !IsTalking)
        {
            if (isJumpAble)
            {
                PlayerJump();
            }
        }
    }

    private void PlayerJump()
    {
        rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, jumpHeight);
    }

    IEnumerator PlayerHurtCD()
    {
        yield return new WaitForSeconds(playerHurtCdTime);
        isCanHurt = true;
    }
}

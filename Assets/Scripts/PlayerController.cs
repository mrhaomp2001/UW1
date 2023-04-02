using Cinemachine;
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
    [SerializeField] private int lv;
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
    [SerializeField] private Slider playerShieldSlider;
    [SerializeField] private GameObject playerNormalAttack;
    [SerializeField] private GameObject playerUpAttack;

    [Header(" --- ")]
    [SerializeField] private GameObject mofuAreaFocusMode;
    private GameObject mofuAreaFocusModeControl;

    [Header(" --- ")]
    [SerializeField] private GameObject fioaHealing;
    [SerializeField] private Timer timerFioaHealing;
    [SerializeField] private Slider sliderFioaHealing;

    [Header(" --- ")]
    [SerializeField] private GameObject playerFocusEffect;
    private GameObject playerFocusEffectControl;

    [SerializeField] private GameObject playerControlEffect;
    private GameObject playerControlEffectControl;

    [Header(" --- ")]
    [SerializeField] private int playerFocusFireType;
    [SerializeField] private float[] playerFocusFireCdTime;
    [SerializeField] private GameObject[] playerFocusFire;

    [Header(" --- ")]
    [SerializeField] private Timer timerPlayerFocusFireSkill;
    [SerializeField] private GameObject[] playerFocusFireSkill;

    [Header(" >> UI: ")]
    [SerializeField] private Slider hpBar;
    [SerializeField] private Text hpText;

    [SerializeField] private Slider mpBar;
    [SerializeField] private Text mpText;

    [SerializeField] private Text epText;
    [SerializeField] private GameObject gameObjectOptionsMenu;

    [SerializeField] private GameObject[] gameObjectsPlayerFocusIcon;

    [SerializeField] private Slider sliderPlayerFocusSkill;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    public bool IsJumpAble { get => isJumpAble; set => isJumpAble = value; }
    public float JumpHeight { get => jumpHeight; set => jumpHeight = value; }
    public float Speed { get => speed; set => speed = value; }
    public int Damage { get => damage; set => damage = value; }
    public bool IsTalking { get => isTalking; set => isTalking = value; }
    public int PlayerFocusFireType { get => playerFocusFireType; set => playerFocusFireType = value; }
    public int Lv { get => lv; set => lv = value; }

    private float GetPlayerSpeed()
    {
        return speed + speedFocus + speedControl;
    }

    private void Awake()
    {
        if (instancePlayerController == null)
        {
            instancePlayerController = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        GameData.PLAYER_HP = 5;
        GameData.PLAYER_HP_MAX = 10;
        GameData.PLAYER_MP = 3;
        GameData.PLAYER_MP_MAX = 5;
        GameData.PLAYER_EP = 1;

        PlayerData playerData = SaveSystem.LoadPlayer();
        if (playerData != null)
        {
            lv = playerData.Lv;
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
                PlayerSkill();
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = new Vector3(0f, 3f);
            }
            else
            {
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = new Vector3(0f, 2f);
                if (Input.GetButtonDown("Fire"))
                {
                    FindObjectOfType<DialogueManager>().DisplayNextSentence();
                }

            }
        }
        Animation(AnimationStates());

        UpdateUI();

        PlayerStatsUpdate();
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayer(this);
    }


    private void PlayerStatsUpdate()
    {
        if (GameData.PLAYER_EP >= 6)
        {
            GameData.PLAYER_MP++;
            GameData.PLAYER_EP -= 6;
        }

        if (GameData.PLAYER_HP <= 0)
        {
            if (SceneManager.GetActiveScene().name != "StageGameOver")
            {
                SceneManager.LoadScene("StageGameOver");
                transform.position = new Vector3(0f, 0f);
                isTalking = true;
                GameData.PLAYER_HP++;
            }
        }

        if(GameData.PLAYER_MP > GameData.PLAYER_MP_MAX)
        {
            GameData.PLAYER_MP = GameData.PLAYER_MP_MAX;
        }

        if (GameData.PLAYER_HP > GameData.PLAYER_HP_MAX)
        {
            GameData.PLAYER_HP = GameData.PLAYER_HP_MAX;
        }

        if (isTalking)
        {
            gameObjectOptionsMenu.SetActive(false);

            if (playerControlEffectControl != null)
            {
                Destroy(playerControlEffectControl.gameObject);
            }

            if (playerControlEffectControl != null)
            {
                Destroy(playerControlEffectControl.gameObject);
                speedControl = 0f;
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
            if (playerFocusFireType == 1)
            {
                mofuAreaFocusModeControl = Instantiate(mofuAreaFocusMode, transform.position, transform.rotation);
            }

            if (isCanShield)
            {
                CreateShield();
                shieldTimer.SetTime(playerShieldCdTime);
            }
        }

        if (Input.GetButtonUp("Left Shift"))
        {
            speedFocus = 0f;
            if (playerFocusFireType == 1)
            {
                Destroy(mofuAreaFocusModeControl);
            }
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
            Destroy(playerControlEffectControl);
        }

        // Healing skills
        if (Input.GetButtonDown("A"))
        {
            if (GameData.PLAYER_MP > 0 && timerFioaHealing.IsCompleted())
            {
                GameData.PLAYER_MP--;
                Instantiate(fioaHealing, new Vector3(transform.position.x + 2, transform.position.y), new Quaternion());
                timerFioaHealing.SetTime(2f);
            }
        }

        if (Input.GetButtonDown("S"))
        {
            if (GameData.PLAYER_MP > 0 && timerPlayerFocusFireSkill.IsCompleted())
            {
                GameData.PLAYER_MP--;
                Instantiate(playerFocusFireSkill[playerFocusFireType], new Vector3(transform.position.x, transform.position.y), new Quaternion());
                timerPlayerFocusFireSkill.SetTime(10f);
                sliderPlayerFocusSkill.maxValue = (10f);
            }
        }
    }

    public void Hurt()
    {
        if (isCanHurt && !isTalking)
        {
            gameObjectOptionsMenu.SetActive(false);
            GameData.PLAYER_HP--;
            // debug
            // print(GameData.PLAYER_HP.ToString());
            anim.SetTrigger("Hurt");
            isHurt = true;
            rbPlayer.velocity = transform.right * -speed;
            rbPlayer.velocity = new Vector3(rbPlayer.velocity.x, jumpHeight);
            isCanHurt = false;
            StartCoroutine(PlayerHurtCD(4f));
        }
    }

    public void ChangeHurtToFalse()
    {
        isHurt = false;
    }

    public void CreateShield()
    {
        Instantiate(playerShield, transform.position, transform.rotation);
        StartCoroutine(PlayerHurtCD(4f));
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

        playerShieldSlider.maxValue = playerShieldCdTime;
        playerShieldSlider.value = shieldTimer.timeTotal;

        sliderFioaHealing.value = timerFioaHealing.timeTotal;

        epText.text = GameData.PLAYER_EP.ToString() + "/6";

        sliderPlayerFocusSkill.value = timerPlayerFocusFireSkill.timeTotal;

        if(playerFocusFireType == 0)
        {
            gameObjectsPlayerFocusIcon[0].SetActive(true);
            gameObjectsPlayerFocusIcon[1].SetActive(false);
        }

        if (playerFocusFireType == 1)
        {
            gameObjectsPlayerFocusIcon[0].SetActive(false);
            gameObjectsPlayerFocusIcon[1].SetActive(true);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            gameObjectOptionsMenu.SetActive(!gameObjectOptionsMenu.activeSelf);
        }
    }

    public void PlayerFire()
    {
        if (attackTimer.IsCompleted())
        {
            if (Input.GetButtonDown("S"))
            {
                StartCoroutine(PlayerHurtCD(6f));
            }
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
                    if (speedFocus < 0)
                    {
                        if (playerFocusFireType == 0)
                        {
                            Instantiate(playerFocusFire[playerFocusFireType], transformFirePoint.position, transformFirePoint.rotation);
                            attackTimer.SetTime(playerFocusFireCdTime[playerFocusFireType]);
                            attackCooldownSlider.maxValue = playerFocusFireCdTime[playerFocusFireType];
                        }
                        else if (playerFocusFireType == 1)
                        {
                            Instantiate(playerFocusFire[playerFocusFireType], transformFirePoint.position, new Quaternion(0f, 0f, transform.rotation.y, 0f));
                            attackTimer.SetTime(playerFocusFireCdTime[playerFocusFireType]);
                            attackCooldownSlider.maxValue = playerFocusFireCdTime[playerFocusFireType];
                        }
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

    public void playerInvicible(float time)
    {
        StopCoroutine(PlayerHurtCD());
        StartCoroutine(PlayerHurtCD(time));
    }

    IEnumerator PlayerHurtCD()
    {
        yield return new WaitForSeconds(playerHurtCdTime);
        isCanHurt = true;
    }
    public IEnumerator PlayerHurtCD(float second)
    {
        isCanHurt = false;
        yield return new WaitForSeconds(second);
        isCanHurt = true;
    }
}

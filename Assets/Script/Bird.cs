using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private bool isDead = false;
    private Rigidbody2D rb2d;
    [SerializeField] KeyCode input = KeyCode.Space;
    private SpriteRenderer sprite;
    private float restartTime = 0f;
    private bool fixGoal;

    /*************** Skills *******************/
    private bool isSkill;

    // Invunerable
    [SerializeField] KeyCode skillInvunerable = KeyCode.Q;
    private bool isInvunerable = false;
    private float cooldownInvunerable = 30f;
    private float initCooldownInvunerable;
    [SerializeField] GameObject obInvunerable;

    // Ez Way
    [SerializeField] KeyCode skillEzWay = KeyCode.W;
    public bool isEzWay = false;
    private float cooldownEzWay = 20f;
    private float initCooldownEzWay;

    /*****************************************/

    [SerializeField] float jumpForce = 200f;
    [SerializeField] int score = 0;

    // User Interface
    public Text scoreText;
    public GameObject gameOver, countSkill, skillActive;
    private bool skillUI = false;

    [SerializeField] UnityEvent onJump, onDead, onPoint;
    private Animator anim;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        initCooldownInvunerable = cooldownInvunerable;
        initCooldownEzWay = cooldownEzWay;
    }

    void Update()
    {
        if (!isDead)
        {
            if (Input.GetKeyDown(input))
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, jumpForce));
                if (onJump != null) onJump.Invoke();
                anim.SetTrigger("Flap");
            }

            if (!isSkill)
            {
                if (Input.GetKeyDown(skillInvunerable))
                {
                    sprite.color = Color.red;
                    isInvunerable = true;
                    countSkill.SetActive(true);
                    skillActive.SetActive(true);
                    skillUI = true;
                    obInvunerable.SetActive(true);
                    isSkill = true;
                }
                else if (Input.GetKeyDown(skillEzWay))
                {
                    isEzWay = true;
                    skillUI = true;
                    isSkill = true;
                    countSkill.SetActive(true);
                    skillActive.SetActive(true);
                }
            } 
            else
            {
                if (isInvunerable)
                {
                    cooldownInvunerable -= Time.deltaTime;
                    countSkill.GetComponent<Text>().text = ((int)cooldownInvunerable + 1).ToString();
                    if (cooldownInvunerable <= 0)
                    {
                        countSkill.SetActive(false);
                        sprite.color = Color.white;
                        isInvunerable = false;
                        cooldownInvunerable = initCooldownInvunerable;
                        obInvunerable.SetActive(false);
                        fixGoal = false;
                        isSkill = false;
                    }

                    if (skillUI) {
                        if (cooldownInvunerable <= initCooldownInvunerable - 0.5f)
                        {
                            skillUI = false;
                            skillActive.SetActive(false);
                        }
                    } 
                } 
                else if (isEzWay)
                {
                    cooldownEzWay -= Time.deltaTime;
                    countSkill.GetComponent<Text>().text = ((int)cooldownEzWay + 1).ToString();
                    if (cooldownEzWay <= 0)
                    {
                        countSkill.SetActive(false);
                        isEzWay = false;
                        cooldownEzWay = initCooldownEzWay;
                        isSkill = false;
                    }

                    if (skillUI)
                    {
                        if (cooldownEzWay <= initCooldownEzWay - 0.5f)
                        {
                            skillUI = false;
                            skillActive.SetActive(false);
                        }
                    }
                }
            }
        }
        else
        {
            restartTime += Time.deltaTime;
            if (restartTime >= 2f) SceneManager.LoadScene(1);
        }
    }

    public bool getDead()
    {
        return isDead;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!isDead) goDead();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (obInvunerable.activeSelf)
        {
            if (collision.gameObject.tag == "Goal")
            {
                if (fixGoal == false)
                {
                    fixGoal = true;
                    incrementScore();
                }
                else fixGoal = false;
            }
        } 
        else
        {
            incrementScore();
        }
    }

    public void goDead()
    {
        isDead = true;
        if (gameOver != null)
        {
            skillActive.SetActive(false);
            gameOver.SetActive(true);
        }
        if (onDead != null) onDead.Invoke();
        anim.SetTrigger("Die");
    }

    public void incrementScore()
    {
        score++;
        if (scoreText != null) scoreText.text = score.ToString();
        if (onPoint != null) onPoint.Invoke();
    }
}
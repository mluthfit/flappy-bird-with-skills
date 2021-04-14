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

    // Skills
    // 0 -> Invunerable
    [SerializeField] KeyCode[] skill = { KeyCode.Q };
    private bool[] isSkill = { false };
    private float[] cooldown = { 30f };
    private float firstCooldown;

    // Invunerable
    [SerializeField] GameObject obInvunerable;

    [SerializeField] float jumpForce = 200f;
    [SerializeField] int score = 0;

    public Text scoreText;

    // UI
    public GameObject gameOver, invunerable, skillActive;
    private bool skillUI = false;

    [SerializeField] UnityEvent onJump, onDead, onPoint;
    private Animator anim;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        firstCooldown = cooldown[0];
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

            if (!isSkill[0])
            {
                if (Input.GetKeyDown(skill[0]))
                {
                    sprite.color = Color.red;
                    isSkill[0] = true;
                    invunerable.SetActive(true);
                    skillActive.SetActive(true);
                    skillUI = true;
                    obInvunerable.SetActive(true);
                }
            } 
            else
            {
                cooldown[0] -= Time.deltaTime;
                invunerable.GetComponent<Text>().text = ((int)cooldown[0]).ToString();
                if (cooldown[0] <= 0)
                {
                    invunerable.SetActive(false);
                    sprite.color = Color.white;
                    isSkill[0] = false;
                    cooldown[0] = firstCooldown;
                    obInvunerable.SetActive(false);
                }

                if (skillUI)
                {
                    if (cooldown[0] <= firstCooldown - 0.5f)
                    {
                        skillUI = false;
                        skillActive.SetActive(false);
                    }
                }
            }

        }
        else
        {
            restartTime += Time.deltaTime;
            if (restartTime >= 2f)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    public bool getDead()
    {
        return isDead;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!isDead)
        {
            goDead();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            if (fixGoal == false)
            {
                fixGoal = true;
                
                // Add a score
                score++;
                if (scoreText != null) scoreText.text = score.ToString();
                if (onPoint != null) onPoint.Invoke();
            }
            else
            {
                fixGoal = false;
            }
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
}

using Control;
using Rabbit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float ochenblizko;
    [SerializeField] private float blizko;
    [SerializeField] private float sredne;
    [SerializeField] private float dalno;
    [SerializeField] private float ochendalno;
    [SerializeField] private float speed, TimeToRevert;
    private const float IDLE_STATE = 0;
    private const float WALK_STATE = 1;
    private const float REVERT_STATE = 2;
    private const float PATROUL_STATE = 0;
    private const float FIND_STATE = 1;
    private const float ATTACK_STATE = 2;
    private float current_big_state;
    private Vector3 videl;
    private float current_state, currentTimeToRevert;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float Gotovo;
    [SerializeField] private float scale_speed;
    [SerializeField] private GameObject Scripts;

    private void Patroulir()
    {
        if (currentTimeToRevert >= TimeToRevert)
        {
            current_state = REVERT_STATE;
            currentTimeToRevert = 0;
        }
        switch (current_state)
        {
            case IDLE_STATE:
                currentTimeToRevert += Time.deltaTime;
                break;

            case WALK_STATE:
                rb.velocity = Vector2.right * speed;
                break;

            case REVERT_STATE:
                sp.flipX = !sp.flipX;
                speed *= -1;
                current_state = WALK_STATE;
                break;
        }
    }
    void Start()
    {
        current_state = WALK_STATE;
        current_big_state = PATROUL_STATE;
        currentTimeToRevert = 0;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        switch(current_big_state)
        {
            case PATROUL_STATE:
                Patroulir();
                if(player.transform.position.x - transform.position.x < blizko & !PlayerInput.Instance.IsHide)   
                {
                    videl = player.position;
                    current_big_state = ATTACK_STATE;
                }
                break;
            case ATTACK_STATE:
                if (transform.localScale.x < Gotovo)
                {
                    transform.localScale = new Vector3(transform.localScale.x + scale_speed * Time.deltaTime, transform.localScale.y + scale_speed * Time.deltaTime, transform.localScale.z + scale_speed * Time.deltaTime);
                }
                else
                {
                    if (player.transform.position.x - transform.position.x < sredne & !PlayerInput.Instance.IsHide)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                        videl = player.position;
                        if (player.transform.position.x - transform.position.x < ochenblizko)
                        {
                            EventSystem.OnRabbitKill?.Invoke();
                        }
                    }
                    else if (player.transform.position.x - transform.position.x < ochendalno & PlayerInput.Instance.IsSprint)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                        videl = player.position;
                        if (player.transform.position.x - transform.position.x < ochenblizko)
                        {
                            EventSystem.OnRabbitKill?.Invoke();
                        }
                    }
                    else if (player.transform.position.x - transform.position.x < blizko & PlayerInput.Instance.IsHide)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                        videl = player.position;
                        if (player.transform.position.x - transform.position.x < ochenblizko)
                        {
                            EventSystem.OnRabbitKill?.Invoke();
                        }
                    }
                    else
                    {
                        current_big_state = FIND_STATE;
                    }
                }
                break;
            case FIND_STATE:
                transform.position = Vector3.MoveTowards(transform.position, videl, speed * Time.deltaTime);
                if(transform.position == videl)
                {
                    if (player.transform.position.x - transform.position.x < sredne & !PlayerInput.Instance.IsHide)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                        videl = player.position;
                        if (player.transform.position.x - transform.position.x < ochenblizko)
                        {
                            EventSystem.OnRabbitKill?.Invoke();
                        }
                    }
                    else if (player.transform.position.x - transform.position.x < ochendalno & player.GetComponent<PlayerInput>().IsSprint)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                        videl = player.position;
                        if (player.transform.position.x - transform.position.x < ochenblizko)
                        {
                            EventSystem.OnRabbitKill?.Invoke();
                        }
                    }
                    else if (player.transform.position.x - transform.position.x < blizko & PlayerInput.Instance.IsHide)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                        videl = player.position;
                        if (player.transform.position.x - transform.position.x < ochenblizko)
                        {
                            EventSystem.OnRabbitKill?.Invoke();
                        }
                    }
                    else
                    {
                        current_big_state = PATROUL_STATE;
                    }
                }
                break;
        }

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Stop"))
        {
            current_state = IDLE_STATE;
        }
    }
}
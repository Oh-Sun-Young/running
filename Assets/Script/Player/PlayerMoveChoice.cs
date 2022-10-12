using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * [Unity] 오브젝트의 회전에 대하여(Rotation, Quaternion, Euler) : https://killu.tistory.com/12
 */
public class PlayerMoveChoice : MonoBehaviour
{
    public Animator myAnimator;

    private float timer = 0;

    private float speed = 1f;

    private float delay;
    [SerializeField] float delayMin;
    [SerializeField] float delayMax;

    private Vector3 destination;

    public GameObject[] player;
    private GameObject icon;
    private bool check;

    public int type;

    public float width;

    private void Awake()
    {
        width = Screen.width / 100;

        player = GameObject.FindGameObjectsWithTag("Player"); // 역순
        icon = transform.Find("ChoiceIcon").gameObject;
        for(int i = 0; i < player.Length ; i++)
        {
            if(System.Object.ReferenceEquals(player[i] , transform.gameObject))
            {
                type = (player.Length - 1) - i;
            }
        }
    }

    private void Start()
    {
        icon.SetActive(false);
    }

    private void Update()
    {
        if (Data.instance.characterType == type && FindObjectOfType<ChoiceButtonManger>().isStart)
        {
            speed *= 1.025f;
            if (!check)
            {
                check = true;
                myAnimator.SetTrigger("run");
            }

            transform.GetChild(0).eulerAngles = new Vector3(0, 0, 0);
            transform.Translate(Vector3.right * speed  * Time.deltaTime);

            if (width / 2 < transform.position.x)
            {
                FindObjectOfType<ButtonManager>().SceneMove(2);
            }
        }
        else
        {
            if (timer > delay)
            {
                timer = 0;
                delay = Random.Range(delayMin, delayMax);


                destination = transform.position;
                destination.x = Random.Range(-(width / 2) + 2, (width / 2) - 2);
                //destination.x = Random.Range(-8, 8);
            }
            else
            {
                timer += Time.deltaTime;

                if ((destination.x - transform.position.x) == 0)
                {
                    myAnimator.SetBool("move", false);
                }
                else
                {
                    myAnimator.SetBool("move", true);

                    if (transform.position.x < destination.x)
                    {
                        transform.GetChild(0).eulerAngles = Vector3.zero;
                    }
                    else
                    {
                        transform.GetChild(0).eulerAngles = new Vector3(0, 180, 0);
                    }
                }

                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            }
        }
    }
}

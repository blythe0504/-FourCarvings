using UnityEngine;

namespace FourCarvings
{

    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 3f;

        public Rigidbody2D rb;

        Vector2 movement;

        public Animator animator;

        public GameObject playerBag;

        public bool isOpen;

        //private AudioSource audioPlayer;

       // public AudioClip walk;
        public static bool _switch;

        //public Rigidbody2D playerGB;

        private void Start()
        {
            //this.transform.position = new Vector2(-17, 1);
           // audioPlayer = GameObject.Find("守護者").GetComponent<AudioSource>();
        }

        private void Update()
        {
            //Input
            if (_switch == false)
            {
                moveSpeed = 3.0f;
                if (DialogueManger.speed == true)
                {
                    moveSpeed = 5.0f;
                    //Debug.Log("守護者加速度成功");

                }

                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
                rb.isKinematic = false;
               
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);

                //audioPlayer.PlayOneShot(walk);
            }
            else
            {
                rb.isKinematic = true;
                moveSpeed = 0;
                

            }

            OpenPlayerBag();

            //Speed();
        }

        private void FixedUpdate()
        {
            //Movement
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

        public void OpenPlayerBag()
        {
            if(Input.GetKeyDown(KeyCode.O))
            {
                isOpen = !isOpen;
                playerBag.SetActive(isOpen);
            }
        }

        public void StepAudio()
        {
            AudiaoManager.PlayFootstepAudio();
        }
        /*
        public void Speed()
        {
            if (DialogueManger.speed == true)
            {
                moveSpeed++;
                moveSpeed++;
                Debug.Log("守護者加速度成功");
               
            }
        }
        */
    }
}

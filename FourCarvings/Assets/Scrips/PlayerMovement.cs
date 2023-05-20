using UnityEngine;

namespace FourCarvings
{

    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;

        public Rigidbody2D rb;

        Vector2 movement;

        public Animator animator;

        public GameObject playerBag;

        public bool isOpen;

        public static bool _switch;

        //public Rigidbody2D playerGB;

        private void Start()
        {
            //this.transform.position = new Vector2(-17, 1);
        }

        private void Update()
        {
            //Input
            if (_switch == false)
            {
                moveSpeed = 5.0f;
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
                rb.isKinematic = false;

                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);
            }
            else
            {
                rb.isKinematic = true;
                moveSpeed = 0;
                

            }

            OpenPlayerBag();
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
    }
}

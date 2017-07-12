using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    // 방향성 움직임에 적용 되는 움직임 속도
    public float playerSpeed = 4.0f;
    // 플레이어의 현재 속도
    private float currentSpeed = 0.0f;
    //마지막으로 행한 움직임
    private Vector3 lastMovement = new Vector3();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Rotation();
        Movement();

    }
    void Rotation()
    {
        // 플레이어를 기준으로 마우스의 위치를 구함.
        Vector3 worldPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);

        /*
         * 각 축을 기준으로 거리 차이를 구함
         * dx deltaX의 약자, DY는 deltaY의 약자
         */
        float dx = this.transform.position.x - worldPos.x;
        float dy = this.transform.position.y - worldPos.y;

        // 두 오브젝트 사이의 각도를 구함

        /*
         * Mathf.Atan2 = 탄젠트를 라디안 각도로 리턴
         * Mathf.Rad2Deg = 라디안을 디그리로 바꿔주는 함수
         */
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        /*
         * Transform의 회전속성은 4원수 (Quaternion)를 사용
         * 따라서 각도를 벡터로 변환 할 필요가 없음
         * Z축은 2D의 회전에 사용
         */
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        this.transform.rotation = rot;
        
    }
    void Movement()
    {
        // 현재 프레임에 일어나야 할 움직임
        Vector3 movement = new Vector3();

        //입력 체크
        movement.x += Input.GetAxis("Horizontal");
        movement.y += Input.GetAxis("Vertical");

        //여러개의 버튼이 눌려도 같은 거리를 움직이게 한다.
        movement.Normalize();
        //무엇이든지 눌렸는지 여부 확인
        // magnitude = 백터의 길이(크기)를 반환

        if (movement.magnitude > 0)
        {
            // 눌렸으면 그 방향으로 움직인다.
            currentSpeed = playerSpeed;
            this.transform.Translate(movement * Time.deltaTime * playerSpeed, Space.World);
            lastMovement = movement;
        }else
        {
            // 그렇지 않다면 가던 방향으로 움직인다.
            this.transform.Translate(movement * Time.deltaTime * playerSpeed, Space.World);
            //시간이 지날수록 느려진다
            currentSpeed *= .9f;
        }
    }
}

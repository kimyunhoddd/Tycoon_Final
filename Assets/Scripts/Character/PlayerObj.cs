using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerObj : MonoBehaviour
{

    public int Damage;
    public GameObject Damage123;

    public GameObject Textpos;
   // public int HPP;


    public Player player;                      //캐릭터 상태 정보
    public SPUM_Prefabs _prefabs;
    public MoveManager moveManager;     //이동 객체
    public GameObject playerObj;        //캐릭터 객체

    //추적 관련 변수
    int time, HLV;                      //이동 횟수 및 사냥터 레벨
    float dirx, diry;                   //이동 방향
    int[][] border = { new int[] { 6, -37, 30, -23 }, new int[] { -18, 0, -2, 16},      //testRoom, Room101
                        new int[] { 15, -1, 30, 16}, new int[] { -18, -37, 0, -20}};    //room102, room103

    //이동 관련 변수
    bool go = true;         //경로탐색 유무
    int cnt, i;             //경로 순서
    float speed = 10f;      //캐릭터 이동 속도
    Vector2Int targetPos;   //목적지
    List<ANode> PosList;    //최적화된 경로 리스트

    //공격 관련 변수
    GameObject targetObj;
    string[] moster = { "boss", "c", "c++", "python","cc" };


    public enum PlayerState
    {
        idle,
        move,
        roam,
        attack,
        hp,
        fatigue,
        death,
        test
    }
    
    public PlayerState _playerState = PlayerState.idle;
    public PlayerState nextState;
    public Vector3 _goalPos;


    public GameObject prfHpBar;
    public GameObject prfFgBar;
    public GameObject canvas;
    public Image nowHpBar;
    public Image nowFgBar;
    RectTransform hpBar;
    RectTransform fgBar;

    SaveManager saveManager;
    void Start()
    {
        Application.targetFrameRate = 20;                       //화면 FPS 설정

        playerObj = GameObject.FindGameObjectWithTag("Player");    //캐릭터 객체 생성
        player = new Player();

        saveManager = new SaveManager();                        //데이터 불러오기

        if (PlayerPrefs.GetInt("user") == 1)                    //기존 유저인 경우            
        {
            player = saveManager.GameLoad();
            Debug.Log("데이터를 저장 완료>> 기존 유저/" + player.exp);
        }
        else                                                    //새로운 유저인 경우
        {
            player = new Player(1, 1, 100, 50, 100, 100, 30, 0, 5000, 0f, 20, false, false, new int[] { 0, 0 });
        }

        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();  //캐릭터 체력 상태 바 클론 생성
        fgBar = Instantiate(prfFgBar, canvas.transform).GetComponent<RectTransform>();  //캐릭터 피로도 상태 바 클론 생성
        nowHpBar = hpBar.transform.GetChild(0).GetComponent<Image>();
        nowFgBar = fgBar.transform.GetChild(0).GetComponent<Image>();

        moveManager = new MoveManager(-21, -39, 33, 21);          //맵 데이터 초기화
                                                                  // root = GameObject.FindGameObjectWithTag("Root");
    }
    
    private void Reset()
    {
        //root.transform.localRotation = Quaternion.Euler(0, 0, 0);
        monster = null;
        targetObj = null;
        nextState = PlayerState.idle;
    }

    void Update()
    {
        /* 체력바 설정 */
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - 0.4f, 0));  //체력바 위치 설정
        Vector3 _fgBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - 0.8f, 0));  //피로도파 위치 설정
        hpBar.position = _hpBarPos; //체력바 위치 적용
        fgBar.position = _fgBarPos; //피로도바 위치 적용
        nowHpBar.fillAmount = (float)player.hp / (float)player.MAXHP;           //체력바 실시간 양
        nowFgBar.fillAmount = (float)player.fatigue / (float)player.MAXFG;      //피로도바 실시간 양

        /* 조작을 위한 임시 코드들 */
        if (Input.GetKeyDown(KeyCode.UpArrow)) player.GainFatigue(20f);     //피로도 증가
        if (Input.GetKeyDown(KeyCode.DownArrow)) player.Attacked(10);       //체력 감소
        if (Input.GetKeyDown(KeyCode.LeftArrow)) player.Attacked(50);  //급격한 체력 감소
        if (Input.GetKeyDown(KeyCode.R)) //리셋 임시 코드
        {
            playerObj.transform.position = new Vector2(5, 0);
            _playerState = PlayerState.idle;
            targetObj = null;
        }
        /***************************/

        switch (_playerState)
        {
            case PlayerState.idle:          //대기 상태
                _prefabs.PlayAnimation(0);  //애니메이션 설정 : 대기
                ChangeState();              //상태에 맞게 상태값 변경
                break;

            case PlayerState.move:          //이동
                _prefabs.PlayAnimation(1);  //애니메이션 설정 : 이동 

                if (go)             //초기 타겟 지점 설정 및 패스파인딩
                {
                    Vector2Int startPos = new Vector2Int((int)transform.position.x, (int)transform.position.y); //Map-Grid와 캐릭터 좌표 크기 맞추기
                    PosList = new List<ANode>(moveManager.Move(startPos, targetPos));   //시작좌표와 타겟좌표를 사용하여 최적화된 경로 찾아 저장
                    cnt = PosList.Count;
                    go = false;
                    i = 0;
                }
                else
                {
                    if (i < cnt)    //이동
                    {
                        Vector2 target = new Vector2(PosList[i].x, PosList[i].y);       //현재 캐릭터 이동 좌표

                        if ((PosList[i].x - playerObj.transform.position.x) > 0)       //캐릭터 방향전환 : Left
                            _prefabs.transform.localScale = new Vector3(-1, 1, 1);
                        else if (PosList[i].x - playerObj.transform.position.x < 0)    //캐릭터 방향전환 : Right
                            _prefabs.transform.localScale = new Vector3(1, 1, 1);

                        playerObj.transform.position = Vector2.Lerp(playerObj.transform.position, target, speed * Time.deltaTime);
                        i++;
                    }
                    else        //도착
                    {
                        go = true;
                        _playerState = nextState;
                    }
                }
                break;
            case PlayerState.roam:          //몬스터 추적
                RoamMonster();              //추적 및 몬스터 감지
                if (targetObj != null) _playerState = PlayerState.attack;   //감지 완료 시 공격모드 돌입
                break;
            case PlayerState.attack:        //몬스터 공격
                if (player.fatigue >= player.MAXFG * 0.8) //피로도가 많이 쌓인 경우
                {
                    targetPos = new Vector2Int(-12, -6); //커피 앞
                    _playerState = PlayerState.move;
                    nextState = PlayerState.fatigue;
                }
                if (player.hp <= player.MAXHP * 0.2)  //체력이 부족한 경우
                {
                    if (player.hp <= 0)
                    {
                        _playerState = PlayerState.death;
                        break;
                    }
                    targetPos = new Vector2Int(-12, -12); //샌드위치 앞
                    _playerState = PlayerState.move;
                    nextState = PlayerState.hp;
                }

                if (playerObj.transform.position.x < targetObj.transform.position.x) _prefabs.transform.localScale = new Vector3(-1, 1, 1);  //캐릭터 방향전환 : Left
                else _prefabs.transform.localScale = new Vector3(1, 1, 1);  //캐릭터 방향전환 : Right

                Vector2 monPos = new Vector2(targetObj.transform.position.x + _prefabs.transform.localScale.x * 2, targetObj.transform.position.y - 2);   //몬스터 위치 구하기
                playerObj.transform.position = Vector2.MoveTowards(playerObj.transform.position, monPos, Time.deltaTime * speed); //추적

                if (targetObj.activeSelf == true) { //몬스터가 살아있는 경우
                    if(Vector2.Distance(playerObj.transform.position, targetObj.transform.position) <= 4)       //사정거리 내 진입
                    {  
                        _prefabs.PlayAnimation(0);
                        Invoke("AttackMonster", 1f);
                    }
                }
                else //몬스터가 죽은 경우
                {
                    Debug.Log("before : " + player.gold + " " + player.exp);
                    //경험치 & 골드 얻기는 애니메이션
                    player.KillMoster();    //경험치와 골드 얻음
                    Debug.Log("after : " + player.gold + " " + player.exp);
                    targetObj = null;
                    _playerState = PlayerState.roam;
                }
                break;
            case PlayerState.test:

                break;
            case PlayerState.hp:
                Debug.Log("현재 골드: " + player.gold);
                if (player.Recovery(0)) //체력 올리기
                {
                    _playerState = PlayerState.idle;
                }
                else Stun();
                break;
            case PlayerState.fatigue:
                Debug.Log("현재 골드: " + player.gold);
                if (player.Recovery(1)) //피로도 내리기
                {
                    _playerState = PlayerState.idle;
                }
                else Stun();
                break;
            case PlayerState.death:
                _prefabs.PlayAnimation(2);
                Invoke("Distroy", 2f);
                break;
        }
    }

    void Distroy()
    {
        playerObj.SetActive(false);
        Invoke("Alive", 5f);
        CancelInvoke("Distroy");
    }
    void Alive()
    {
        Reset();
        player.alive();
        _playerState = PlayerState.idle;
        _prefabs.PlayAnimation(0);
        playerObj.transform.position = new Vector2(7, -10);
        playerObj.SetActive(true);

        _prefabs.PlayAnimation(2);
        CancelInvoke("Alive");
    }
    void Stun()
    {
        _prefabs.PlayAnimation(3);
        Invoke("MakeGold", 3f);
    }
    void MakeGold()
    {
        player.gold += 50;
        CancelInvoke("MakeGold");
    }
    void ChangeState()
    {
        if (player.CanHunt())    //자동 사냥
        {
            HLV = player.GetHLV();
            switch (HLV)  //목적지 설정
            {
                case 1:
                    targetPos = new Vector2Int(-6, 3);
                    break;
                case 2:
                    targetPos = new Vector2Int(15, 3);
                    break;
                case 3:
                    targetPos = new Vector2Int(-9, -18);
                    break;
            }
            _playerState = PlayerState.move;
            nextState = PlayerState.roam;
            time = 3;
            dirx = Random.Range(-1f, 1f) * 0.8f;
            diry = Random.Range(-1f, 1f) * 0.8f;
        }
        else
        {
            if (player.fatigue >= player.MAXFG * 0.8) //피로도가 많이 쌓인 경우
            {
                targetPos = new Vector2Int(-12, -6); //커피 앞
                _playerState = PlayerState.move;
                nextState = PlayerState.fatigue;
            }
            if (player.hp <= player.MAXHP * 0.2)  //체력이 부족한 경우
            {
                targetPos = new Vector2Int(-12, -12); //샌드위치 앞
                _playerState = PlayerState.move;
                nextState = PlayerState.hp;
            }
        }

    }
    Monster2 monster;
    bool attack;
    void AttackMonster()
    {
        _prefabs.PlayAnimation(4);
        monster = targetObj.GetComponent<Monster2>();
        monster.Attacked(player.power);
        player.GainFatigue(0.2f);
        Debug.Log("현재 피로도: " + player.fatigue);
        CancelInvoke("AttackMonster");

    }
    void RoamMonster()
    {
        if (time == 0)
        {
            time = Random.Range(3, 5);
            dirx = Random.Range(-1f, 1f);
            diry = Random.Range(-1f, 1f);
        }

        if (dirx < 0) _prefabs.transform.localScale = new Vector3(1, 1, 1);
        else _prefabs.transform.localScale = new Vector3(-1, 1, 1);

        Vector2 pos = playerObj.transform.position;
        if (pos.x + dirx > border[HLV][0] && pos.x + dirx < border[HLV][2] && pos.y + diry > border[HLV][1] && pos.y + diry < border[HLV][3])
        {
            playerObj.transform.position = new Vector2(playerObj.transform.position.x + dirx, playerObj.transform.position.y + diry);
        }
        time--;
    }

    void FixedUpdate()
    {
        float dir = _prefabs.transform.localScale.x * -1;
        Debug.DrawRay(playerObj.transform.position, new Vector3(dir, 0, 0) * 2f, new Color(0, 1, 0));
        if (targetObj == null)
        {
            RaycastHit2D hit = Physics2D.Raycast(playerObj.transform.position, new Vector3(dir, 0, 0) * 1f);

            if (hit.collider.CompareTag("Monster"))
            {
                Debug.Log(hit.collider.name);
                targetObj = GameObject.Find(moster[HLV]);
            }
        }
    }


    public void Damage12345()
    {
        GameObject damage12 = Instantiate(Damage123, transform.position, Quaternion.identity);
        Destroy(damage12, 2f);
        Damage123.GetComponent<Damageee>().Damage = Damage;
      //  Textpos.SetActive(true);
    }

    public void ddaa()
    {
        this.Textpos.SetActive(true);
    }

    public void ddbb()
    {
        this.Textpos.SetActive(false);
    }

    public void h_damage()
    {
        player.Attacked(10);
        // player.hp -= 10;
        hpBar.GetComponent<Image>().fillAmount = player.hp / player.MAXHP;
    }


}

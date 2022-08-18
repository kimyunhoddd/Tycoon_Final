using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ANode
{
    /** 맵 위치를 담을 노드 **/
    public ANode(bool _isWall, int _x, int _y)
    {
        isWall = _isWall;
        x = _x;
        y = _y;
    }
    public bool isWall;             //벽
    public ANode ParentNode;        //이전 노드
    public int x, y, G, H, F;       //x좌표, y좌표, G값, H값, F값
}

public class MoveManager
{
    /** 필요 데이터 선언 **/
    public Vector2Int bottomLeft, topRight;     //맵의 왼쪽 최하단 좌표값, 오른쪽 최상단 좌표값
    public Vector2Int startPos, targetPos;      //시작위치, 도착위치
    public List<ANode> FinalNodeList;           //최적화된 경로 리스트

    int sizeX, sizeY;                           //맵 사이즈
    float speed = 0.5f;                         //오브젝터 이동 속도
    ANode[,] NodeArray;                         //맵의 정보(벽/길)
    ANode StartNode, TargetNode, CurNode;       //시작위치 노드, 도착위치 노드, 현재노드
    List<ANode> OpenList, ClosedList;

    /** MoveManager 초기화**/
    public MoveManager(int leftBottomX, int leftBottomY, int rightTopX, int rightTopY)    //맵 초기화 13x8
    {
        bottomLeft = new Vector2Int(leftBottomX, leftBottomY);  //맵의 왼쪽 최하단 좌표값 지정 
        topRight = new Vector2Int(rightTopX, rightTopY);        //맵의 오른쪽 최상단 좌표값 지정

        MapInitialize();    //맵 데이터 추출 및 저장
    }

    /** 맵 데이터 추출 및 저장**/
    public void MapInitialize()
    {
        sizeX = topRight.x - bottomLeft.x + 1;          //맵 크기: 가로
        sizeY = topRight.y - bottomLeft.y + 1;          //맵 크기: 세로

        NodeArray = new ANode[sizeX, sizeY];            //맵 데이터를 저장할 노드리스트 초기화

        for (int i = 0; i < sizeX; i++)                 //열
        {
            for (int j = 0; j < sizeY; j++)             //행
            {
                bool isWall = false;

                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.5f))   //해당 위치에서 검출되는 객체 가져오기
                {
                    if (col.gameObject.layer == LayerMask.NameToLayer("Wall"))              //검출된 객체의 레이어가 "Wall"인 경우
                    {
                        isWall = true;                                                      //벽임을 체크
                    }
                }
                NodeArray[i, j] = new ANode(isWall, i + bottomLeft.x, j + bottomLeft.y);    //해당위치의 노드에 벽 데이터 저장
            }
        }
    }
    /** 최적화된 경로 찾기: A* 알고리즘 **/
    public void PathFinding()
    {
        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];    //시작위치의 노드 데이터 가져오기
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y]; //도착위치의 노드 데이터 가져오기


        //StartNode.G = 0;
        OpenList = new List<ANode>() { StartNode };     //열린리스트 초기화: 시작노드 추가
        ClosedList = new List<ANode>();                 //닫힌리스트 초기화
        FinalNodeList = new List<ANode>();              //최적화된 경로 리스트 초기화

        while (OpenList.Count > 0)                      //열린리스트에 노드가 있는 경우에만 진행
        {
            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
            {
                int openF = OpenList[i].G + OpenList[i].H;
                int curF = CurNode.G + CurNode.H;
                if (openF <= curF && OpenList[i].H < CurNode.H) CurNode = OpenList[i];
            }
            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            //목적지를 찾은 경우: 도착노드부터 시작노드를 만날 때까지 노드의 부모를 찾아 올라가 경로를 저장
            if (CurNode == TargetNode)
            {
                ANode TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();                //반전('도착-시작' --> '시작-도착')

                return;
            }
            //대각선 방향 추가
            OpenListAdd(CurNode.x + 1, CurNode.y + 1);  // ↗
            OpenListAdd(CurNode.x - 1, CurNode.y + 1);  // ↖
            OpenListAdd(CurNode.x - 1, CurNode.y - 1);  // ↙
            OpenListAdd(CurNode.x + 1, CurNode.y - 1);  // ↘

            //직선 방향 추가
            OpenListAdd(CurNode.x, CurNode.y + 1);      //↑
            OpenListAdd(CurNode.x + 1, CurNode.y);      // → 
            OpenListAdd(CurNode.x, CurNode.y - 1);      //↓ 
            OpenListAdd(CurNode.x - 1, CurNode.y);      // ←
        }
    }

    /** 갈 수 있는 길인지 판단&경로 최적화 **/
    void OpenListAdd(int checkX, int checkY)
    {
        // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // 대각선 허용시, 벽 사이로 통과 안됨
            if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

            // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
            if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;


            // 이웃노드에 추가(비용 : 직선=10, 대각선=14)
            ANode NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;
                OpenList.Add(NeighborNode);
            }
        }
    }
    /** 최적화된 경로 리턴 **/
    public List<ANode> Move(Vector2Int _startPos, Vector2Int _targetPos)
    {
        startPos = _startPos;       //시작위치 설정
        targetPos = _targetPos;     //도착위치 설정

        PathFinding();              //길 찾기

        return FinalNodeList;       //최적화된 경로 리턴
    }
}

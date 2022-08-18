using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ANode
{
    /** �� ��ġ�� ���� ��� **/
    public ANode(bool _isWall, int _x, int _y)
    {
        isWall = _isWall;
        x = _x;
        y = _y;
    }
    public bool isWall;             //��
    public ANode ParentNode;        //���� ���
    public int x, y, G, H, F;       //x��ǥ, y��ǥ, G��, H��, F��
}

public class MoveManager
{
    /** �ʿ� ������ ���� **/
    public Vector2Int bottomLeft, topRight;     //���� ���� ���ϴ� ��ǥ��, ������ �ֻ�� ��ǥ��
    public Vector2Int startPos, targetPos;      //������ġ, ������ġ
    public List<ANode> FinalNodeList;           //����ȭ�� ��� ����Ʈ

    int sizeX, sizeY;                           //�� ������
    float speed = 0.5f;                         //�������� �̵� �ӵ�
    ANode[,] NodeArray;                         //���� ����(��/��)
    ANode StartNode, TargetNode, CurNode;       //������ġ ���, ������ġ ���, ������
    List<ANode> OpenList, ClosedList;

    /** MoveManager �ʱ�ȭ**/
    public MoveManager(int leftBottomX, int leftBottomY, int rightTopX, int rightTopY)    //�� �ʱ�ȭ 13x8
    {
        bottomLeft = new Vector2Int(leftBottomX, leftBottomY);  //���� ���� ���ϴ� ��ǥ�� ���� 
        topRight = new Vector2Int(rightTopX, rightTopY);        //���� ������ �ֻ�� ��ǥ�� ����

        MapInitialize();    //�� ������ ���� �� ����
    }

    /** �� ������ ���� �� ����**/
    public void MapInitialize()
    {
        sizeX = topRight.x - bottomLeft.x + 1;          //�� ũ��: ����
        sizeY = topRight.y - bottomLeft.y + 1;          //�� ũ��: ����

        NodeArray = new ANode[sizeX, sizeY];            //�� �����͸� ������ ��帮��Ʈ �ʱ�ȭ

        for (int i = 0; i < sizeX; i++)                 //��
        {
            for (int j = 0; j < sizeY; j++)             //��
            {
                bool isWall = false;

                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.5f))   //�ش� ��ġ���� ����Ǵ� ��ü ��������
                {
                    if (col.gameObject.layer == LayerMask.NameToLayer("Wall"))              //����� ��ü�� ���̾ "Wall"�� ���
                    {
                        isWall = true;                                                      //������ üũ
                    }
                }
                NodeArray[i, j] = new ANode(isWall, i + bottomLeft.x, j + bottomLeft.y);    //�ش���ġ�� ��忡 �� ������ ����
            }
        }
    }
    /** ����ȭ�� ��� ã��: A* �˰��� **/
    public void PathFinding()
    {
        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];    //������ġ�� ��� ������ ��������
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y]; //������ġ�� ��� ������ ��������


        //StartNode.G = 0;
        OpenList = new List<ANode>() { StartNode };     //��������Ʈ �ʱ�ȭ: ���۳�� �߰�
        ClosedList = new List<ANode>();                 //��������Ʈ �ʱ�ȭ
        FinalNodeList = new List<ANode>();              //����ȭ�� ��� ����Ʈ �ʱ�ȭ

        while (OpenList.Count > 0)                      //��������Ʈ�� ��尡 �ִ� ��쿡�� ����
        {
            // ��������Ʈ �� ���� F�� �۰� F�� ���ٸ� H�� ���� �� ������� �ϰ� ��������Ʈ���� ��������Ʈ�� �ű��
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
            {
                int openF = OpenList[i].G + OpenList[i].H;
                int curF = CurNode.G + CurNode.H;
                if (openF <= curF && OpenList[i].H < CurNode.H) CurNode = OpenList[i];
            }
            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            //�������� ã�� ���: ���������� ���۳�带 ���� ������ ����� �θ� ã�� �ö� ��θ� ����
            if (CurNode == TargetNode)
            {
                ANode TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();                //����('����-����' --> '����-����')

                return;
            }
            //�밢�� ���� �߰�
            OpenListAdd(CurNode.x + 1, CurNode.y + 1);  // ��
            OpenListAdd(CurNode.x - 1, CurNode.y + 1);  // ��
            OpenListAdd(CurNode.x - 1, CurNode.y - 1);  // ��
            OpenListAdd(CurNode.x + 1, CurNode.y - 1);  // ��

            //���� ���� �߰�
            OpenListAdd(CurNode.x, CurNode.y + 1);      //��
            OpenListAdd(CurNode.x + 1, CurNode.y);      // �� 
            OpenListAdd(CurNode.x, CurNode.y - 1);      //�� 
            OpenListAdd(CurNode.x - 1, CurNode.y);      // ��
        }
    }

    /** �� �� �ִ� ������ �Ǵ�&��� ����ȭ **/
    void OpenListAdd(int checkX, int checkY)
    {
        // �����¿� ������ ����� �ʰ�, ���� �ƴϸ鼭, ��������Ʈ�� ���ٸ�
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // �밢�� ����, �� ���̷� ��� �ȵ�
            if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

            // �ڳʸ� �������� ���� ������, �̵� �߿� �������� ��ֹ��� ������ �ȵ�
            if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;


            // �̿���忡 �߰�(��� : ����=10, �밢��=14)
            ANode NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            // �̵������ �̿����G���� �۰ų� �Ǵ� ��������Ʈ�� �̿���尡 ���ٸ� G, H, ParentNode�� ���� �� ��������Ʈ�� �߰�
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;
                OpenList.Add(NeighborNode);
            }
        }
    }
    /** ����ȭ�� ��� ���� **/
    public List<ANode> Move(Vector2Int _startPos, Vector2Int _targetPos)
    {
        startPos = _startPos;       //������ġ ����
        targetPos = _targetPos;     //������ġ ����

        PathFinding();              //�� ã��

        return FinalNodeList;       //����ȭ�� ��� ����
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrint
{
    private MapData[,] bluePrint;
    public int height, width;
    private int lWeight = 5, rWeight = 5, dWeight = 5, lrWeight = 15,
                ldWeight = 10, rdWeight = 10, lrdWeight = 10,
                vWeight = 10, fWeight = 10;
    private bool hasBluePrint = false;

    // 생성자
    public BluePrint(int height, int width)
    {
        this.height = height;
        this.width = width;
        bluePrint = new MapData[height, width];

        int j;
        for( int i = 0; i < height; i++ )
        {
            for( j = 0; j + 1 < width; j += 2 )
            {
                bluePrint[i, j] = new MapData();
                bluePrint[i, j + 1] = new MapData();
            }

            if( (j + 1) == width )
            {
                bluePrint[i, j] = new MapData();
            }
        }

        bluePrint[0, 0] = new SpetialMapData("Start");
        bluePrint[height - 1, width - 1] = new SpetialMapData("End");
    }

    // 설계도 리턴
    // 템플릿 메소드
    public MapData[,] getBluePrint()
    {
        if( !hasBluePrint )
        {
            createMainRoad();
            fillRemainder();
            hasBluePrint = true;
        }
        return bluePrint;
    }

    private void createMainRoad()
    {
        int[] pos = { 0, 0 };

        while( bluePrint[pos[0], pos[1]].getPrev() != "End" )
        {
            selectDirection(pos);
            System.Threading.Thread.Sleep(1);
        }
    }

    private void selectDirection(int[] pos)
    {
        int flag = 0;
        int num;
        Random.InitState(System.DateTime.Now.Millisecond);

        if( pos[0] + 1 == height )
        {
            // 마지막 줄이면 그냥 오른쪽으로 쭉 가야 됨, 왼쪽으로 가면 안됨
            rightMove(bluePrint, pos);
            flag = 1;
        }

        while( flag == 0 )
        {
            num = Random.Range(1, 6);
            
            switch( num ){
                case 1: // left
                case 2:
                    if( pos[1] - 1 >= 0 &&
                        bluePrint[pos[0], pos[1] - 1].getPrev() == "non" )
                    {
                        leftMove(bluePrint, pos);
                        flag = 1;
                    }
                    break;
                case 3: // down
                    if( pos[0] + 1 < height &&
                        ( bluePrint[pos[0] + 1, pos[1]].getPrev() == "non" ||
                        bluePrint[pos[0] + 1, pos[1]].getPrev() == "End" ) )
                    {
                        downMove(bluePrint, pos);
                        flag = 1;
                    }
                    break;
                default: // right, Random.range에 따라 가중치를 주기 위해 default에 배치
                    if (pos[1] + 1 < width &&
                        bluePrint[pos[0], pos[1] + 1].getPrev() == "non")  // End인지는 검사 필요 없음
                    {
                        rightMove(bluePrint, pos);
                        flag = 1;
                    }
                    break;
            }
        }
    }

    // 셋 중 하나가 뽑힘
    // ----------------------------------------------------------
    private void leftMove(MapData[,] bluePrint, int[] pos)
    {   // 1번
        bluePrint[pos[0], pos[1]].setNow("Left");   // 이동 전 장소
        decideMapType(bluePrint, pos);              // 타입 결정
        pos[1] -= 1;                                // 이동
        bluePrint[pos[0], pos[1]].setPrev("Left");  // 이동 후 장소
    }

    private void rightMove(MapData[,] bluePrint, int[] pos)
    {   // 2번
        bluePrint[pos[0], pos[1]].setNow("Right");  // 이동 전 장소
        decideMapType(bluePrint, pos);              // 타입 결정
        pos[1] += 1;                                // 이동
        bluePrint[pos[0], pos[1]].setPrev("Right"); // 이동 후 장소
    }

    private void downMove(MapData[,] bluePrint, int[] pos)
    {   // 3번
        bluePrint[pos[0], pos[1]].setNow("Vertical");   // 이동 전 장소
        decideMapType(bluePrint, pos);                  // 타입 결정
        pos[0] += 1;                                    // 이동
        bluePrint[pos[0], pos[1]].setPrev("Vertical");  // 이동 후 장소
    }
    // ----------------------------------------------------------

    // ~Move 계열 함수에서 이동 직전에 사용
    private void decideMapType(MapData[,] bluePrint, int[] pos)
    {   
        int num;

        if( bluePrint[pos[0], pos[1]].getPrev() == "Left" &&
            bluePrint[pos[0], pos[1]].getNow() == "Left" )
        {
            // LR, LRD 가능
            num = Random.Range(0, 2);

            switch( num )
            {
                case 0:
                    bluePrint[pos[0], pos[1]].setType(Type.LR);
                    break;
                case 1:
                    bluePrint[pos[0], pos[1]].setType(Type.LRD);
                    break;
            }
        }
        else if( bluePrint[pos[0], pos[1]].getPrev() == "Left" &&
                 bluePrint[pos[0], pos[1]].getNow() == "Vertical" )
        {
            // RD, LRD 가능
            num = Random.Range(0, 2);

            switch (num)
            {
                case 0:
                    bluePrint[pos[0], pos[1]].setType(Type.RD);
                    break;
                case 1:
                    bluePrint[pos[0], pos[1]].setType(Type.LRD);
                    break;
            }
        }
        else if (bluePrint[pos[0], pos[1]].getPrev() == "Right" &&
                 bluePrint[pos[0], pos[1]].getNow() == "Right")
        {
            // LR, LRD 가능
            num = Random.Range(0, 2);

            switch (num)
            {
                case 0:
                    bluePrint[pos[0], pos[1]].setType(Type.LR);
                    break;
                case 1:
                    bluePrint[pos[0], pos[1]].setType(Type.LRD);
                    break;
            }
        }
        else if (bluePrint[pos[0], pos[1]].getPrev() == "Right" &&
                 bluePrint[pos[0], pos[1]].getNow() == "Vertical")
        {
            // LD, LRD 가능
            num = Random.Range(0, 2);

            switch (num)
            {
                case 0:
                    bluePrint[pos[0], pos[1]].setType(Type.LD);
                    break;
                case 1:
                    bluePrint[pos[0], pos[1]].setType(Type.LRD);
                    break;
            }
        }
        else if (bluePrint[pos[0], pos[1]].getPrev() == "Vertical" &&
                 bluePrint[pos[0], pos[1]].getNow() == "Vertical")
        {
            // D, LD, RD, LRD 가능
            num = Random.Range(0, 4);

            switch (num)
            {
                case 0:
                    bluePrint[pos[0], pos[1]].setType(Type.D);
                    break;
                case 1:
                    bluePrint[pos[0], pos[1]].setType(Type.LD);
                    break;
                case 2:
                    bluePrint[pos[0], pos[1]].setType(Type.RD);
                    break;
                case 3:
                    bluePrint[pos[0], pos[1]].setType(Type.LRD);
                    break;
            }
        }
        else if (bluePrint[pos[0], pos[1]].getPrev() == "Vertical" &&
                 bluePrint[pos[0], pos[1]].getNow() == "Left")
        {
            // L, LR 가능
            num = Random.Range(0, 2);

            switch (num)
            {
                case 0:
                    bluePrint[pos[0], pos[1]].setType(Type.L);
                    break;
                case 1:
                    bluePrint[pos[0], pos[1]].setType(Type.LR);
                    break;
            }
        }
        else if (bluePrint[pos[0], pos[1]].getPrev() == "Vertical" &&
                 bluePrint[pos[0], pos[1]].getNow() == "Right")
        {
            // R, LR 가능
            num = Random.Range(0, 2);

            switch (num)
            {
                case 0:
                    bluePrint[pos[0], pos[1]].setType(Type.R);
                    break;
                case 1:
                    bluePrint[pos[0], pos[1]].setType(Type.LR);
                    break;
            }
        }

    }

    // 메인 길을 채우고 남은 곳 채우기
    private void fillRemainder()
    {
        int plusWeight = 8;
        int num;

        for( int i = 0; i < height; i++ )
        {
            for( int j = 0; j < width; j++ )
            {
                if( bluePrint[i, j].getType() != Type.NON )
                {
                    continue;
                }

                // 가중치 조정
                if( j != 0 )
                {
                    Type prevType = bluePrint[i, j - 1].getType();

                    if ( prevType == Type.R || prevType == Type.LR )
                    {
                        lWeight += plusWeight;
                        ldWeight += plusWeight;
                        lrWeight += plusWeight;
                        lrdWeight += plusWeight;
                        vWeight -= plusWeight;
                        fWeight -= plusWeight;
                    }
                    else if( prevType == Type.V )
                    {
                        vWeight += plusWeight;
                    }
                    else if( prevType == Type.F )
                    {
                        fWeight += plusWeight;
                    }
                }

                num = Random.Range(0, lWeight + rWeight + dWeight + lrWeight + rdWeight +
                                      ldWeight + lrdWeight + vWeight + fWeight);

                if( num < lWeight )
                {
                    bluePrint[i, j].setType(Type.L);
                }
                else if( num < lWeight + rWeight )
                {
                    bluePrint[i, j].setType(Type.R);
                }
                else if( num < lWeight + rWeight + dWeight)
                {
                    bluePrint[i, j].setType(Type.D);
                }
                else if( num < lWeight + rWeight + dWeight + lrWeight)
                {
                    bluePrint[i, j].setType(Type.LR);
                }
                else if( num < lWeight + rWeight + dWeight + lrWeight + rdWeight )
                {
                    bluePrint[i, j].setType(Type.RD);
                }
                else if(num < lWeight + rWeight + dWeight + lrWeight + rdWeight +
                                ldWeight)
                {
                    bluePrint[i, j].setType(Type.LD);
                }
                else if(num < lWeight + rWeight + dWeight + lrWeight + rdWeight +
                                ldWeight + lrdWeight)
                {
                    bluePrint[i, j].setType(Type.LRD);
                }
                else if(num < lWeight + rWeight + dWeight + lrWeight + rdWeight +
                                ldWeight + lrdWeight + vWeight)
                {
                    bluePrint[i, j].setType(Type.V);
                }
                else if (num < lWeight + rWeight + dWeight + lrWeight + rdWeight +
                                ldWeight + lrdWeight + vWeight + fWeight)
                {
                    bluePrint[i, j].setType(Type.F);
                }

                // 가중치 원상 복귀
                if (j != 0)
                {
                    Type prevType = bluePrint[i, j - 1].getType();

                    if (prevType == Type.R || prevType == Type.LR)
                    {
                        lWeight -= plusWeight;
                        ldWeight -= plusWeight;
                        lrWeight -= plusWeight;
                        lrdWeight -= plusWeight;
                        vWeight += plusWeight;
                        fWeight += plusWeight;
                    }
                    else if (prevType == Type.V)
                    {
                        vWeight -= plusWeight;
                    }
                    else if (prevType == Type.F)
                    {
                        fWeight -= plusWeight;
                    }
                }
            }
        }
    }
}


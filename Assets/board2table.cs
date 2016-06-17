using UnityEngine;
using System.Collections;
using System.IO;

public class board2table : MonoBehaviour {

    public static int[,] board;
    
    // Use this for initialization
    void Start () {
        board = new int[8, 8];
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public static void print()
    {
        System.IO.File.Delete("board.txt");

        for (int line = 7; line >= 0; line--)
        {
            for (int row = 0; row < 8; row++)
            {
                System.IO.File.AppendAllText("board.txt", " " + board[line, row]);
            }
            System.IO.File.AppendAllText("board.txt", "\n");

        }
    }


    public static void add(string name, bool player)
    {
        //converter linha e coluna para numero chat-2-int
        int row = name[0] -'A'; //letra
        int line = name[1] - '1'; //numero
        
        if (player) board[line, row] = 1;
        else board[line, row] = -1;
        
    }



    /*
    public static void refresh(int _line, int _row, bool playerTF)
    {
        int player;
        if (playerTF) { player = 1; }
        else { player = -1; }
        int ret = 0;

        if (board[_line--, _row] == player * -1)
            ret = count(_line, _row, _line--, _row);
        if (board[_line++, _row] == player * -1)
            ret = count(_line, _row, _line++, _row);
    }




    static int count(int i, int j, int x, int y)
    {
        int c= 0;
        if (j == y)
            if (i > x)
                while (i > x)
                    c += board[i--,j];
            if (i < x)
                while (i < x)
                    c += board[i++, j];
        return c;
    }






    


    public static void validmoves(bool playerTF)
    {
        int player;
        if (playerTF) { player = 1; }
        else { player = -1; }
        int ret = 0;

        for (int row = 0; row < 8; row++)
            for (int line = 0; line < 8; line++) {
                
                if (board[line--, row] == player * -1)
                    for(int i=line;i>-1;i--)
                        if (board[i, row] == player)



            }

                
    }
    */


}

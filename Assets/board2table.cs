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

    public static void refresh(string name, bool player)
    {
        //converter linha e coluna para numero chat-2-int
        int row = name[0] -'A'; //letra
        int line = name[1] - '1'; //numero
        
        if (player) board[line, row] = 1;
        else board[line, row] = -1;
    }


    public static void print()
    {
        System.IO.File.Delete("board.txt");

        for (int line = 7; line >= 0; line--) {
            for (int row = 0; row < 8; row++)
            {
                System.IO.File.AppendAllText("board.txt", " "+board[line, row] );
            }
            System.IO.File.AppendAllText("board.txt", "\n");

        }
    }



    public static void validmoves(bool player)
    {
    }

    }

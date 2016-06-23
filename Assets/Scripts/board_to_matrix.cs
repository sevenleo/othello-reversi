using UnityEngine;
using System.Collections;
using System.IO;

public class board_to_matrix : MonoBehaviour {

    public static int[,] board;

    //PECAS BRANCAS = PLAYER1 = 1
    //PECAS PRETAS = PLAYER2 = -1

    void Start () {
        int i, j, k;
        board = new int[10, 10];

        for (i = 0; i < 10; i++)
        {
            for (j = 0; j < 10; j++)
            {
                board[i, j] = 0; //QUAL VALOR COLOCAR? ZERO NAO INFLUENCIA NO RESULTADO FINAL, ACHO Q ÉA UANICA OPCAO.
            }
        }


        //SE O VALOR DO OUTER FOR IGUAL O EMPTY SÓ PRECISA ITERAR 1 VEZ, SENAO DESCOMENTAR
        /*
        for (i = 1; i < 9; i++)
        {
            for (j = 1; j < 9; j++)
            {
                board[i, j] = 0;//QUADRA VAZIA
            }
        }*/



    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public static void print()
    {
        System.IO.File.Delete("board.txt");

        for (int line = 9; line >= 0; line--)
        {
            for (int row = 0; row < 10; row++)
            {
                System.IO.File.AppendAllText("board.txt", "\t" + board[line, row]);
            }
            System.IO.File.AppendAllText("board.txt", "\n");

        }
    }


    public static void add(string name, bool player)
    {
        //converter linha e coluna para numero chat-2-int
        int row = name[0] -'A'; //letra
        int line = name[1] - '1'; //numero

        row++;
        line++;
        if (player) board[line, row] = 1;
        else board[line, row] = -1;

        print();
    }

}

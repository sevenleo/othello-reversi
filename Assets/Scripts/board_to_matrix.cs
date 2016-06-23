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


    public static bool add(string name, bool player)
    {
        //converter linha e coluna para numero chat-2-int
        int row = name[0] -'A'; //letra
        int line = name[1] - '1'; //numero

        row++;
        line++;

        if (verifymove(line, row, player))
        {
            if (player) board[line, row] = 1;
            else board[line, row] = -1;
            return true;
        }
        else return false;

        print();
    }



    static bool verifymove(int line, int row, bool player)
    {
        if (line == row) return true;
        return false;
    }



    /*
     def valid_moves(self, color):
    ret = []
    for i in range(1, 9):
      for j in range(1, 9):
        if self.board[i][j] == Board.EMPTY:
          for direction in Board.DIRECTIONS:
            move = Move(i, j)
            bracket = self._find_bracket(move, color, direction)
            if bracket:
              ret += [move]
    return ret
     */


    /*
       def _find_bracket(self, move, color, direction):
    bracket = [move.x + direction[0], move.y + direction[1]]
    bracket_color = self.board[bracket[0]][bracket[1]]

    if bracket_color == color:
      return None
    opponent = self._opponent(color)
    while bracket_color == opponent:
      bracket = [bracket[0] + direction[0], bracket[1] + direction[1]]
      bracket_color = self.board[bracket[0]][bracket[1]]

    return None if self.board[bracket[0]][bracket[1]] in (Board.OUTER, Board.EMPTY) else bracket

  def _opponent(self, color):
    return Board.BLACK if color is Board.WHITE else Board.WHITE
     */

}

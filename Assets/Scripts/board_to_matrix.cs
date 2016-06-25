using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

public struct position
{
    public int x, y;

    public position(int a, int b)
    {
        x = a;
        y = b;
    }

    public string toString()
    {
        return this + ": " + this.x + " " + this.y;
    }
}


public class board_to_matrix : MonoBehaviour {

    
    //public static direction[] directions;
    public static int[,] board;
    public static bool Turn = true;

    //Direcoes
    public static List<position> directions = new List<position>();
    position UP = new position(-1,0);
    position DOWN = new position(1,0);
    position LEFT = new position(0,-1);
    position RIGHT = new position(0,1);
    position UP_RIGHT = new position(-1,1);
    position DOWN_RIGHT = new position(1,1);
    position DOWN_LEFT = new position(1,-1);
    position UP_LEFT = new position(-1,-1);

    //PECAS BRANCAS = TurnPlayer1 = 1
    //PECAS PRETAS = TurnPlayer2 = -1


    void Start () {
        
        board = new int[10, 10];

        board[4, 4] = 1;
        board[4, 5] = 1;
        board[5, 4] = -1;
        board[5, 5] = -1;

        directions.Add(UP);
        directions.Add(DOWN);
        directions.Add(LEFT);
        directions.Add(RIGHT);
        directions.Add(UP_RIGHT);
        directions.Add(DOWN_RIGHT);
        directions.Add(DOWN_LEFT);
        directions.Add(UP_LEFT);


        int i, j;
        

        for (i = 0; i < 10; i++)
        {
            for (j = 0; j < 10; j++)
            {
                board[i, j] = 0; //QUAL VALOR COLOCAR? ZERO NAO INFLUENCIA NO RESULTADO FINAL, ACHO Q ÉA UANICA OPCAO.
            }
        }


        //ENQUANTO O VALOR DO OUTER FOR IGUAL O EMPTY SÓ PRECISA ITERAR 1 VEZ, SENAO DESCOMENTAR
        /*
        for (i = 1; i < 9; i++)
        {
            for (j = 1; j < 9; j++)
            {
                board[i, j] = 0;//QUADRA VAZIA
            }
        }*/



    }
	
	void Update () {
    }

    public static void print()
    {
        File.Delete("board.txt");
        File.AppendAllText("board.txt", "Turno:" + (Turn? "P1" : "P2") + "\n");

        for (int line = 9; line >= 0; line--)
        {
            for (int row = 0; row < 10; row++)
            {
                if ( valid_moves().Contains(new position(line, row)) )
                    File.AppendAllText("board.txt", "\t" + "[]");
                else
                    File.AppendAllText("board.txt", "\t" + board[line, row]);
                
            }
            File.AppendAllText("board.txt", "\n");

        }

        
    }


    public static string matrix2board(position p)
    {
        char letra = '@'; //letra  ///////////////////////////////VERIFICAR SE O ASCII DO LINUX É IGUAL E ANTES DO A VEM O @
        char numero = '0'; //numero

        letra += (char)p.x ; //letra
        numero += (char)p.y; //numero
        return ""+letra+numero;
    }


    public static void startwith(string name,bool player)
    {

        //converter linha e coluna para numero chat-2-int
        int row = name[0] - 'A'; //letra
        int line = name[1] - '1'; //numero

        row++;
        line++;

        if (player) board[line, row] = 1;
        else board[line, row] = -1;
        print();
        
  
    }

    public static bool add(string name)
    {
        
        //converter linha e coluna para numero chat-2-int
        int row = name[0] -'A'; //letra
        int line = name[1] - '1'; //numero

        row++;
        line++;

        if (verifymove(line, row))        {
            if (Turn) board[line, row] = 1;
            else board[line, row] = -1;
            Turn = !Turn;
            print();
            
            return true;
        }
        else
        {
            print();
            return false;
        }
    }


    public static List<position> all_moves()
    {
        List<position> moves = new List<position>();

        int i, j;
        for (i = 1; i < 9; i++)
        {
            for (j = 1; j < 9; j++)
            {
                            moves.Add(new position(i,j));
            }
        }
        return moves;
    }


    public static List<position> all_empty()
    {
        List<position> moves = new List<position>();

        int i, j;
        for (i = 1; i < 9; i++)
        {
            for (j = 1; j < 9; j++)
            {
                if (board[i, j] == 0)
                    moves.Add(new position(i, j));
            }
        }
        return moves;
    }


    public static List<position> not_valid_moves() {
        List<position> not_valid_moves = board_to_matrix.all_moves();
        not_valid_moves.RemoveAll(item => board_to_matrix.valid_moves().Contains(item));
        return not_valid_moves;
    }


    static bool verifymove(int line, int row)
    {
        if (valid_moves().Contains(new position(line, row))) return true;
        else return false;
    }


    public static List<position> valid_moves()
    {
        List<position> moves = new List<position>();
        
        int i, j;
        for (i = 1; i < 9; i++)
        {
            for (j = 1; j < 9; j++)
            {
                if (board[i, j] == 0) {
                        directions.ForEach(direction => {
                            position? bracket;
                            bracket = find_bracket(i, j, direction);
                            if (bracket.HasValue) {
                                moves.Add(new position(i,j));
                            }
                        });
                }
            }
        }
        return moves;
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



    public static position? find_bracket(int x, int y, position direction)
    {
        int color, opponent;
        if (Turn) { color = 1; opponent = -1; }
        else { color = -1; opponent = 1; }

        position bracket = new position(x + direction.x, y + direction.y);
        int bracket_color = board[bracket.x, bracket.y];

        if (bracket_color == color) return null;
        else
        {
            while (bracket_color == opponent)
            {
                bracket = new position(bracket.x + direction.x, bracket.y + direction.y);
                bracket_color = board[bracket.x, bracket.y];
            }
            if (board[bracket.x, bracket.y] == 0) return null;
            else return bracket;
            
        }
    }


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

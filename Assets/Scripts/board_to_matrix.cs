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

    public bool equals(position item)
    {
        if (x==item.x && y==item.y) return true;
        else return false;
    }
}


public class board_to_matrix : MonoBehaviour {

    
    //public static direction[] directions;
    public static int[,] main_board;
    public static bool Turn = true;

    //quem esta jogando
    public static bool p1 = true;
    public static bool p2 = true;


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
        main_board = new int[10, 10];

        main_board[4, 4] = 1;
        main_board[4, 5] = 1;
        main_board[5, 4] = -1;
        main_board[5, 5] = -1;

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
                main_board[i, j] = 0; //QUAL VALOR COLOCAR? ZERO NAO INFLUENCIA NO RESULTADO FINAL, ACHO Q ÉA UANICA OPCAO.
            }
        }
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
                if ( valid_moves(board_to_matrix.main_board).Contains(new position(line, row)) )
                    File.AppendAllText("board.txt", "\t" + "[]");
                else
                    File.AppendAllText("board.txt", "\t" + main_board[line, row]);
            }
            File.AppendAllText("board.txt", "\n");
        }
    }


    public static int CalculateBoardValue(int[,] board)
    {
        int value = 0;

        for (int line = 9; line >= 0; line--)
        {
            for (int row = 0; row < 10; row++)
            {
                if (board[line, row] == 1) value++;
                if (board[line, row] == -1) value--;
            }
        }
        return value;
    }

    public static void playersscore(int[] scores)
    {
        
        scores[0] = 0;
        scores[1] = 0;
        scores[2] = 0;

        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                if (main_board[i, j] == 1)
                    scores[1]++;
                else if (main_board[i, j] == -1)
                    scores[2]++;
                else
                    scores[0]++;
            }
        }

    }


    public static string matrix2board(position p)
    {
        ////////// VERIFICAR SE O ASCII DO LINUX É IGUAL E ANTES DO A VEM O @
        char letra = '@'; //letra  
        char numero = '0'; //numero

        letra += (char)p.y ; //letra
        numero += (char)p.x; //numero
        return ""+letra+numero;
    }


    public static void startwith(string name,bool player)
    {

        //converter linha e coluna para numero chat-2-int
        int row = name[0] - 'A'; //letra
        int line = name[1] - '1'; //numero

        row++;
        line++;

        if (player) main_board[line, row] = 1;
        else main_board[line, row] = -1;
        print();
        
  
    }


    public static bool add(int[,] board,string name, List<position> changed,bool player, bool mainboard=false)
    {
        
        //converter linha e coluna para numero chat-2-int
        int row = name[0] -'A'; //letra
        int line = name[1] - '1'; //numero

        row++;
        line++;

        if (verifymove(board,line, row))        {
            if (Turn) board[line, row] = 1;
            else board[line, row] = -1;
            reverse(board, line, row, changed);
            if(mainboard == false)
                player = !player;
            else
                board_to_matrix.Turn = !board_to_matrix.Turn;

            print();
            return true;
        }
        else
        {
            print();
            return false;
        }
    }


    public static List<position> all_moves(int[,] board)
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


    public static List<position> all_empty(int[,] board)
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


    public static List<position> not_valid_moves(int[,] board) {
        List<position> not_valid_moves = board_to_matrix.all_moves(board);
        not_valid_moves.RemoveAll(item => board_to_matrix.valid_moves(board).Contains(item));
        return not_valid_moves;
    }



    static bool verifymove(int[,] board,int line, int row)
    {
        if (valid_moves(board).Contains(new position(line, row))) return true;
        else return false;
    }



    public static List<position> valid_moves(int[,] board)
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
                            bracket = find_bracket(board, i, j, direction);
                            if (bracket.HasValue) {
                                if ( !moves.Contains(new position(i, j) ) )
                                    moves.Add(new position(i,j));
                            }
                        });
                }
            }
        }
        return moves;
    }

    
    public static position? find_bracket(int[,] board,int x, int y, position direction)
    {
        int opponent;
        if (Turn) { opponent = -1; }
        else { opponent = 1; }

        position bracket = new position(x + direction.x, y + direction.y);
        int bracket_color = board[bracket.x, bracket.y];

        if (bracket_color == opponent)
        {
            while (bracket_color == opponent)
            {
                bracket = new position(bracket.x + direction.x, bracket.y + direction.y);
                bracket_color = board[bracket.x, bracket.y];
            }
            if (board[bracket.x, bracket.y] == 0) return null;
            else
            {
                return bracket;
            }

        }
        else return null;
    }


    public static void reverse(int[,] board,int i, int j, List<position> changed)
    {
      
        directions.ForEach(direction =>
        {
            make_flips(board, i, j, direction, changed);
        });
        //Debug.Log("LISTA >> " + changed.Count);
    }


    
    public static void make_flips(int[,] board,int x,int y, position direction, List<position> changed)
    {
        position? bracket = find_bracket(board, x, y, direction);
        

        if (bracket.HasValue)
        {
            //Debug.Log("BRACKET YES");
            //Debug.Log( x + " " + y + " = " + board[x, y]+"\n ate\n");
            //Debug.Log( ((position)bracket).x + " " + ((position)bracket).y + " = " + board[((position)bracket).x, ((position)bracket).y]);
            position square = new position(x + direction.x, y + direction.y);
            while (!square.equals((position)bracket))
            {
                board[square.x, square.y] = (Turn ? 1 : -1);
                changed.Add(new position(square.x, square.y));
                square = new position(square.x + direction.x, square.y + direction.y);
            }
        }
        //else //Debug.Log("BRACKET no");
    }
    

    public void changeplayer1()
    {
        p1 = !p1;
    }


    public void changeplayer2()
    {
        p2 = !p2;
    }


    public static void replay()
    {
        p1 = true;
        p2 = true;
        Turn = true;

        main_board = new int[10, 10];

        int i, j;


        for (i = 0; i < 10; i++)
        {
            for (j = 0; j < 10; j++)
            {
                main_board[i, j] = 0; //QUAL VALOR COLOCAR? ZERO NAO INFLUENCIA NO RESULTADO FINAL, ACHO Q ÉA UANICA OPCAO.
            }
        }

    }

}
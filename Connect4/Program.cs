using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    /*
     * James A. Stewart
     * August 2017
     * 
     * 
     * Connect 4
     * Console interface
     * 
     * 
     * map of board:
     * 
     *      |[0,5]|[1,5]|[2,5]|[3,5]|[4,5]|[5,5]|[6,5]|
     *      |-----------------------------------------| 
     *      |[1,4]|[1,4]|[2,4]|[3,4]|[4,4]|[5,4]|[6,4]|
     *      |-----------------------------------------|
     *       .
     *       .
     *       .
     *      |[0,0]|[1,0]|[2,0]|[3,0]|[4,0]|[5,0]|[6,0]|
     *      |-----------------------------------------|
     *
     *      so [0,0] is at the bottom of the board, and [0,1] is directly above that spot.
     *      
     * Player goes first.
     * 
     * pieces are 'p' or 'c' for the player and computer, respectively.
     * 
     *
     */
    class Program
    {

        class Game
        {
            char[,] map;//  char[7,6] represents the board.
            int turns;
            const int max_turns = 42;


            public const char empty = ' ';

            public Game()       //constructor
            {
                map = new char[7, 6];   //allocate memory
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        map[i, j] = empty;      //fill with empty indicators.  a blank board
                    }
                }
                turns = 0;      //0 turns have been taken.  42 is max number of turns (7*6)
            }

            // other methods
            public bool move(int x, char mover)
            {
                //  Does not assume valid input, but ignores bad input.
                //  Returns true if the move was successful.  If bad input or the column is full, returns false

                bool valid = false;
                if ((mover == User.player) || (mover == Computer.computer))      //check that the mover is a valid choice
                {
                    if ((x >= 0) && (x < 7))        //check that x is in range
                    {
                        int y = 0;
                        while (map[x, y] != empty)
                        {
                            y++;                //find the first empty spot in column x
                            if (y == 6) { break; }  //in case the column is full
                        }
                        if (y < 6)
                        {
                            map[x, y] = mover;     //assign this spot to player
                            turns++;
                            valid = true;
                        }
                    }
                }
                return valid;
            }

            static public void draw_map(Game g)
            {
                string separator = "\t|---------------------------|\n";
                Console.Write("\n\n" + separator);   //top line
                for (int y = 5; y >= 0; y--)
                {
                    Console.Write("\t|");
                    for (int x = 0; x < 7; x++)
                    {
                        Console.Write(" {0} |", g.map[x, y]);
                    }
                    Console.Write("\n");
                    Console.Write(separator);
                }
            }

            public bool check_for_win()
            {
                bool win = false;
                //do check here.
                return win;
            }

            public void display_win(char game_player, Game g)
            {
                switch (game_player)
                {
                    case User.player:
                        Game.draw_map(g);
                        Console.Write("\nCongratulations!  You won!\n");
                        break;
                    case Computer.computer:
                       Game.draw_map(g);
                        Console.Write("\nI'm sorry, but yoou lost.\n");
                        break;
                    default:
                        Console.Write("What?  This should't have happerned.  Who won?");
                        break;
                }
            }
            public bool check_col(int x, Game g)
            {                               //returns true if column is not full.  False if it is full.
                bool full = false;
                if ((x >= 0) && (x < 7))
                {
                    if (g.map[x, 5] == empty)
                    {
                        full = true;
                    }
                }
                return full;
            }
            public int get_turns()
            {
                return turns;
            }

            public int get_max_turns()
            {
                return max_turns;
            }

            public void display_draw(Game g)
            {
                Game.draw_map(g);
                Console.Write("It's a draw!");                
            }

        }
            public abstract class Player
            {
                abstract public void take_turn();
            }


        class User : Player
        {
            public const char player = 'p';
            Game g;

            public User(Game game) { g = game; }

            public override void take_turn()
            {
                bool valid = false;
                bool good_input = false;
                int x;
                string s;

                while (!valid)
                {
                    do
                    {
                        Game.draw_map(g);
                        Console.Write("It's your turn.  Drop a token in which column? (1..7)>");
                        s = Console.ReadLine();
                        if (int.TryParse(s, out x)) 
                        {
                            if ((x >= 0) && (x <= 7
                                ))
                            {
                                if (g.check_col(x-1, g))
                                {
                                    good_input = true;
                                } else  // if the column is full
                                {
                                    Console.Write("Column {0} is full.  Please choose another.\n", x);
                                }
                            } else  //if not a number or in range
                            {
                                Console.Write("Please use the digits 1 through 7.\n");
                            }
                        }
                    } while (!good_input);
                    g.move(x-1, player);
                    valid = true;
                }
            }
        }

        class Computer : Player
        {
            public const char computer = 'c';
            Game g;

            public Computer(Game game) { g = game; }

            public override void take_turn()
            {

            }
        }
        
        static void Main(string[] args)
        {
            Game g = new Game();        //keeps the game state, and is initialized by the constructor, so we are already good to go
            User p = new User(g);        //represents the user player
            Computer c = new Computer(g);//represents the computer player

            //for debug use
            Game.draw_map(g);
            do
            {
                p.take_turn();
                if (g.get_turns() == g.get_max_turns())
                {
                    g.display_draw(g);
                    break;
                }
            } while (g.get_turns() < g.get_max_turns());


            /*  main loop
            while (g.get_turns() < g.get_max_turns())
            {
                p.take_turn();
                if (g.check_for_win())
                {
                    g.display_win(User.player);
                }
                c.take_turn();
                if (g.check_for_win())
                {
                    g.display_win(Computer.computer);
                }
            } */
            Console.ReadKey();
        }
    }
}

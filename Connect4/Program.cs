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
            bool win = false;

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
                win = false;
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
                            check_for_win(x,y);        //check to see if this move was a winner

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

            void check_for_win(int x, int y)
            {
                int i = 0;
                int j = 0;
                char token = map[x,y];  //x and y are good coords from the move method
                //bool wins = false;
                int[] lengths = { 0, 0, 0, 0, 0, 0, 0, 0 };  //holds the length of matching locations on the map in each direction
                /*
                lengths[0]=up
                       [1]=up-right
                       [2]=right
                       [3]=down-right
                       [4]=down
                       [5]=down-left
                       [6]=left
                       [7]=up-left
                */
                //check up
                for (int c = 1; c <= 3; c++)     //only need to check up to 3 spaces in each direction
                {   
                    if ((y + c) < 6)
                    {
                        if (map[x, y + c] == token)
                        {
                            lengths[0]++;
                        } else
                        {
                            break;  //it doesn't match, so the streak ends here
                        }
                    }
                    else
                    {
                        break;  //we reached the edge of the map.  no need to keep going.
                    }
                }
                //check up-right
                for (int c = 1; c <= 3; c++)
                {
                    if (((x + c) < 7) && ((y + c) < 6))
                    {
                        if (map[x+c,y+c]==token)
                        {
                            lengths[1]++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //check right
                for (int c = 1; c <= 3; c++)
                {
                    if ((x + c) < 7) 
                    {
                        if (map[x + c, y] == token)
                        {
                            lengths[2]++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //check down-right
                for (int c = 1; c <= 3; c++)
                {
                    if (((x + c) < 7) && ((y -c) >=0 ))
                    {
                        if (map[x + c, y - c] == token)
                        {
                            lengths[3]++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //check down
                for (int c = 1; c <= 3; c++)     
                {
                    if ((y - c) >= 0)
                    {
                        if (map[x, y - c] == token)
                        {
                            lengths[4]++;
                        }
                        else
                        {
                            break;  
                        }
                    }
                    else
                    {
                        break;  
                    }
                }
                //check down-left
                for (int c = 1; c <= 3; c++)
                {
                    if (((x - c) >= 0) && ((y - c) >= 0))
                    {
                        if (map[x - c, y - c] == token)
                        {
                            lengths[5]++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //check left
                for (int c = 1; c <= 3; c++)
                {
                    if ((x - c) >= 0)
                    {
                        if (map[x - c, y] == token)
                        {
                            lengths[6]++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //check up-left
                for (int c = 1; c <= 3; c++)
                {
                    if (((x - c) >= 0) && ((y + c) < 6))
                    {
                        if (map[x - c, y + c] == token)
                        {
                            lengths[7]++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //check the lengths
                /*
                lengths[0]=up
                       [1]=up-right
                       [2]=right
                       [3]=down-right
                       [4]=down
                       [5]=down-left
                       [6]=left
                       [7]=up-left
                */
                //up + down (+1 for this spot)  if its 4 or more its a win
                if (lengths[0] + lengths[4] +1 >= 4)
                {
                    win = true;
                }
                //up-right + down-left
                if (lengths[1] + lengths[5] + 1 >= 4)
                {
                    win = true;
                }
                //right + left
                if (lengths[2] + lengths[6] + 1 >= 4)
                {
                    win = true;
                }
                //down-right + up-left
                if (lengths[3] + lengths[7] + 1 >= 4)
                {
                    win = true;
                }
            }

            public bool get_win() { return win; }

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
                        Console.Write("\nI'm sorry, but you lost.\n");
                        break;
                    default:
                        Console.Write("What?  This should't have happened.  Who won?");
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

            public int check_move(int column, char token)
            {
                //this function does essentially what Game.check_for_win() does, but for a blank space, and for a specified side (computer or player).
                //It checks the first blank space going up column column.  This is called before making a move.
                //This function returns the longest streak of mathing squares that would result in moving here.  4 or more = win!
                //minimum good value is 1, the space in question by itself.  max is 7, but unlikely.
                //returns 0 if column is full.
                int len = 0;
                int[] lens = new int[8];
                for (int z = 0; z < 8; z++) { lens[z] = 0; }
                //find row
                int row = 0;
                while(map[column,row]!=' ') { row++; if (row == 6) { break; } }
                if (row == 6) { return len; }
                //do the checks
                //check up
                for (int c = 1; c <= 3; c++)     //only need to check up to 3 spaces in each direction
                {
                    if ((row + c) < 6)
                    {
                        if (map[column, row + c] == token)
                        {
                            lens[0]++;
                        }
                        else
                        {
                            break;  //it doesn't match, so the streak ends here
                        }
                    }
                    else
                    {
                        break;  //we reached the edge of the map.  no need to keep going.
                    }
                }
                //check up-right
                for (int c = 1; c <= 3; c++)
                {
                    if (((column + c) < 7) && ((row + c) < 6))
                    {
                        if (map[column + c, row + c] == token)
                        {
                            lens[1]++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //check right
                for (int c = 1; c <= 3; c++)
                {
                    if ((column + c) < 7)
                    {
                        if (map[column + c, row] == token)
                        {
                            lens[2]++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //check down-right
                for (int c = 1; c <= 3; c++)
                {
                    if (((column + c) < 7) && ((row - c) >= 0))
                    {
                        if (map[column + c, row - c] == token)
                        {
                            lens[3]++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //check down
                for (int c = 1; c <= 3; c++)
                {
                    if ((row - c) >= 0)
                    {
                        if (map[column, row - c] == token)
                        {
                            lens[4]++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //check down-left
                for (int c = 1; c <= 3; c++)
                {
                    if (((column - c) >= 0) && ((row - c) >= 0))
                    {
                        if (map[column - c, row - c] == token)
                        {
                            lens[5]++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //check left
                for (int c = 1; c <= 3; c++)
                {
                    if ((column - c) >= 0)
                    {
                        if (map[column - c, row] == token)
                        {
                            lens[6]++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //check up-left
                for (int c = 1; c <= 3; c++)
                {
                    if (((column - c) >= 0) && ((row + c) < 6))
                    {
                        if (map[column - c, row + c] == token)
                        {
                            lens[7]++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //check the lengths
                /*
                lengths[0]=up
                       [1]=up-right
                       [2]=right
                       [3]=down-right
                       [4]=down
                       [5]=down-left
                       [6]=left
                       [7]=up-left
                */
                // take the largest
                //up + down 
                if ((lens[0] + lens[4])>len) { len = lens[0] + lens[4]; }
               
                //up-right + down-left
                if ((lens[1] + lens[5])>len) { len = lens[1] + lens[5]; }
                
                //right + left
                if ((lens[2] + lens[6]) > len) { len = lens[2] + lens[6]; }
                
                //down-right + up-left
                if ((lens[3] + lens[7]) > len) { len = lens[3] + lens[7]; }
                
                return len+1;
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
            public const char enemy = 'p';

            Game g;

            public Computer(Game game) { g = game; }

            public override void take_turn()
            {
                int[] moves = { 0, 0, 0, 0, 0, 0, 0 };  //score for moving into each column
                int selected_column = 0;
                int[] choices = new int[7];          //will hold the choice or choices  for where to move.

                for (int x = 0; x < 7; x++) 
                {
                    //first, see which move gives best chance at a win
                    int len = g.check_move(x, computer);
                    switch (len)
                    {
                        case 0:     //column is full.  Can not move here.
                            moves[x] = -1;
                            break;
                        case 1:     //nothing around it.  not great.
                            moves[x] = 25;
                            break;
                        case 2:     //there is a match, but nothing spectacular.  only 2 in row.
                            moves[x] = 50;
                            break;
                        case 3:     //there is some real possibility here.  3 in row.
                            moves[x] = 75;
                            break;
                        case 4:     //this is a win!  go here!
                            moves[x] = 1000;
                            break;
                        default:    //len is either negative or greater than 4 -- awesome or broken.
                            if (len > 4)
                            {
                                moves[x] = 1000;  //an even better win
                            } else
                            {
                                moves[x] = -1;  //I don't know hpw we got here, but don't move here
                            }
                            break;
                    }
                    // now see which moves benefit the enemy
                    len = g.check_move(x, enemy);
                    switch (len)
                    {
                        case 0:     //column is full.  Can not move here.
                            //moves[x] = -1;
                            break;
                        case 1:     //nothing around it.  not great.
                            if (moves[x] > 25) { moves[x] -= 25; } else { moves[x] = 0; }
                            break;
                        case 2:     //there is a match, but nothing spectacular.  only 2 in row.
                            if (moves[x] > 50) { moves[x] -= 50; } else { moves[x] = 0; }
                            break;
                        case 3:     //there is some real possibility here.  3 in row.
                            if (moves[x] > 75) { moves[x] -= 75; } else { moves[x] = 0; }
                            break;
                        case 4:     //this is a win for the other guy!  Go here to block!
                            moves[x] = 1000;
                            break;
                        default:    //len is either negative or greater than 4 -- awesome or broken.
                            if (len > 4)
                            {
                                moves[x] = 1000;  //an even better win for the other guy-->  bigger block.
                            }
                            else
                            {
                                moves[x] = -1;  //I don't know hpw we got here, but don't move here
                            }
                            break;
                    }
                }
                //now take the best.
                int max = 0;
                int count = 0;
                int choice = 0;
                for (int i = 0; i < 7; i++) { if (moves[i] > max) { max = moves[i]; } }     //get the highest score in max
                //couont how many moves scored that high.  Put the index into choices
                for (int j = 0; j < 7; j++) { if (moves[j] == max) { count++; choices[count - 1] = j; } }           
                if (count > 1)
                {
                    //we have more than one move that scored well.  we will pick at random from among these moves.
                    Random r = new Random();
                    choice  = (int)(r.NextDouble() * 1000) % count;  //this should return an int from 0 to count-1.  that is the index in choices that we want.
                    selected_column = choices[choice];
                }
                else    //if there is only one
                {
                    selected_column = choices[0];
                }
                g.move(selected_column, computer);
            }
                       
        }
        
        static void Main(string[] args)
        {
            Game g = new Game();        //keeps the game state, and is initialized by the constructor, so we are already good to go
            User p = new User(g);        //represents the user player
            Computer c = new Computer(g);//represents the computer player
           
            //  main loop
            while (g.get_turns() < g.get_max_turns())
            {
                p.take_turn();
                if (g.get_win())
                {
                    g.display_win(User.player, g);
                    break;
                }
                c.take_turn();
                if (g.get_win())
                {
                    g.display_win(Computer.computer, g);
                    break;
                }
            } 
            Console.ReadKey();
        }
    }
}

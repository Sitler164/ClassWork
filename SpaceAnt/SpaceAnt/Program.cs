using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace SpaceAnt
{
    struct coordinates
    {
        private int x;
        private int y;

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }
    }

    enum MoveStateForSolider
    {
        up = 4,
        down = 4,
        left = 4,
        right = 4
    }



    class AntWorker : Ant
    {
        public AntWorker(coordinates a)
            : base(a, "worker")
        {

        }

        public override void DoWork()
        {
            coordinate.X = random.Next(100);
            coordinate.Y = random.Next(100);

            if (random.Next(100) < 5)
            {
                Console.WriteLine("found food");
                RunToBase();
            }
        }

        public override void RunToBase()
        {
            coordinate.X = 5;
            coordinate.Y = 5;
        }

        public override string Job
        {
            get { return job; }
        }

        public override coordinates Cor
        {
            get { return coordinate; }
        }
    }

    class AntSoldier : Ant
    {
        public AntSoldier(coordinates a) : base(a, "soldier")
        {
        }

        public override void RunToBase()
        {
            coordinate.X = 5;
            coordinate.Y = 5;

            coordinate.Y += (int)MoveStateForSolider.up;
            for (int i = 0; i < 2; i++)
            {
                coordinate.X += (int)MoveStateForSolider.right;
                coordinate.Y += (int)MoveStateForSolider.down;
                coordinate.Y += (int)MoveStateForSolider.down;
                coordinate.X += (int)MoveStateForSolider.left;
                coordinate.X += (int)MoveStateForSolider.left;
                coordinate.Y += (int)MoveStateForSolider.up;
                coordinate.Y += (int)MoveStateForSolider.up;
                coordinate.X += (int)MoveStateForSolider.right;
            }
        }

        public override string Job
        {
            get { return job; }
        }

        public override coordinates Cor
        {
            get { return coordinate; }
        }
    }

    class State
    {
        private int amountAnts;
        public List<string> jobs;
        public List<coordinates> c;

        public int Amount
        {
            get
            {
                return amountAnts;
            }

            set
            {
                amountAnts = value;
            }

        }
    }


    class Memento
    {
        private State s;

        public Memento(State buf)
        {
            s = buf;
        }

        public State S
        {
            get { return s; }
            set { s = value; }
        }
    } // 

    class Caretaker
    {
        public Stack<Memento> arr;

        public Stack<Memento> Arr
        {
            get { return arr; }
            set { arr = value; }
        }
    } // 



    class Swarm
    {
        private coordinates c;
        private List<Ant> lst;

        private State s; //

        public Swarm()
        {
            c.X = 5;
            c.Y = 5;

            lst = new List<Ant>();
        }



        public void SetMemento(Memento m)
        {
            s = m.S;

            lst = null;
            lst = new List<Ant>();

            for (int i = 0; i < s.Amount; i++)
                lst.Add(new Ant(s.c[i], s.jobs[i]));
        } //

        public Memento CreateMemento()
        {
            s.Amount = lst.Count;

            for (int i = 0; i < lst.Count; i++)
                s.c.Add(lst[i].Cor);

            for (int i = 0; i < lst.Count; i++)
                s.jobs.Add(lst[i].Job);

            return new Memento(s);
        } // 



        public void GrowWorker()
        {
            lst.Add(new AntWorker(c));
        }

        public void GrowSoldier()
        {
            lst.Add(new AntSoldier(c));
        }

        public void MoveAll()
        {
            for (int i = 0; i < lst.Count; i++)
                lst[i].Move();
        }

        public void ToBaseAll()
        {
            for (int i = 0; i < lst.Count; i++)
                lst[i].RunToBase();
        }

        public void DoWorkAll()
        {
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].Job == "worker")
                    lst[i].DoWork();
            }
        }

        public void Show()
        {
            for (int i = 0; i < lst.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(lst[i].Cor.X, lst[i].Cor.Y);
                Console.Write(".*.");
                Console.ResetColor();
            }
        }

    }



    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Swarm a = new Swarm();

            a.GrowWorker();
            a.GrowWorker();
            while(true)
            {
                a.MoveAll();
                a.Show();
                Thread.Sleep(200);

                Console.Clear();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAnt
{
    struct coords
    {
        public int x;
        public int y;
    }

    enum MoveState
    {
        up = 4,
        down = 4,
        left = 4,
        right = 4
    }

    class Ant
    {
        protected string job;
        protected coords c;
        protected Random rnd = new Random();

        public Ant(coords c, string j)
        {
            this.c = c;
            job = j;
        }

        public virtual void Move()
        {
            int dir = rnd.Next(0, 4);
            int step = rnd.Next(1, 10);
            for (int i = 0; i < step; i++)
            {
                switch (dir)
                {
                    case 0:
                        c.x += 1;
                        break;
                    case 1:
                        c.y += 1;
                        break;
                    case 2:
                        c.y += 1;
                        break;
                    case 3:
                        c.x += 1;
                        break;
                }
            }
        }

        public virtual void RunToBase() { }
        public virtual void DoWork() { }

        public virtual coords Cor
        {
            get { return c; }
        }

        public virtual string Job
        {
            get { return job; }
        }
    }

    class AntWorker : Ant
    {
        public AntWorker(coords a)
            : base(a, "worker")
        {

        }

        public override void DoWork()
        {
            c.x = rnd.Next(100);
            c.y = rnd.Next(100);

            if (rnd.Next(100) < 5)
            {
                Console.WriteLine("found food");
                RunToBase();
            }
        }

        public override void RunToBase()
        {
            c.x = 5;
            c.y = 5;
        }

        public override string Job
        {
            get { return job; }
        }

        public override coords Cor
        {
            get { return c; }
        }
    }

    class AntSoldier : Ant
    {
        public AntSoldier(coords a) : base(a, "soldier")
        {
        }

        public override void RunToBase()
        {
            c.x = 5;
            c.y = 5;

            c.y += (int)MoveState.up;
            for (int i = 0; i < 2; i++)
            {
                c.x += (int)MoveState.right;
                c.y += (int)MoveState.down;
                c.y += (int)MoveState.down;
                c.x += (int)MoveState.left;
                c.x += (int)MoveState.left;
                c.y += (int)MoveState.up;
                c.y += (int)MoveState.up;
                c.x += (int)MoveState.right;
            }
        }

        public override string Job
        {
            get { return job; }
        }

        public override coords Cor
        {
            get { return c; }
        }
    }

    class State
    {
        private int amountAnts;
        public List<string> jobs;
        public List<coords> c;

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
        private coords c;
        private List<Ant> lst;

        private State s; //

        public Swarm()
        {
            c.x = 5;
            c.y = 5;

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
                Console.WriteLine("Number: " + i);
                Console.WriteLine("Job: " + lst[i].Job);
                Console.WriteLine("Coords: " + lst[i].Cor.x + " " + lst[i].Cor.y);
            }
        }

    }



    class Program
    {
        static void Main(string[] args)
        {

            Swarm a = new Swarm();

            a.GrowWorker();
            a.GrowWorker();
            a.GrowSoldier();
            a.MoveAll();

            a.Show();
        }
    }
}

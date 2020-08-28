using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player
{
  public  class PlayerList : IList<Player>
    {
        List<Player> list = new List<Player>();
        public PlayerList()
        {

        }

        public PlayerList(PlayerList PlayerList)
        {
            this.list.AddRange(PlayerList.list);
        }
        public int IndexOf(Player item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, Player item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public Player this[int index]
        {
            //wrap index around
            get
            {
                while (index > list.Count() - 1)
                    index -= list.Count();
                while (index < 0)
                    index += list.Count();
                return list[index];
            }
            set
            {
                while (index > list.Count() - 1)
                    index -= list.Count();
                while (index < 0)
                    index += list.Count();
                list[index] = value;
            }
        }
        public Player GetPlayer(ref int index)
        {
            //wrap index and changing the index passed through
            while (index > list.Count() - 1)
                index -= list.Count();
            while (index < 0)
                index += list.Count();
            return list[index];
        }
        public void Add(Player item)
        {
            list.Add(item);
        }
        public void AddRange(PlayerList players)
        {
            list.AddRange(players);
        }
        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(Player item)
        {
            return list.Contains(item);
        }

        public void CopyTo(Player[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Player item)
        {
            return list.Remove(item);
        }

        public IEnumerator<Player> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}

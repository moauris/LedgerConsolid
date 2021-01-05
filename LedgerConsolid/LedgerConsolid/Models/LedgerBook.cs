using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LedgerConsolid.Models
{
    public class LedgerBook : IList<LedgerItem>
    {
        #region IList<LedgerItem> Implementations
        public LedgerItem[] _content = new LedgerItem[0];
        public int _count = 0;
        public LedgerItem this[int index] 
        { 
            get => _content[index]; 
            set => _content[index] = value; 
        }

        public int Count => _count;

        public bool IsReadOnly => false;

        public void Add(LedgerItem item)
        {
            LedgerItem[] temp = new LedgerItem[_count];
            _content.CopyTo(temp, 0);
            _content = new LedgerItem[_count + 1];
            temp.CopyTo(_content, 0);
            _content[_count] = item;
            _count++;
        }

        public void Clear()
        {
            _content = new LedgerItem[0];
            _count = 0;
        }
        public bool Contains(LedgerItem item)
        {
            foreach (LedgerItem ledger in _content)
            {
                if (ledger == item) return true;
            }
            return false;
        }

        public void CopyTo(LedgerItem[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<LedgerItem> GetEnumerator()
        {
            foreach (LedgerItem item in _content)
            {
                yield return item;
            }
        }

        public int IndexOf(LedgerItem item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_content[i] == item) return i;
            }
            return -1;
        }

        public void Insert(int index, LedgerItem item)
        {
            LedgerItem[] temp = new LedgerItem[_count + 1];
            _content.CopyTo(temp, 0);
            for (int i = _count; i > index; i--)
            {
                temp[i - 1] = temp[i];
            }
            temp[index] = item;
            _content = temp;
            _count++;
        }

        public bool Remove(LedgerItem item)
        {
            int index = IndexOf(item);
            if (index > -1)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            if (index <= 0 || index > _count) throw new IndexOutOfRangeException($"Index {index} is out of range [0~{_count}]");
            LedgerItem[] temp = new LedgerItem[_count - 1];
            for (int i = 0; i < index; i++)
            {
                temp[i] = _content[i];
            }
            for (int i = index; i < _count - 1; i++)
            {
                temp[i] = _content[i + 1];
            }
            _content = temp;
            _count--;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}

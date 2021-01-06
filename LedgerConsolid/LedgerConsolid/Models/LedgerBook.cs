using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace LedgerConsolid.Models
{
    public class LedgerBook : IList<LedgerItem>, INotifyPropertyChanged
    {
        public static HashSet<string> ExistingNames = new HashSet<string>();
        public LedgerBook(string title)
        {
            if (ExistingNames.Contains(title))
            {
                throw new Exception($"Name {title} already exist, cannot duplicate");
            }
            Title = title;
            ExistingNames.Add(title);
        }

        public double TotalCredit 
        { 
            get
            {
                IEnumerable<double> CreditCollection = 
                    from entries in _content 
                    where entries.Credit != 0 
                    select entries.Credit;

                return CreditCollection.Sum();
            }
        }
        public double TotalDebit
        {
            get
            {
                IEnumerable<double> DebitCollection =
                    from entries in _content
                    where entries.Debit != 0
                    select entries.Debit;

                return DebitCollection.Sum();
            }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set 
            { 
                _title = value;
                OnPropChanged("Title");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public override string ToString()
        {
            return Title;
        }

        #region IList<LedgerItem> Implementations
        private LedgerItem[] _content = new LedgerItem[0];
        private int _count = 0;


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
            item.Parent = this;
            _content[_count] = item;
            _count++;
            OnPropChanged("Count");
            OnPropChanged("TotalCredit");
            OnPropChanged("TotalDedit");
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
            int remainSize = array.Length - arrayIndex;
            if (remainSize < _count) throw new IndexOutOfRangeException($"Remaining size of target {remainSize} is too small for copying {_count} items");
            if (arrayIndex < 0 || arrayIndex >= array.Length) throw new IndexOutOfRangeException($"Index {arrayIndex} is out of range [0~{array.Length}]");
            for (int i = arrayIndex, j = 0; i < array.Length && j < _count; i++, j++)
                array[i] = _content[j];
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
                temp[i] = temp[i - 1];
            }
            item.Parent = this;
            temp[index] = item;
            _content = temp;
            _count++;
            OnPropChanged("Count");
            OnPropChanged("TotalCredit");
            OnPropChanged("TotalDedit");
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
            if (index < 0 || index > _count) throw new IndexOutOfRangeException($"Index {index} is out of range [0~{_count}]");
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

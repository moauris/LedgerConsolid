using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LedgerConsolid.Models
{
    public enum LedgerItemCreateMode
    {
        Credit, Debit
    }
    public struct LedgerItem : IEquatable<LedgerItem>
    {
        public LedgerBook Parent { get; set; }
        public DateTime IncurredDate { get; set; }
        public string Summary { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }

        public static LedgerItem Create(DateTime IncurDate, string summary, LedgerItemCreateMode mode, double value)
        {
            LedgerItem newItem = new LedgerItem {
                Parent = null,
                IncurredDate = IncurDate,
                Summary = summary,
                Debit = 0,
                Credit = 0
            };
            switch (mode)
            {
                case LedgerItemCreateMode.Credit:
                    newItem.Credit = value;
                    break;
                case LedgerItemCreateMode.Debit:
                    newItem.Debit = value;
                    break;
                default:
                    break;
            }
            return newItem;
        }

        public bool Equals([AllowNull] LedgerItem other)
        {
            if (
                Parent == other.Parent &&
                IncurredDate == other.IncurredDate &&
                Summary == other.Summary &&
                Debit == other.Debit &&
                Credit == other.Credit
                ) return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Parent, IncurredDate, Summary, Debit, Credit);
        }

        public override string ToString()
        {
            return string.Join(',', new object[] { Parent, IncurredDate, Summary, Debit, Credit });
        }

        public static bool operator ==(LedgerItem left, LedgerItem right) =>
            left.Equals(right);
        public static bool operator !=(LedgerItem left, LedgerItem right) =>
            !left.Equals(right);
    }
}

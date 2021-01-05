using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LedgerConsolid.Models
{
    public struct LedgerItem : IEquatable<LedgerItem>
    {
        public LedgerBook Parent { get; set; }
        public DateTime IncurredDate { get; set; }
        public string Summary { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }

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

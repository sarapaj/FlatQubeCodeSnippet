using System;
using System.Collections.Generic;

namespace FlatQubeCodeSnippet.Models
{
    public class TransactionsDataModel
    {
        public long Count { get; set; }
        public long Offset { get; set; }
        public long TotalCount { get; set; }
        public List<Transactions> Transactions { get; set; }

    }

    public class Transactions
    {
        public long CreatedAt { get; set; }
        public Event EventType { get; set; }
        public string Fee { get; set; }
        public string FeeCurrency { get; set; }
        public string Left { get; set; }
        public string LeftAddress { get; set; }
        public string LeftValue { get; set; }
        public string MessageHash { get; set; }
        public string PoolAddress { get; set; }
        public string Right { get; set; }
        public string RightAddress { get; set; }
        public string RightValue { get; set; }
        public long TimeStampBlock { get; set; }
        public string TransactionHash { get; set; }
        public string Tv { get; set; }
        public string UserAddress { get; set; }
    }

    public enum Event
    {
        SWAPLEFTTORIGHT,
        SWAPRIGHTTOLEFT

    }
}

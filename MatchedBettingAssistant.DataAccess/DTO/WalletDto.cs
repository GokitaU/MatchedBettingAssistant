using System.Collections.Generic;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public class WalletDto : AccountDto, IWallet
    {
        public WalletDto(Wallet bank) : base(bank)
        {
        }
    }
}
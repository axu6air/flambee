using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Core.Domain.UserDetails
{
    public class TwoFactorRecoveryCode : BaseEntity
    {
        public string Code { get; set; }
        public bool IsRedeemed { get; set; }
        public DateTime GenerationTime { get; set; }
        public DateTime? RedemptionTime { get; set; }
        public DateTime? ExpirationTime { get; set; }
    }
}
